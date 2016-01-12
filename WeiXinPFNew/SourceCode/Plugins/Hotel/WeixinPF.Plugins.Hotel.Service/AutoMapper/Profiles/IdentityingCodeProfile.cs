using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.AutoMapper.Profiles
{
    public class IdentityingCodeProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<IdentifyingCodeInfo, QrCodeDto>()
                .ForMember(des => des.Code, source => source.MapFrom(i => i.IdentifyingCode))
                .ForMember(des => des.Status, source => source.MapFrom(i => i.Status));
        }
    }
}
