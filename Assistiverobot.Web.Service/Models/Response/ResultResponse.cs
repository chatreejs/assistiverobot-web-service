using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Assistiverobot.Web.Service.Constants;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assistiverobot.Web.Service.Models.Response
{
    public class ResultResponse
    {
        private string Message { get; set; }
        private object Result { get; set; }
        public static object GetResultSuccess(object result = null)
        {
            return new ResultResponse()
            {
                Message = StatusMessage.MESSAGE_SUCCESS,
                Result = result,
            };
        }
    }
}