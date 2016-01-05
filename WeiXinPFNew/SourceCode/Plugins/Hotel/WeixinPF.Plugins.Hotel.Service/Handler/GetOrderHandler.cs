using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetOrderHandler : IHandleMessages<GetOrderRequest>
    {
        public readonly IBus _bus;

        public GetOrderHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(GetOrderRequest message)
        {
            _bus.Reply(new HotelOrderService().GetOrderList(order => order.id == message.OrderId));
        }
    }
}
