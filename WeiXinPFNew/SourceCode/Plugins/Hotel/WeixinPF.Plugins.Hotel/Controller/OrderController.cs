using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Shared;

namespace WeixinPF.Hotel.Plugins.Controller
{
    public class OrderController : ApiController
    {
        private const int WaitSeconds = 10000000;
        private const string ServiceName = "WeixinPF.Hotel.Plugins.Service";

        [System.Web.Http.HttpPost]
        public CreateOrderResponse Save(CreateOrderRequest request)
        {
            try
            {
                var result = Global.Bus.Send<CreateOrderResponse>(ServiceName, request);
                if (!result.IsSuccess)
                {

                    throw new HttpResponseException(
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            "保存订失败。"));
                }
                return result.Data;
            }
            catch
            {
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "保存订失败。"));
            }

        }

        /// <summary>
        /// 获取最新历史订单里用户信息
        /// </summary>
        public GetOrderUserInfoResponse GetOrderLastUserInfo([FromUri]GetOrderUserInfoRequest request)
        {
            try
            {
                var result = Global.Bus.Send<GetOrderUserInfoResponse>(ServiceName, request);
                if (!result.IsSuccess)
                {

                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "获取用户信息失败。"));
                }
                return result.Data;
            }
            catch
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                       "获取用户信息失败。"));
            }
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        public GetOrderResponse GetOrder([FromUri]GetOrderRequest request)
        {
            try
            {
                var result = Global.Bus.Send<GetOrderResponse>(ServiceName, request);
                if (!result.IsSuccess)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        "获取订单信息失败。"));
                }
                return result.Data;
            }
            catch
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        "获取订单信息失败。"));
            }
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        public GetOrderListResponse GetOrderList([FromUri]GetOrderListRequest request)
        {
            try
            {
                var result = Global.Bus.Send<GetOrderListResponse>(ServiceName, request);
                if (!result.IsSuccess)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "获取订单列表失败。"));
                }
                return result.Data;
            }
            catch
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                "获取订单列表失败。"));
            }
        }

        /// <summary>
        /// 获取订单数量
        /// </summary>
        public GetOrderCountResponse GetOrderCount([FromUri]GetOrderCountRequest request)
        {
            try
            {
                var result = Global.Bus.Send<GetOrderCountResponse>(ServiceName, request);
                if (!result.IsSuccess)
                {

                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            "获取数量列表失败。"));
                }
                return result.Data;
            }
            catch
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        "获取数量列表失败。"));
            }
        }
        public List<QrCodeDto> GetQrCode(int orderId)
        {
            try
            {
                var data = new List<QrCodeDto>();
                for (int i = 1; i <= 3; i++)
                {
                    var qr = new QrCodeDto()
                    {
                        Code = "jxiaoxi" + i,
                        Status = i
                    };
                    data.Add(qr);
                }
                return data;
            }
            catch
            {

                throw new HttpResponseException(
                     Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                     "获取验证码失败。"));
            }
        }
    }

    public class QrCodeDto
    {
        public string Code { get; set; }
        public int Status { get; set; }
    }
}