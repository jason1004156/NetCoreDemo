using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Demo.Common
{
    public class ApiResponse : IDisposable
    {
        //public static Logger _loggerTrace = NLog.LogManager.GetLogger("WebApiLogTraceLogger");
        //public static Logger _logger = NLog.LogManager.GetLogger("WebApiLogLogger");
        //internal static ILoggerFactory _loggerFactory { get; set; }// = new LoggerFactory();
   
        public string status = "FAIL";
        public string version => "1.0";
        public int statusCode { get; set; }
        public string requestId { get; }
        public string errorMessage { get; set; }
        public string txId { get; set; }
        public string errorCode { get; set; }
        public object result { get; set; }

        public ApiResponse()
        {
        }

        public static readonly ApiResponse Instance = new ApiResponse();

        public ApiResponse CreateOK(object createResult = null)
        {
            return new ApiResponse(HttpStatusCode.OK, "OK", createResult, "");
        }
        public async Task<ApiResponse> CreateOKAsync(object createResult = null)
        {
            return await Task.Run(() => new ApiResponse(HttpStatusCode.OK, "OK", createResult, ""));
        }

        public ApiResponse CreateFail(HttpResponse Response, string failErrorMessage)
        {
            //switch (errorMessage)
            //{
            //    //case ExceptionConst.sdk_validator_not_exist:
            //    //    return new CommonApiResponse(HttpStatusCode.Forbidden, "FAIL", null, errorMessage);
            //    default:
            //        return new CommonApiResponse((HttpStatusCode)Response.StatusCode , "FAIL", null, errorMessage);
            //}
            if (Response.StatusCode == 200)
                return new ApiResponse(HttpStatusCode.BadRequest, "FAIL", null, failErrorMessage);
            else
                return new ApiResponse((HttpStatusCode)Response.StatusCode, "FAIL", null, failErrorMessage);

        }

        public ApiResponse CreateFail(HttpResponse Response, string failErrorMessage , string errCode , string errTxId )
        {
            //switch (errorMessage)
            //{
            //    //case ExceptionConst.sdk_validator_not_exist:
            //    //    return new CommonApiResponse(HttpStatusCode.Forbidden, "FAIL", null, errorMessage);
            //    default:
            //        return new CommonApiResponse((HttpStatusCode)Response.StatusCode , "FAIL", null, errorMessage);
            //}
            if (Response.StatusCode == 200)
                return new ApiResponse(HttpStatusCode.BadRequest, "FAIL", null, failErrorMessage ,errCode , errTxId);
            else
                return new ApiResponse((HttpStatusCode)Response.StatusCode, "FAIL", null, failErrorMessage, errCode, errTxId);

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        protected ApiResponse(HttpStatusCode httpStatusCode, string resStatus, object resResult = null, string failErrorMessage = null ,string errCode = null , string errTxId = null)
        {

            requestId = Guid.NewGuid().ToString();
            statusCode = (int)httpStatusCode;
            result = resResult;
            errorMessage = failErrorMessage;
            status = resStatus;
            txId = errTxId;
            errorCode = errCode;

        }


    }


}
