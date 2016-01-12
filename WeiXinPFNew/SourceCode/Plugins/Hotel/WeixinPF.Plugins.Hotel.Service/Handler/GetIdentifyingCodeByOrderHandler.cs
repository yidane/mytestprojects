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
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetIdentifyingCodeByOrderHandler : BaseHandler, IHandleMessages<GetIdentifyingCodeByOrderRequest>
    {
        private readonly IBus _bus;

        public GetIdentifyingCodeByOrderHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(GetIdentifyingCodeByOrderRequest message)
        {
            var order = new HotelOrderService().GetModel(message.OrderId);
            var response = new GetIdentifyingCodeByOrderResponse();
            if (order != null)
            {
                if (order.orderStatus == HotelStatusManager.OrderStatus.Payed.StatusId
                    || order.orderStatus == HotelStatusManager.OrderStatus.Refunded.StatusId
                    || order.orderStatus == HotelStatusManager.OrderStatus.Refunding.StatusId
                    )
                {
                    response.Codes =
                        IdentifyingCodeService
                        .GetIdentifyingCodeByOrder(message.OrderId, "Hotel")
                        .MapTo<List<QrCodeDto>>();
                }
            }

            _bus.Reply(response);
        }
    }
}

