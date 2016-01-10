using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Handler.Base;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Messages.RequestResponse.Dtos;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetOrderCountHandler : BaseHandler, IHandleMessages<GetOrderCountRequest>
    {
        private readonly IBus _bus;

        public GetOrderCountHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(GetOrderCountRequest message)
        {
            var service = new HotelOrderService();

            string strWhere = "1=1 ";
            if (!string.IsNullOrEmpty(message.OpenId))
            {
                strWhere += string.Format(" And OpenId='{0}' ", message.OpenId);
            }

            if (message.HotelId > 0)
            {
                strWhere += string.Format(" And hotelid={0}", message.HotelId);
            }
            var orderDtos = service.GetModelList(strWhere).MapTo<List<OrderDto>>();

            _bus.Reply(new GetOrderCountResponse() { Count = orderDtos.Count });
        }
    }
}
