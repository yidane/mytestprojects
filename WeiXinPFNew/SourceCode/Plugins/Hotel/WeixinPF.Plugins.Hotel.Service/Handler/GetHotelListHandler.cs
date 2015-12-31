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
    public class GetHotelListHandler : IHandleMessages<GetHotelListRequest>
    {
        private readonly IBus _bus;

        public GetHotelListHandler(IBus bus)
        {
            _bus = bus;
            Mapper.CreateMap<GetHotelResponse, HotelInfo>();
        }
        public void Handle(GetHotelListRequest message)
        {
            var hotelService = new Application.Service.HotelService(new HotelRepository());

            var entityList = hotelService.GetModelList(string.Format("wid={0}", message.Wid));

            _bus.Reply(new GetHotelListResponse() { Hotels = entityList.MapTo<List<GetHotelResponse>>() });

        }
    }
}
