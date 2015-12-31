using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Plugins.Hotel.Service.Infrastructure;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Handler
{
    public class GetHotelHandler : IHandleMessages<GetHotelRequest>
    {
        private readonly IBus _bus;

        public GetHotelHandler(IBus bus)
        {
            _bus = bus;
            Mapper.CreateMap<HotelInfo, GetHotelResponse>();
        }
        public void Handle(GetHotelRequest message)
        {
            var hotelService = new Application.Service.HotelService(new HotelRepository());
            var entity = hotelService.GetModel(message.HotelId);

            _bus.Reply(entity.MapTo<GetHotelResponse>());

        }
    }
}
