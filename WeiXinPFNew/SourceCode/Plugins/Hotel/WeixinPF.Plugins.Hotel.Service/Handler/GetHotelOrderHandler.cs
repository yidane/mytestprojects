using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Unicast;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Plugins.Hotel.Service.Application.Service;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Handler
{
    public class GetHotelOrderHandler: IHandleMessages<GetHotelOrderByOrderIdRequest>
    {
        private IBus bus;

        public GetHotelOrderHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(GetHotelOrderByOrderIdRequest message)
        {
            var info = HotelOrderService.GetOrderInfo(message.OrderId);

            var response = TransformToResponse(info);

            bus.Reply(response);
        }

        private GetHotelOrderResponse TransformToResponse(HotelOrderInfo order)
        {
            return new GetHotelOrderResponse()
                    {
                        Id = order.id,
                        HotelId = order.hotelid,
                        OpenId = order.openid,
                        OrderPersonName = order.oderName,
                        Tel = order.tel,
                        ArriveDate = order.arriveTime,
                        LeaveDate = order.leaveTime,
                        RoomType = order.roomType,
                        OrderDate = order.orderTime,
                        OrderNum = order.orderNum,
                        Price = decimal.Parse(order.price.ToString()) ,
                        OrderStatus = order.orderStatus,
                        IsDelete = order.isDelete,
                        CreateDate = order.createDate,
                        RoomId = order.roomid,
                        yuanjia = decimal.Parse(order.yuanjia.ToString()) ,
                        Remark = order.remark,
                        IdentityCode = order.identityNumber,
                        WXOrderNumber = order.wxOrderNumber,
                        OrderNumber = order.orderNumber
                    };
        }
    }
}
