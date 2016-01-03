using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace WeixinPF.Plugins.Hotel.Service.AutoMapper
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
