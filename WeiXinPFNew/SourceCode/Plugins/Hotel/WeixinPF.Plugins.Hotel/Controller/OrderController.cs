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
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Common.Helper;
using WeixinPF.Messages.RequestResponse;
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

                    int wid = orderRequest.Wid;
                    //总花费
                    var dateSpan = orderDto.LeaveTime - orderDto.ArriveTime;
                    var totalPrice = orderDto.OrderPrice * orderDto.OrderNum * dateSpan.Days;

                    var port = WebHelper.GetHostPort();
                    var url = string.Format("{0}/Functoin/BackPage/hotel_order_paycallback.aspx", port);
                    var entity = new UnifiedOrderEntity
                    {
                        wid = wid,
                        total_fee = totalPrice == null ? 0 : (int)totalPrice,
                        out_trade_no = orderDto.OrderNumber,
                        openid = orderRequest.OpenId,
                        OrderId = orderRequest.OrderId.ToString(),
                        body = string.Format("订单编号{2} {3}{1}{0}间", orderDto.OrderNum, orderDto.RoomType
                            , orderDto.OrderNumber, orderDto.HotelName),
                        PayModuleID = (int)PayModuleEnum.Hotel,
                        PayComplete = url
                    };

                    entity.Extra.Add("orderId", orderRequest.OrderId.ToString());
                    entity.Extra.Add("openid", orderRequest.OpenId);
                    entity.Extra.Add("hotelid", orderDto.HotelId.ToString());
                    entity.Extra.Add("roomid", orderDto.RoomId.ToString());
                    entity.Extra.Add("wid", wid.ToString());

                    var ticket = EncryptionManager.CreateIV();
                    var payData = EncryptionManager.AESEncrypt(entity.ToJson(), ticket);

                    var strResult = PayHelper.GetPayUrl(payData, ticket);
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





    #region 测试支付用代码 接好后删除 
  /// <summary>
    /// 统一下单对象
    /// </summary>

    public class UnifiedOrderEntity
    {
        /// <summary>
        /// 微信号ID
        /// </summary>

        public int wid { get; set; }

        /// <summary>
        /// 支付模块ID
        /// </summary>

        public int PayModuleID { get; set; }

        /// <summary>
        /// 业务系统订单号
        /// </summary>

        public string OrderId { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>

        public string body { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>

        public string out_trade_no { get; set; }

        /// <summary>
        /// 支付费用，单位：分
        /// </summary>

        public int total_fee { get; set; }

        /// <summary>
        /// 支付的OpenID
        /// </summary>

        public string openid { get; set; }

        /// <summary>
        /// 支付前
        /// </summary>

        public string BeforePay { get; set; }

        /// <summary>
        /// 支付成功
        /// </summary>

        public string PaySuccess { get; set; }

        /// <summary>
        /// 支付失败
        /// </summary>

        public string PayFail { get; set; }

        /// <summary>
        /// 支付取消
        /// </summary>

        public string PayCancel { get; set; }

        /// <summary>
        /// 支付完成
        /// </summary>

        public string PayComplete { get; set; }

        /// <summary>
        /// 额外参数
        /// </summary>

        public Dictionary<string, string> Extra = new Dictionary<string, string>();

        public string ToJson()
        {
            string message;
            if (!CheckRequired(out message))
            {
                throw new Exception(message);
            }

            return JSONHelper.Serialize(this, "yyyy-MM-dd");
        }

        public bool CheckRequired(out string message)
        {
            var stringBuilder = new StringBuilder();
            const string msg = "{0}必须赋值{1}";
            if (wid <= 0)
                stringBuilder.AppendFormat(msg, "wid", Environment.NewLine);

            if (PayModuleID < 0)
                stringBuilder.AppendFormat(msg, "PayModuleID", Environment.NewLine);

            if (string.IsNullOrEmpty(OrderId))
                stringBuilder.AppendFormat(msg, "OrderId", Environment.NewLine);

            if (string.IsNullOrEmpty(body))
                stringBuilder.AppendFormat(msg, "body", Environment.NewLine);

            if (string.IsNullOrEmpty(out_trade_no))
                stringBuilder.AppendFormat(msg, "out_trade_no", Environment.NewLine);

            if (total_fee <= 0)
                stringBuilder.AppendFormat(msg, "total_fee", Environment.NewLine);

            if (string.IsNullOrEmpty(openid))
                stringBuilder.AppendFormat(msg, "openid", Environment.NewLine);

            if (stringBuilder.Length > 0)
            {
                message = stringBuilder.ToString();
                return false;
            }

            message = string.Empty;
            return true;
        }
    }

    public enum PayModuleEnum
    {
        /// <summary>
        /// 餐饮
        /// </summary>
        Restaurant = 0,

        /// <summary>
        /// 酒店
        /// </summary>
        Hotel = 1
    }
    #endregion


  

}