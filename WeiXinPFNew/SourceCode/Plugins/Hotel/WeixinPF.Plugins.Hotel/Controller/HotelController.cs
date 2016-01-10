using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using NServiceBus;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Shared;

namespace WeixinPF.Hotel.Plugins.Controller
{
    public class HotelController : ApiController
    {
        private const int WaitSeconds = 10000000;

        /// <summary>
        /// 获取酒店基本信息
        /// </summary>
        public GetHotelResponse GetHotelInfo(GetHotelRequest request)
        {
            try
            {
                GetHotelResponse responseData = null;
                IAsyncResult asyncResult = BusEntry.dictBus["hotel"].Send(
                    "WeixinPF.Hotel.Plugins.Service",
                    request
                ).Register(response =>
                {
                    CompletionResult result = response.AsyncState as CompletionResult;
                    if (result != null)
                    {
                        responseData = result.Messages[0] as GetHotelResponse;

                    }
                }, this);

                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(WaitSeconds);

                if (!asyncResult.IsCompleted)
                {
                    throw new HttpResponseException(
                     Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                     "获取酒店信息失败。"));
                }
                return responseData;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(
                     Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                     "获取酒店信息失败。"));
            }
        }
    }
}