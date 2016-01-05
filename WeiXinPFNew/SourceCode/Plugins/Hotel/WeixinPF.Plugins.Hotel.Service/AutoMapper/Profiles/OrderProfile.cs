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
    public class OrderProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<HotelOrderInfo, GetOrderResponse>()
                .ForMember(des => des.Id, source => source.MapFrom(s => s.id))
                .ForMember(des => des.OrderNumber, source => source.MapFrom(s => s.orderNumber))
                .ForMember(des => des.ArriveTime, source => source.MapFrom(s => s.id))
                .ForMember(des => des.LeaveTime, source => source.MapFrom(s => s.id))
                .ForMember(des => des.OrderTime, source => source.MapFrom(s => s.id))
                .ForMember(des => des.OrderNum, source => source.MapFrom(s => s.id))
                .ForMember(des => des.Remark, source => source.MapFrom(s => s.id))
                .ForMember(des => des.OrderPrice, source => source.MapFrom(s => s.id))
                .ForMember(des => des.Status, source => source.MapFrom(s => s.id))
                .ForMember(des => des.StatusName, source => source.MapFrom(s => s.id))
                .ForMember(des => des.HotelName, source => source.MapFrom(s => s.id))
                .ForMember(des => des.RoomId, source => source.MapFrom(s => s.id))
                .ForMember(des => des.RoomType, source => source.MapFrom(s => s.id))
                .AfterMap((source, des) => des.OrderUser = new OrderUserDto
                {
                    UserName = source.oderName,
                    UserIdcard = source.identityNumber,
                    UserMobile = source.tel
                });
        }
    }
}
