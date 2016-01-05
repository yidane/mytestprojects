using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetOrderListHandler : IHandleMessages<GetOrderListRequest>
    {
        private readonly IBus _bus;

        public GetOrderListHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(GetOrderListRequest message)
        {
            _bus.Reply(
                new HotelOrderService().GetOrderList(order => order.hotelid == message.HotelId
                ).MapTo<List<GetOrderResponse>>());
        }
    }
}
