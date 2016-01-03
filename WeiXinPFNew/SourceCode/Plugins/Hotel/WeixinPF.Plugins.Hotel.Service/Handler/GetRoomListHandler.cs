using System.Collections.Generic;
using AutoMapper;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse.Room;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetRoomListHandler : IHandleMessages<GetRoomListRequest>
    {
        private readonly IBus _bus;

        public GetRoomListHandler(IBus bus)
        {
            _bus = bus;
            Mapper.CreateMap<RoomInfo, GetRoomResponse>();
        }
        public void Handle(GetRoomListRequest message)
        {
            var roomService = new RoomService();
            var roomList = roomService.GetModelList(string.Format("HotelId={0}", message.HotelId));
            _bus.Reply(new GetRoomListResponse()
            {
                Rooms = roomList.MapTo<List<GetRoomResponse>>()
            });
        }
    }
}
