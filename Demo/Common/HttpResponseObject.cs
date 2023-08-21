using System;
namespace Demo.Common
{
    public class HttpResponseObject
    {
        //public string stat { get; set; }
        //public ParamsObject condition { get; set; }
        //public string title { get; set; }
        //public List<string> fields { get; set; }
        public List<List<string>> data { get; set; }
        //public List<String> notes { get; set; }
    }

    public class ParamsObject
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string tradeType { get; set; }
        public string stockNo { get; set; }
        public string response { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public string lang { get; set; }
    }
}

