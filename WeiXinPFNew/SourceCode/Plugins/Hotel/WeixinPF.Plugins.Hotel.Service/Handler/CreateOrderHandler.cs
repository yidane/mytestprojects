using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Handler.Base;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class CreateOrderHandler : BaseHandler, IHandleMessages<CreateOrderRequest>
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

            using (var scope = new TransactionScope())
            {
                if (entity.id == 0)
                {
                    entity.id = service.Add(entity);
                    //创建验证码
                    CreateIdentifyingCode(message.Wid, entity);
                }
                else
                {
                    service.Update(entity);
                }

                scope.Complete();
            }

            _bus.Reply(new CreateOrderResponse() { OrderId = entity.id });
        }


        private void CreateIdentifyingCode(int wid, HotelOrderInfo order)
        {
            for (var i = 0; i < order.orderNum; i++)
            {
                var iCode = new IdentifyingCodeInfo()
                {
                    IdentifyingCodeId = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    IdentifyingCode = string.Empty,
                    ModifyTime = DateTime.Now,
                    ModuleName = "hotel",
                    OrderCode = order.orderNumber,
                    OrderId = order.id.ToString(),
                    ProductCode = order.roomType,
                    ProductId = order.roomid.ToString(),
                    ShopId = order.hotelid.ToString(),
                    Wid = wid,
                    Status = 0
                };
                IdentifyingCodeService.AddIdentifyingCode(iCode);
            }
        }
    }
}
