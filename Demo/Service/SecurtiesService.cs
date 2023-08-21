using System;
using System.Text;
using Demo.Service.Base;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Demo.Utilities;
using Demo.Common;
using Demo.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;

namespace Demo.Service
{
    public interface ISecurtiesService : IBaseService
    {
        public Task<HttpResponseObject> RefreshData();

        public Task<List<Security>> GetSecurity();
    }

    public class SecurtiesService : ISecurtiesService
    {
        private DemoContext _db;

        public SecurtiesService(DemoContext db) 
        {
            _db = db;
        }

        public async Task<HttpResponseObject> RefreshData()
        {
            String url = "https://www.twse.com.tw/rwd/zh/lending/t13sa710?startDate=20230601&endDate=20230602&tradeType=&stockNo=&response=json";
            HttpResponseObject res = await GetSecuritiesAsync(url);

            return res;
        }

        public async Task<List<Security>> GetSecurity()
        {
            List<Security> securities = _db.Security.ToList<Security>() ;
            return securities;
        }

        private async Task<HttpResponseObject> GetSecuritiesAsync(string path)
        {
            HttpResponseObject result = new HttpResponseObject();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var getMsg = response.Content.ReadAsStringAsync().Result;

                var dataObj = JObject.Parse(getMsg)["data"];
                if (dataObj != null)
                {
                    result.data = dataObj.ToObject<List<List<string>>>();
                    handleDateAndSave(result.data);
                }

            }

            _db.SaveChanges();
            return result;
        }

        private void handleDateAndSave(List<List<String>> dataList)
        {
            List<Security> securityList = new List<Security>();
            deleteData();
            foreach (List<String> data in dataList)
            {
                string securityStr = data.ToArray()[1];
                string code = securityStr.Split(" ")[0];
                string name = securityStr.Split(" ")[1];
                Security security;
                bool existsFlg = true;

                security = _db.Security.Where(x => x.Code == code).First();
                if(security == null)
                {
                    security = new Security();
                    security.Code = code;
                    existsFlg = false;
                }
                int valueIndex = 0;
                foreach(string value in securityStr.Split(" ").ToList()){
                    if (String.IsNullOrWhiteSpace(value))
                    {
                        continue;
                    } else
                    {
                        if(valueIndex != 1)
                        {
                            valueIndex++;
                        } else
                        {
                            name = value;
                            break;
                        }
                    }
                }
                security.Name = name;

                if (existsFlg)
                {
                    _db.Update(security);
                }
                else
                {
                    _db.Add(security);
                }
                 
            }
        }

        private void deleteData()
        {
            List<Security> securities = _db.Security.Where<Security>(x => x.Name == "").ToList();

            _db.RemoveRange(securities);
        }

    }


    


}

