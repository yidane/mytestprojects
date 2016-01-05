using AutoMapper;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.AutoMapper.Profiles
{
    public class HotelProfile :Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<HotelInfo, GetHotelResponse>();
        }
    }
}
