using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Handler.Base;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class CreateOrderHandler :BaseHandler, IHandleMessages<CreateOrderRequest>
    {
        private readonly IBus _bus;

        public CreateOrderHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(CreateOrderRequest message)
        {
            var service = new HotelOrderService();

            var entity = message.Order.MapTo<HotelOrderInfo>();
            entity.hotelid = message.HotelId;
            entity.openid = message.OpenId;
            entity.roomid = message.RoomId;
            entity.roomType = message.RoomType;
            entity.createDate = DateTime.Now;
            entity.orderTime = DateTime.Now;
            entity.orderStatus = HotelStatusManager.OrderStatus.Pending.StatusId;
            entity.orderNumber = "H" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + Utils.Number(5);

            //获取房间的加个
            var room = new RoomService().GetModel(entity.roomid);
            entity.yuanjia = (double)room.roomPrice.Value;
            entity.price = (double)room.salePrice;

            int id;
            if (entity.id == 0)
            {
                 id = service.Add(entity);
            }
            else
            {
                service.Update(entity);
                id = entity.id;
            }

            _bus.Reply(new CreateOrderResponse() { OrderId = id });
        }
    }
}
