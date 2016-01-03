using AutoMapper;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Plugins.Hotel.Service.AutoMapper
{
    public class HotelProfile :Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<HotelInfo, GetHotelResponse>();
        }
    }
}
