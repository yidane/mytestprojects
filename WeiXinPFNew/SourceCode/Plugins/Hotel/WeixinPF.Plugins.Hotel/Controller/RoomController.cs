using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Shared;

namespace WeixinPF.Hotel.Plugins.Controller
{
    public class RoomController : ApiController
    {
        private const int WaitSeconds = 10000000;


        /// <summary>
        /// 获取房间列表
        /// </summary>
        public GetRoomListResponse GetRoomList(GetRoomListRequest request)
        {
            try
            {
                GetRoomListResponse responseData = null;
                IAsyncResult asyncResult = BusEntry.dictBus["hotel"].Send("WeixinPF.Hotel.Plugins.Service",
                    request)
                    .Register(response =>
                    {
                        CompletionResult result = response.AsyncState as CompletionResult;
                        if (result != null)
                        {
                            responseData = result.Messages[0] as GetRoomListResponse;

                        }
                    }, this);

                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(WaitSeconds);

                if (!asyncResult.IsCompleted)
                {
                    throw new HttpResponseException(
                     Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                     "获取房间信息失败。"));
                }
                return responseData;
            }
            catch
            {
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "获取房间信息失败。"));
            }
        }

        /// <summary>
        /// 获取房间明细
        /// </summary>

        public GetRoomResponse GetRoom(GetRoomRequest request)
        {
            try
            {
                GetRoomResponse responseData = null;
                IAsyncResult asyncResult = BusEntry.dictBus["hotel"].Send("WeixinPF.Hotel.Plugins.Service", request)
                        .Register(response =>
                        {
                            CompletionResult result = response.AsyncState as CompletionResult;
                            if (result != null)
                            {
                                responseData = result.Messages[0] as GetRoomResponse;

                            }
                        }, this);

                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(WaitSeconds);

                if (asyncResult.IsCompleted)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        "获取房间信息失败。"));
                }
                return responseData;
            }
            catch
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "获取房间信息失败。"));
            }
        }
    }
}