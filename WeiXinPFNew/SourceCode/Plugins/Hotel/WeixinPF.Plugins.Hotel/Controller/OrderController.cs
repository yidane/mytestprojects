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
    public class OrderController : ApiController
    {
        private const int WaitSeconds = 10000000;
        public CreateOrderResponse Save(CreateOrderRequest request)
        {
            try
            {
                CreateOrderResponse responseData = null;

                IAsyncResult asyncResult = BusEntry.dictBus["hotel"].Send("WeixinPF.Hotel.Plugins.Service", request)
                        .Register(response =>
                        {
                            CompletionResult result = response.AsyncState as CompletionResult;
                            if (result != null)
                            {
                                responseData = result.Messages[0] as CreateOrderResponse;

                            }
                        }, this);

                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(1000000000);

                if (asyncResult.IsCompleted)
                {
                    return responseData;
                }
                else
                {
                    throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "保存订失败。"));
                }
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

        public GetOrderUserInfoResponse GetOrderLastUserInfo(GetOrderUserInfoRequest request)
        {
            try
            {
                GetOrderUserInfoResponse responseData = null;
                IAsyncResult asyncResult = BusEntry.dictBus["hotel"].Send("WeixinPF.Hotel.Plugins.Service",
                    request)
                        .Register(response =>
                        {
                            CompletionResult result = response.AsyncState as CompletionResult;
                            if (result != null)
                            {
                                responseData = result.Messages[0] as GetOrderUserInfoResponse;

                            }
                        }, this);

                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(WaitSeconds);

                if (!asyncResult.IsCompleted)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "获取用户信息失败。"));
                }

                return responseData;
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

        public GetOrderResponse GetOrder(GetOrderRequest request)
        {
            AjaxResult ajaxResult;
            try
            {
                GetOrderResponse responseData = null;
                IAsyncResult asyncResult = BusEntry.dictBus["hotel"].Send("WeixinPF.Hotel.Plugins.Service",
                    request)
                        .Register(response =>
                        {
                            CompletionResult result = response.AsyncState as CompletionResult;
                            if (result != null)
                            {
                                responseData = result.Messages[0] as GetOrderResponse;
                            }
                        }, this);

                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(WaitSeconds);

                if (!asyncResult.IsCompleted)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                       "获取订单信息失败。"));
                }
                return responseData;
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
        /// <param name="wid"></param>
        /// <param name="openid"></param>
        /// <param name="hotelId"></param>

        public GetOrderListResponse GetOrderList(GetOrderListRequest request)
        {
            try
            {
                GetOrderListResponse responseData = null;
                IAsyncResult asyncResult = BusEntry.dictBus["hotel"].Send("WeixinPF.Hotel.Plugins.Service",
                request).Register(response =>
                {
                    CompletionResult result = response.AsyncState as CompletionResult;
                    if (result != null)
                    {
                        responseData = result.Messages[0] as GetOrderListResponse;

                    }
                }, this);

                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(WaitSeconds);

                if (!asyncResult.IsCompleted)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
"获取订单列表失败。"));
                }
                return responseData;
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
        /// <param name="wid"></param>
        /// <param name="openid"></param>
        /// <param name="hotelId"></param>

        public GetOrderCountResponse GetOrderCount(GetOrderCountRequest request)
        {
            AjaxResult ajaxResult;
            try
            {
                GetOrderCountResponse responseData = null;
                IAsyncResult asyncResult = BusEntry.dictBus["hotel"].Send("WeixinPF.Hotel.Plugins.Service",
                    request)
                        .Register(response =>
                        {
                            CompletionResult result = response.AsyncState as CompletionResult;
                            if (result != null)
                            {
                                responseData = result.Messages[0] as GetOrderCountResponse;

                            }
                        }, this);

                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(WaitSeconds);

                if (!asyncResult.IsCompleted)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        "获取订数量表失败。"));
                }
                return responseData;
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