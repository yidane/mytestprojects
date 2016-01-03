using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Services;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Common.Extension;
using WeixinPF.Common.Helper;
using WeixinPF.Messages.Command;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Plugins.Hotel;

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

        [WebMethod]
        public void HelloWorld(int wid, int shopId)
        {
            ShowAllRoomResponse res = null;

            IAsyncResult responseData =
                Global.Bus
                .Send("WeixinPF.Plugins.Hotel.Service.HotelService", new ShowAllRoom() { ShopId = shopId, Wid = wid })
                .Register(response =>
                {
                    CompletionResult localResult = (CompletionResult)response.AsyncState;
                    res = localResult.Messages[0] as ShowAllRoomResponse;
                }, this);

            WaitHandle asyncWaitHandle = responseData.AsyncWaitHandle;
            asyncWaitHandle.WaitOne(10000);

            if (responseData.IsCompleted)
            {
                Context.Response.Write(string.Format("{{'RoomName':'{0}','Price':'{1}'}}", res.RoomName, res.Price));
                Context.Response.End();
            }

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
            GetHotelResponse responseData = null;
            IAsyncResult asyncResult = Global.Bus.Send("WeixinPF.Plugins.Hotel.Service.HotelService", new GetHotelRequest() { HotelId = hotelId })
                    .Register(response =>
                    {
                        CompletionResult result = response.AsyncState as CompletionResult;
                        if (result != null)
                        {
                            responseData = result.Messages[0] as GetHotelResponse;

                        }
                    }, this);

            WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
            asyncWaitHandle.WaitOne(10000);

            if (asyncResult.IsCompleted)
            {
                this.WriteJson(AjaxResult.Succeed(responseData));
            }
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
            GetRoomListResponse responseData = null;
            IAsyncResult asyncResult = Global.Bus.Send("WeixinPF.Plugins.Hotel.Service.HotelService", new GetRoomListRequest() { HotelId = hotelId })
                    .Register(response =>
                    {
                        CompletionResult result = response.AsyncState as CompletionResult;
                        if (result != null)
                        {
                            responseData = result.Messages[0] as GetRoomListResponse;

                        }
                    }, this);

            WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
            asyncWaitHandle.WaitOne(10000);

            if (asyncResult.IsCompleted)
            {
                this.WriteJson(AjaxResult.Succeed(responseData));
            }
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
            GetRoomResponse responseData = null;
            IAsyncResult asyncResult = Global.Bus.Send("WeixinPF.Plugins.Hotel.Service.HotelService", new GetRoomRequest() { HotelId = hotelId, RoomId = roomId })
                    .Register(response =>
                    {
                        CompletionResult result = response.AsyncState as CompletionResult;
                        if (result != null)
                        {
                            responseData = result.Messages[0] as GetRoomResponse;

                        }
                    }, this);

            WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
            asyncWaitHandle.WaitOne(10000);

            if (asyncResult.IsCompleted)
            {
                this.WriteJson(AjaxResult.Succeed(responseData));
            }
        }

        /// <summary>
        /// 获取最新历史订单里用户信息
        /// </summary>
        [WebMethod]
        public void GetOrderLastUserInfo(int wid, string openid)
        {
            var orderUserDto = new OrderUserDto()
            {
                UserName = "jxiaox",
                UserIdcard = "360430199002050611",
                UserMobile = "18311300760"
            };
            this.WriteJson(AjaxResult.Succeed(orderUserDto));

        }

        /// <summary>
        /// 保存订单
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="openid"></param>
        /// <param name="hotelId"></param>
        /// <param name="roomId"></param>
        /// <param name="order"></param>
        [WebMethod]
        public void SaveOrder(int wid, string openid, int hotelId, int roomId, string order)
        {

            var orderDto = JSONHelper.Deserialize<OrderDto>(order);
            if (orderDto.Id > 0)
            {
                //update
            }
            else
            {
                //add
            }
            this.WriteJson(AjaxResult.Succeed(orderDto.Id));
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
            var orderDto = new OrderDto()
            {
                Id = orderId,
                OrderUser = new OrderUserDto()
                {
                    UserName = "jxiaox",
                    UserIdcard = "360430199002050611",
                    UserMobile = "18311300760"
                },
                OrderTime = DateTime.Now,
                ArriveTime = DateTime.Today,
                LeaveTime = DateTime.Today.AddDays(2),
                OrderNum = 2,
                Remark = "happy new year!",
                Status = 0
            };
            this.WriteJson(AjaxResult.Succeed(orderDto));
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
            var orderList = new List<OrderDto>();
            for (int i = 0; i < 9; i++)
            {
                var orderDto = new OrderDto()
                {
                    Id = i + 1,
                    OrderNumber = "H" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + Utils.Number(5),
                    OrderUser = new OrderUserDto()
                    {
                        UserName = "jxiaox",
                        UserIdcard = "360430199002050611",
                        UserMobile = "18311300760"
                    },
                    OrderTime = DateTime.Now,
                    ArriveTime = DateTime.Today.AddDays(i),
                    LeaveTime = DateTime.Today.AddDays(i + 2),
                    OrderNum = 2,
                    RoomId = i + 1,
                    HotelId = hotelId,
                    Remark = "happy new year!",
                    Status = i,
                    StatusName = "订单状态" + i,
                    OrderPrice = i * 3,
                    RoomType = "大床房"
                };
                orderList.Add(orderDto);
            }
            this.WriteJson(AjaxResult.Succeed(orderList));
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
            this.WriteJson(AjaxResult.Succeed(8));
        }
    }

    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public string Tel { get; set; }
        public string JieShao { get; set; }
    }

    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomType { get; set; }
        public List<RoomImgDto> RoomImgs { get; set; }
        public double CostPrice { get; set; }
        public double TotalPrice { get; set; }
        public string Detail { get; set; }
        public string Instruction { get; set; }
        public string RefundRule { get; set; }
    }

    public class RoomImgDto
    {

        public string Url { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// 订单
    /// </summary>
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public OrderUserDto OrderUser { get; set; }
        public DateTime ArriveTime { get; set; }
        public DateTime LeaveTime { get; set; }
        public DateTime OrderTime { get; set; }
        public int OrderNum { get; set; }
        public string Remark { get; set; }

        /// <summary>
        /// 最后订单的总金额
        /// </summary>
        public double OrderPrice { get; set; }

        public int Status { get; set; }
        public string StatusName { get; set; }

        //        //todo:这2个参数要加这么,还是换成对应的dto？
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public int RoomId { get; set; }
        public string RoomType { get; set; }
    }

    /// <summary>
    /// 订单用户信息
    /// </summary>
    public class OrderUserDto
    {
        public string UserName { get; set; }
        public string UserIdcard { get; set; }
        public string UserMobile { get; set; }
    }


}
