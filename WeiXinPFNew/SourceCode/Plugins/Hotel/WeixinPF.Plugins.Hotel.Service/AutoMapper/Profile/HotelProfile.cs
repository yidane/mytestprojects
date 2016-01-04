using AutoMapper;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.AutoMapper.Profile
{
    public class HotelProfile :global::AutoMapper.Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<HotelInfo, GetHotelResponse>();
        }
    }
}
