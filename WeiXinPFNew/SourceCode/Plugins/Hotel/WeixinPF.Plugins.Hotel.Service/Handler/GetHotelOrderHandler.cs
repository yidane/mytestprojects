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
                        Id = order.Id,
                        HotelId = order.HotelId,
                        OpenId = order.OpenId,
                        OrderPersonName = order.OrderPersonName,
                        Tel = order.Tel,
                        ArriveDate = order.ArriveDate,
                        LeaveDate = order.LeaveDate,
                        RoomType = order.RoomType,
                        OrderDate = order.OrderDate,
                        OrderNum = order.OrderNum,
                        Price = order.Price,
                        OrderStatus = order.OrderStatus,
                        IsDelete = order.IsDelete,
                        CreateDate = order.CreateDate,
                        RoomId = order.RoomId,
                        yuanjia = order.yuanjia,
                        Remark = order.Remark,
                        IdentityCode = order.IdentityCode,
                        WXOrderNumber = order.WXOrderNumber,
                        OrderNumber = order.OrderNumber
                    };
        }
    }
}
