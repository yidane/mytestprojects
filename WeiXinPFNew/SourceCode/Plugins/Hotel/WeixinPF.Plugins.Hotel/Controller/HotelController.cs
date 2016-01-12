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
        /// <summary>
        /// 获取酒店基本信息
        /// </summary>
        public GetHotelResponse GetHotelInfo([FromUri]GetHotelRequest request)
        {
            try
            {
                var result = Global.Bus.Send<GetHotelResponse>(Constants.HotelServiceAddress, request);
                if (!result.IsSuccess)
                {
                    throw new HttpResponseException(
                     Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                     "获取酒店信息失败。"));
                }
                return result.Data;
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