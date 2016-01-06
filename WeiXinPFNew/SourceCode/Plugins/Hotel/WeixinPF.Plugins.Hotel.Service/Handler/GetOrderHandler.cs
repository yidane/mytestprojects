using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Messages.RequestResponse.Dtos;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetOrderHandler : IHandleMessages<GetOrderRequest>
    {
        private readonly IBus _bus;

        public GetOrderHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(GetOrderRequest message)
        {
            var service = new HotelOrderService();
            var order = service.GetModel(message.OrderId);

            if (order == null)
            {
                throw new Exception("订单不存在或者已删除。");
            }

            _bus.Reply(new GetOrderResponse() { Order = order.MapTo<OrderDto>() });
        }
    }
}
