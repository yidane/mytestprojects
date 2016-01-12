using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Common.Enum;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class CancelOrderHandler : IHandleMessages<CancelOrderRequest>
    {
        public IBus _bus;

        public CancelOrderHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(CancelOrderRequest message)
        {
            var response = new CancelOrderResponse();
            var service = new HotelOrderService();
            try
            {
                var order = service.GetModel(message.OrderId);
                if (CheckStatus(order))
                {
                    response.Success = service.Update(message.OrderId,
                        HotelStatusManager.OrderStatus.Cancelled.StatusId.ToString());
                }
                else
                {
                    response.Success = false;
                }
            }
            catch
            {
                response.Success = false;
            }

            _bus.Reply(response);
        }

        private static bool CheckStatus(HotelOrderInfo order)
        {
            if (order == null)
            {
                return false;
            }

            return order.orderStatus != HotelStatusManager.OrderStatus.Payed.StatusId && order.orderStatus != HotelStatusManager.OrderStatus.Completed.StatusId;
        }

    }
}
