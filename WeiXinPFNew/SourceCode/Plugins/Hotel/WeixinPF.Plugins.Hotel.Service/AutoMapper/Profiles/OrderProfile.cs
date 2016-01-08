using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WeixinPF.Common.Enum;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Messages.RequestResponse.Dtos;

namespace WeixinPF.Hotel.Plugins.Service.AutoMapper.Profiles
{
    public class OrderProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<HotelOrderInfo, OrderDto>()
                .ForMember(des => des.Id, source => source.MapFrom(s => s.id))
                .ForMember(des => des.OrderNumber, source => source.MapFrom(s => s.orderNumber))
                .ForMember(des => des.ArriveTime, source => source.MapFrom(s => s.arriveTime))
                .ForMember(des => des.LeaveTime, source => source.MapFrom(s => s.leaveTime))
                .ForMember(des => des.OrderTime, source => source.MapFrom(s => s.orderTime))
                .ForMember(des => des.OrderNum, source => source.MapFrom(s => s.orderNum))
                .ForMember(des => des.Remark, source => source.MapFrom(s => s.remark))
                .ForMember(des => des.OrderPrice, opt => opt.ResolveUsing<OrderPriceResolver>())
                .ForMember(des => des.Status, source => source.MapFrom(s => s.orderStatus))
                .ForMember(des => des.StatusName, source => source.MapFrom(s => HotelStatusManager.OrderStatus.GetStatusDict(s.orderStatus).StatusName))
                .ForMember(des => des.HotelName, source => source.MapFrom(s => s.HotelName))
                .ForMember(des => des.RoomId, source => source.MapFrom(s => s.roomid))
                .ForMember(des => des.RoomType, source => source.MapFrom(s => s.roomType))
                .ForMember(des => des.RoomPicture, source => source.MapFrom(s => s.RoomPicture))
                .AfterMap((source, des) => des.OrderUser = new OrderUserDto
                {
                    UserName = source.oderName,
                    UserIdcard = source.identityNumber,
                    UserMobile = source.tel
                });

            Mapper.CreateMap<OrderDto, HotelOrderInfo>()
                .ForMember(des => des.id, source => source.MapFrom(s => s.Id))
                .ForMember(des => des.orderNumber, source => source.MapFrom(s => s.OrderNumber))
                .ForMember(des => des.arriveTime, source => source.MapFrom(s => s.ArriveTime))
                .ForMember(des => des.leaveTime, source => source.MapFrom(s => s.LeaveTime))
                .ForMember(des => des.orderTime, source => source.MapFrom(s => s.OrderTime))
                .ForMember(des => des.orderNum, source => source.MapFrom(s => s.OrderNum))
                .ForMember(des => des.remark, source => source.MapFrom(s => s.Remark))
                .ForMember(des => des.orderStatus, source => source.MapFrom(s => s.Status))
                .ForMember(des => des.hotelid, source => source.MapFrom(s => s.HotelId))
                .ForMember(des => des.roomid, source => source.MapFrom(s => s.RoomId))
                .ForMember(des => des.roomType, source => source.MapFrom(s => s.RoomType))
                .ForMember(des => des.oderName, source => source.MapFrom(s => s.OrderUser.UserName))
                .ForMember(des => des.identityNumber, source => source.MapFrom(s => s.OrderUser.UserIdcard))
                .ForMember(des => des.tel, source => source.MapFrom(s => s.OrderUser.UserMobile));
        }
    }

    /// <summary>
    /// 订单总额计算
    /// </summary>
    public class OrderPriceResolver : ValueResolver<HotelOrderInfo, double>
    {
        protected override double ResolveCore(HotelOrderInfo source)
        {
            int days = (source.leaveTime - source.arriveTime).Days;

            return source.price * source.orderNum * days;
        }
    }
}
