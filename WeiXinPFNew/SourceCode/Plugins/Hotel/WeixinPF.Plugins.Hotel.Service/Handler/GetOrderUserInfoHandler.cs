using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Messages.RequestResponse.Dtos;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetOrderUserInfoHandler : IHandleMessages<GetOrderUserInfoRequest>
    {
        public readonly IBus _bus;

        public GetOrderUserInfoHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(GetOrderUserInfoRequest message)
        {
            var service = new HotelOrderService();
            var order = service
                .GetModelList(string.Format("openid={0}",message.OpendId))
                .OrderByDescending(o => o.createDate)
                .FirstOrDefault();

            var user = new OrderUserDto()
            {
                UserIdcard = string.Empty,
                UserMobile = string.Empty,
                UserName = string.Empty
            };

            if (order != null)
            {
                user.UserName = order.oderName;
                user.UserMobile = order.tel;
                user.UserIdcard = order.identityNumber;
            }

            _bus.Reply(new GetOrderUserInfoResponse() { User = user });
        }
    }
}
