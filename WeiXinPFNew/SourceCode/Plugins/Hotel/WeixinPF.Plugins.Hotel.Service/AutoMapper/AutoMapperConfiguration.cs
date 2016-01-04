using AutoMapper;
using WeixinPF.Hotel.Plugins.Service.AutoMapper.Profile;

namespace WeixinPF.Hotel.Plugins.Service.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<RoomProfile>();
                cfg.AddProfile<HotelProfile>();
            });
        }
    }
}
