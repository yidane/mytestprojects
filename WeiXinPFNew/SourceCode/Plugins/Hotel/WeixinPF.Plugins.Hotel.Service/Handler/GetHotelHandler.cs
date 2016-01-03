using AutoMapper;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Infrastructure;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetHotelHandler : IHandleMessages<GetHotelRequest>
    {
        private readonly IBus _bus;

        public GetHotelHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(GetHotelRequest message)
        {
            var hotelService = new Application.Service.HotelService(new HotelRepository());
            var entity = hotelService.GetModel(message.HotelId);

            _bus.Reply(entity.MapTo<GetHotelResponse>());

        }
    }
}
