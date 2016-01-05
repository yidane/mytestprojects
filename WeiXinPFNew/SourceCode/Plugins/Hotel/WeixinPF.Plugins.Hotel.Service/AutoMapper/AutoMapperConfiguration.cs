using AutoMapper;
using WeixinPF.Hotel.Plugins.Service.AutoMapper.Profiles;

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
                cfg.AddProfile<OrderProfile>();
            });
        }
    }
}
