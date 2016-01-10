using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Services;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Common.Extension;
using WeixinPF.Common.Helper;
using WeixinPF.Messages.Command;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Messages.RequestResponse.Dtos;
using WeixinPF.Shared;

namespace WeixinPF.Hotel.Plugins.Functoin.Service
{
    /// <summary>
    /// HotelService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class HotelService : System.Web.Services.WebService
    {
        private const int WaitSeconds = 10000000;

        [WebMethod]
        public void Test()
        {
            this.WriteJson(new { success = true, message = "我是测试服务，请向我开炮！！！！" });
        }
        [WebMethod]
        public void HelloWorld(int wid, int shopId)
        {


        }

        /// <summary>
        /// 获取酒店基本信息
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="openid"></param>
        /// <param name="hotelId"></param>
        [WebMethod]
        public void GetHotelInfo(int wid, string openid, int hotelId)
        {
            AjaxResult ajaxResult;
            try
            {
                GetHotelResponse responseData = null;
                IAsyncResult asyncResult = new BusEntry("WeixinPF.Hotel.Plugins").MyBus.Send("WeixinPF.Hotel.Plugins.Service",
                    new GetHotelRequest() { HotelId = hotelId })
                    .Register(response =>
                    {
                        CompletionResult result = response.AsyncState as CompletionResult;
                        if (result != null)
                        {
                            responseData = result.Messages[0] as GetHotelResponse;

                        }
                    }, this);

                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(WaitSeconds);

                ajaxResult = asyncResult.IsCompleted ?
                    AjaxResult.Succeed(responseData) :
                    AjaxResult.Fail("获取酒店信息失败。");
            }
            catch (Exception ex)
            {
                ajaxResult = AjaxResult.Fail("获取酒店信息失败。");
            }
            this.WriteJson(ajaxResult);
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="openid"></param>
        /// <param name="hotelId"></param>
        [WebMethod]
        public void GetRoomList(int wid, string openid, int hotelId)
        {
            AjaxResult ajaxResult;
            try
            {
                GetRoomListResponse responseData = null;
                IAsyncResult asyncResult = new BusEntry("WeixinPF.Hotel.Plugins").MyBus.Send("WeixinPF.Hotel.Plugins.Service",
                    new GetRoomListRequest() { HotelId = hotelId })
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

                ajaxResult = asyncResult.IsCompleted ?
                  AjaxResult.Succeed(responseData.Rooms) :
                  AjaxResult.Fail("获取房间列表失败。");
            }
            catch
            {
                ajaxResult = AjaxResult.Fail("获取房间列表失败。");
            }

            this.WriteJson(ajaxResult);
        }

        /// <summary>
        /// 获取房间明细
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="openid"></param>
        /// <param name="hotelId"></param>
        /// <param name="roomId"></param>
        [WebMethod]
        public void GetRoom(int wid, string openid, int hotelId, int roomId)
        {
            AjaxResult ajaxResult;
            try
            {
                GetRoomResponse responseData = null;
                IAsyncResult asyncResult = new BusEntry("WeixinPF.Hotel.Plugins").MyBus.Send("WeixinPF.Hotel.Plugins.Service", new GetRoomRequest() { HotelId = hotelId, RoomId = roomId })
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

                ajaxResult = asyncResult.IsCompleted ?
                      AjaxResult.Succeed(responseData) :
                      AjaxResult.Fail("获取房间信息失败。");
            }
            catch
            {
                ajaxResult = AjaxResult.Fail("获取房间信息失败。");
            }

            this.WriteJson(ajaxResult);
        }

        /// <summary>
        /// 获取最新历史订单里用户信息
        /// </summary>
        [WebMethod]
        public void GetOrderLastUserInfo(int wid, string openid)
        {
            AjaxResult ajaxResult = AjaxResult.Succeed(OrderUserDto.Empty());
            try
            {
                GetOrderUserInfoResponse responseData = null;
                IAsyncResult asyncResult = new BusEntry("WeixinPF.Hotel.Plugins").MyBus.Send("WeixinPF.Hotel.Plugins.Service", new GetOrderUserInfoRequest() { OpendId = openid })
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

                if (asyncResult.IsCompleted)
                {
                    AjaxResult.Succeed(responseData.User);
                }
            }
            catch
            {
                //ajaxResult = AjaxResult.Fail("获取房间信息失败。");
            }

            this.WriteJson(ajaxResult);
        }

        /// <summary>
        /// 保存订单
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="openid"></param>
        /// <param name="hotelId"></param>
        /// <param name="roomId"></param>
        /// <param name="roomType"></param>
        /// <param name="order"></param>
        [WebMethod]
        public void SaveOrder(int wid, string openid, int hotelId, int roomId, string roomType, string order)
        {
            AjaxResult ajaxResult;
            try
            {
                CreateOrderResponse responseData = null;
                CreateOrderRequest request = new CreateOrderRequest()
                {
                    Wid = wid,
                    OpenId = openid,
                    HotelId = hotelId,
                    RoomId = roomId,
                    RoomType = roomType,
                    Order = JSONHelper.Deserialize<OrderDto>(order)
                };
                IAsyncResult asyncResult = new BusEntry("WeixinPF.Hotel.Plugins").MyBus.Send("WeixinPF.Hotel.Plugins.Service", request)
                        .Register(response =>
                        {
                            CompletionResult result = response.AsyncState as CompletionResult;
                            if (result != null)
                            {
                                responseData = result.Messages[0] as CreateOrderResponse;

                            }
                        }, this);

                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(WaitSeconds);

                ajaxResult = asyncResult.IsCompleted ?
                          AjaxResult.Succeed(responseData.OrderId) :
                          AjaxResult.Fail("保存订失败。");
            }
            catch
            {
                ajaxResult = AjaxResult.Fail("保存订失败。");
            }

            this.WriteJson(ajaxResult);
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="openid"></param>
        /// <param name="orderId"></param>
        [WebMethod]
        public void GetOrder(int wid, string openid, int orderId)
        {
            AjaxResult ajaxResult;
            try
            {
                GetOrderResponse responseData = null;
                IAsyncResult asyncResult = new BusEntry("WeixinPF.Hotel.Plugins").MyBus.Send("WeixinPF.Hotel.Plugins.Service", new GetOrderRequest() { OrderId = orderId })
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

                ajaxResult = asyncResult.IsCompleted ?
                          AjaxResult.Succeed(responseData.Order) :
                          AjaxResult.Fail("获取订单信息失败。");
            }
            catch
            {
                ajaxResult = AjaxResult.Fail("获取订单信息失败。");
            }

            this.WriteJson(ajaxResult);
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="openid"></param>
        /// <param name="hotelId"></param>
        [WebMethod]
        public void GetOrderList(int wid, string openid, int hotelId)
        {
            AjaxResult ajaxResult;
            try
            {
                GetOrderListResponse responseData = null;
                IAsyncResult asyncResult = new BusEntry("WeixinPF.Hotel.Plugins").MyBus.Send("WeixinPF.Hotel.Plugins.Service",
                new GetOrderListRequest()
                {
                    HotelId = hotelId,
                    OpenId = openid
                }).Register(response =>
                {
                    CompletionResult result = response.AsyncState as CompletionResult;
                    if (result != null)
                    {
                        responseData = result.Messages[0] as GetOrderListResponse;

                    }
                }, this);

                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(WaitSeconds);

                ajaxResult = asyncResult.IsCompleted ?
                              AjaxResult.Succeed(responseData.Orders) :
                              AjaxResult.Fail("获取订单列表失败。");
            }
            catch
            {
                ajaxResult = AjaxResult.Fail("获取订单列表失败。");
            }

            this.WriteJson(ajaxResult);
        }

        /// <summary>
        /// 获取订单数量
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="openid"></param>
        /// <param name="hotelId"></param>
        [WebMethod]
        public void GetOrderCount(int wid, string openid, int hotelId)
        {
            AjaxResult ajaxResult;
            try
            {
                GetOrderListResponse responseData = null;
                IAsyncResult asyncResult = new BusEntry("WeixinPF.Hotel.Plugins").MyBus.Send("WeixinPF.Hotel.Plugins.Service", new GetOrderListRequest() { HotelId = hotelId })
                        .Register(response =>
                        {
                            CompletionResult result = response.AsyncState as CompletionResult;
                            if (result != null)
                            {
                                responseData = result.Messages[0] as GetOrderListResponse;

                            }
                        }, this);

                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(WaitSeconds);

                ajaxResult = asyncResult.IsCompleted ?
                              AjaxResult.Succeed(responseData.Orders.Count) :
                              AjaxResult.Fail("获取订单数量失败。");
            }
            catch
            {
                ajaxResult = AjaxResult.Fail("获取订单数量失败。");
            }

            this.WriteJson(ajaxResult);
        }
    }
}
