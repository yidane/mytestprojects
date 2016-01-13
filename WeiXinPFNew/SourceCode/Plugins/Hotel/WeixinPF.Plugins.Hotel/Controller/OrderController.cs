using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using NServiceBus;
using WeixinPF.Application.Weixin.Pay.Service;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Common.Helper;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Model.WeiXin.Pay;
using WeixinPF.Shared;

namespace WeixinPF.Hotel.Plugins.Controller
{
    public class OrderController : ApiController
    {
        [HttpPost]
        public CreateOrderResponse Save(CreateOrderRequest request)
        {
            try
            {
                var result = Global.Bus.Send<CreateOrderResponse>(Constants.HotelServiceAddress, request);
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
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public CancelOrderResponse Cancel(CancelOrderRequest request)
        {
            try
            {
                var result = Global.Bus.Send<CancelOrderResponse>(Constants.HotelServiceAddress, request);
                if (!result.IsSuccess)
                {

                    throw new HttpResponseException(
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            "取消订单失败。"));
                }
                return result.Data;
            }
            catch
            {
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "取消订单失败。"));
            }
        }

        /// <summary>
        /// 获取支付url
        /// </summary> 
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        public string GetPayUrl([FromUri]GetOrderRequest orderRequest)
        {
            try
            {

                var order = Global.Bus.Send<GetOrderResponse>(Constants.HotelServiceAddress, orderRequest);
                if (!order.IsSuccess)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        "获取订单信息失败。"));
                }
                var orderDto = order.Data;
                if (orderDto != null)
                {
                    var unifiedOrderService = new UnifiedOrderService();
                    int wid = orderRequest.Wid;
                    //总花费
                    var dateSpan = orderDto.LeaveTime - orderDto.ArriveTime;
                    var totalPrice = orderDto.OrderPrice * orderDto.OrderNum * dateSpan.Days * 100;//单位分

                    var port = WebHelper.GetHostPort();
                    var url = string.Format("{0}/Functoin/BackPage/hotel_order_paycallback.aspx", port);

                    var entity = new UnifiedOrderInfo()
                    {
                        PayModuleName = "hotel",
                        AppId = wid,
                        TotalFee = totalPrice == null ? 0 : (int)totalPrice,
                        OutTradeNo = orderDto.OrderNumber,
                        Openid = orderRequest.OpenId,
                        OrderId = orderRequest.OrderId.ToString(),
                        Body = string.Format("订单编号{2} {3}{1}{0}间", orderDto.OrderNum, orderDto.RoomType
                            , orderDto.OrderNumber, orderDto.HotelName),
                        PayComplete = url
                    };
                    entity.Extra.Add("orderId", orderRequest.OrderId.ToString());
                    entity.Extra.Add("openid", orderRequest.OpenId);
                    entity.Extra.Add("hotelid", orderDto.HotelId.ToString());
                    entity.Extra.Add("roomid", orderDto.RoomId.ToString());
                    entity.Extra.Add("wid", wid.ToString());


                    var strResult = unifiedOrderService.GeneratePayUrl(entity);
                    if (string.IsNullOrEmpty(strResult))
                    {
                        throw new Exception("生成下单链接失败");
                    }
                    return strResult;
                }
                else
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "订单不存在"));
                }
            }
            catch (HttpResponseException)
            {
                throw;
            }
            catch
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        "获取支付信息失败"));
            }
        }

        //public CancelOrderResponse Payed(PayedOrderRequest request)
        //{
        //    try
        //    {
        //        var result = Global.Bus.Send<PayedOrderResponse>(Constants.HotelServiceAddress, request);
        //        if (!result.IsSuccess)
        //        {

        //            throw new HttpResponseException(
        //                Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
        //                    "更改订单状态失败。"));
        //        }
        //        return result.Data;
        //    }
        //    catch
        //    {
        //        throw new HttpResponseException(
        //            Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
        //            "更改订单状态失败。"));
        //    }
        //}
        /// <summary>
        /// 获取最新历史订单里用户信息
        /// </summary>
        public GetOrderUserInfoResponse GetOrderLastUserInfo([FromUri]GetOrderUserInfoRequest request)
        {
            try
            {
                var result = Global.Bus.Send<GetOrderUserInfoResponse>(Constants.HotelServiceAddress, request);
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
                var result = Global.Bus.Send<GetOrderResponse>(Constants.HotelServiceAddress, request);
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
                var result = Global.Bus.Send<GetOrderListResponse>(Constants.HotelServiceAddress, request);
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
                var result = Global.Bus.Send<GetOrderCountResponse>(Constants.HotelServiceAddress, request);
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
        public GetIdentifyingCodeByOrderResponse GetQrCode(GetIdentifyingCodeByOrderRequest request)
        {
            try
            {
                var result = Global.Bus.Send<GetIdentifyingCodeByOrderResponse>(Constants.HotelServiceAddress, request);
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
    }








}