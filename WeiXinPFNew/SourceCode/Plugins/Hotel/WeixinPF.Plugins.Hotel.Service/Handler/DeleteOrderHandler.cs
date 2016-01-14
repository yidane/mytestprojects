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
    public class DeleteOrderHandler : IHandleMessages<DeleteOrderRequest>
    {
        private readonly IBus _bus;

        public DeleteOrderHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(DeleteOrderRequest message)
        {
            var orderService = new HotelOrderService();

            if (message.OrderIds != null && message.OrderIds.Any())
            {
                for (var index = 0; index <= message.OrderIds.Count; index++)
                {

                }
            }
        }
    }
}
