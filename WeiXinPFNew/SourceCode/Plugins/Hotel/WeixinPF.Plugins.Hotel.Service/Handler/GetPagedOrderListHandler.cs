using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Handler.Base;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetPagedOrderListHandler : BaseHandler, IHandleMessages<GetPagedOrderListRequest>
    {
        private readonly IBus _bus;

        public GetPagedOrderListHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(GetPagedOrderListRequest message)
        {
            var orderService = new HotelOrderService();
            var hotelService = new Application.Service.HotelService();
            int totalCount = 0;

            var orderList = orderService.GetList(message.PageSize, message.Page, message.Where, message.Orderby,
                out totalCount);

            if (orderList != null && orderList.Tables.Count > 0 && orderList.Tables[0].Rows.Count > 0)
            {
                orderList.Tables[0].Columns.Add("isRefund", typeof(string));

                orderList.Tables[0].Columns.Add("hotelName", typeof(string));
                orderList.Tables[0].Columns.Add("totalPrice", typeof(decimal));
                orderList.Tables[0].Columns.Add("statusName", typeof(string));
                orderList.Tables[0].Columns.Add("strisRefund", typeof(string));

                int count = orderList.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    var dr = orderList.Tables[0].Rows[i];

                    int id = dr.Field<int>("id");
                    int hotelId = dr.Field<int>("hotelId");
                    var status = HotelStatusManager.OrderStatus.GetStatusDict(
                        MyCommFun.Obj2Int(dr["orderStatus"]));
                    dr["payStatusStr"] = "<em  style='width:70px;' class='status " + status.CssClass
                        + "'>" + status.StatusName + "</em>";
                    dr["statusName"] = status.StatusName;
                    if (status.StatusId == HotelStatusManager.OrderStatus.Refunding.StatusId
                        || status.StatusId == HotelStatusManager.OrderStatus.Refunded.StatusId)
                    {
                        dr["isRefund"] = "<em  style='width:70px;' class='status ok'>是</em>";
                        dr["strisRefund"] = "是";
                    }
                    else if (status.StatusId == HotelStatusManager.OrderStatus.Completed.StatusId)
                    {
                        var refundOrder = new RefundOrderService().GetModel(o => o.OrderId == id && o.HotelId == hotelId);
                        if (refundOrder != null)
                        {
                            dr["isRefund"] = "<em  style='width:70px;' class='status ok'>是</em>";
                            dr["strisRefund"] = "是";
                        }
                        else
                        {
                            dr["isRefund"] = "<em  style='width:70px;' class='status no'>否</em>";
                            dr["strisRefund"] = "否";
                        }
                    }
                    else
                    {
                        dr["isRefund"] = "<em  style='width:70px;' class='status no'>否</em>";
                        dr["strisRefund"] = "否";

                    }
                    dr["hotelName"] = hotelService.GetModel(hotelId).hotelName;
                    //总花费
                    var dateSpan = dr.Field<DateTime>("leaveTime") - dr.Field<DateTime>("arriveTime");
                    var totalPrice = MyCommFun.Str2Decimal(dr["price"].ToString()) * dr.Field<int>("orderNum") * dateSpan.Days;
                    dr["totalPrice"] = totalPrice;
                }
                orderList.AcceptChanges();
            }

            var response = new GetPagedOrderListResponse()
            {
                TotalCount = totalCount,
                OrderList = orderList
            };

            _bus.Reply(response);
        }
    }
}
