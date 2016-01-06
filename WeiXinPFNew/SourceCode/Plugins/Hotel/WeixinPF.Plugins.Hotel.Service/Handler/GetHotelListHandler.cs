using System.Collections.Generic;
using AutoMapper;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Handler.Base;
using WeixinPF.Hotel.Plugins.Service.Infrastructure;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetHotelListHandler : BaseHandler, IHandleMessages<GetHotelListRequest>
    {
        private readonly IBus _bus;

        public GetHotelListHandler(IBus bus)
        {
            _bus = bus;
           // Mapper.CreateMap<GetHotelResponse, HotelInfo>();
        }
        public void Handle(GetHotelListRequest message)
        {
            var hotelService = new Application.Service.HotelService(new HotelRepository());

            var entityList = hotelService.GetModelList(string.Format("wid={0}", message.Wid));

            _bus.Reply(new GetHotelListResponse() { Hotels = entityList.MapTo<List<GetHotelResponse>>() });

        }
    }
}
