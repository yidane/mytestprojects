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

        private string ServiceName = "WeixinPF.Hotel.Plugins.Service";


        /// <summary>
        /// 获取房间列表
        /// </summary>
        public GetRoomListResponse GetRoomList([FromUri]GetRoomListRequest request)
        {
            try
            {
                var result = Global.Bus.Send<GetRoomListResponse>(ServiceName, request);
                if (!result.IsSuccess)
                {
                    throw new HttpResponseException(
                     Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                     "获取房间信息失败。"));
                }
                return result.Data;
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

        public GetRoomResponse GetRoom([FromUri]GetRoomRequest request)
        {
            try
            {
                var result = Global.Bus.Send<GetRoomResponse>(ServiceName, request);
                if (!result.IsSuccess)
                {
                    throw new HttpResponseException(
                     Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                     "获取房间信息失败。"));
                }
                return result.Data;
            }
            catch
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "获取房间信息失败。"));
            }
        }
    }
}