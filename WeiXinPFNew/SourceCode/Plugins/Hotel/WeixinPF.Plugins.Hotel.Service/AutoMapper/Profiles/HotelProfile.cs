using AutoMapper;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.AutoMapper.Profiles
{
    public class HotelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<HotelInfo, GetHotelResponse>()
                .ForMember(dest => dest.Id, source => source.MapFrom(h => h.id))
                .ForMember(dest => dest.Name, source => source.MapFrom(h => h.hotelName))
                .ForMember(dest => dest.Address, source => source.MapFrom(h => h.hotelAddress))
                .ForMember(dest => dest.Tel, source => source.MapFrom(h => h.hotelPhone))
                .ForMember(dest => dest.Introduction, source => source.MapFrom(h => h.hotelIntroduct))
                .ForMember(dest => dest.CoverSrc, source => source.MapFrom(h => h.coverPic));
        }
    }
}
