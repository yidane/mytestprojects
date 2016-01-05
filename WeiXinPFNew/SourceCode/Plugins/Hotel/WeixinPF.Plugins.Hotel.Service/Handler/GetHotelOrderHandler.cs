﻿using System;
using AutoMapper;
using NServiceBus;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetHotelOrderHandler : IHandleMessages<GetHotelOrderByOrderIdRequest>
    {
        private IBus bus;

        public GetHotelOrderHandler(IBus bus)
        {
            this.bus = bus;
            Mapper.CreateMap<HotelOrderInfo, GetHotelOrderResponse>()
                .ForMember(des => des.OrderPersonName, source => source.MapFrom(s => s.oderName))
                .ForMember(des => des.ArriveDate, source => source.MapFrom(s => s.arriveTime))
                .ForMember(des => des.LeaveDate, source => source.MapFrom(s => s.leaveTime))
                .ForMember(des => des.OrderDate, source => source.MapFrom(s => s.orderTime))
                .ForMember(des => des.IdentityCode, source => source.MapFrom(s => s.identityNumber))
                .ForMember(des => des.Price, source => source.MapFrom(s => s.price.ToString()))
                .ForMember(des => des.yuanjia, source => source.MapFrom(s => s.yuanjia.ToString()));
        }

        public void Handle(GetHotelOrderByOrderIdRequest message)
        {
            var info = new HotelOrderService().GetModel(message.OrderId);

            var response = TransformToResponse(info);

            bus.Reply(response);


        }

        private GetHotelOrderResponse TransformToResponse(HotelOrderInfo order)
        {
            return new GetHotelOrderResponse()
            {
                Id = order.id,
                HotelId = order.hotelid,
                OpenId = order.openid,
                OrderPersonName = order.oderName,
                Tel = order.tel,
                ArriveDate = order.arriveTime,
                LeaveDate = order.leaveTime,
                RoomType = order.roomType,
                OrderDate = order.orderTime,
                OrderNum = order.orderNum,
                Price = decimal.Parse(order.price.ToString()),
                OrderStatus = order.orderStatus,
                IsDelete = order.isDelete,
                CreateDate = order.createDate,
                RoomId = order.roomid,
                yuanjia = decimal.Parse(order.yuanjia.ToString()),
                Remark = order.remark,
                IdentityCode = order.identityNumber,
                WXOrderNumber = order.wxOrderNumber,
                OrderNumber = order.orderNumber
            };
        }
    }
}
