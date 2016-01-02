using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Messages.RequestResponse.Room;
using WeixinPF.Plugins.Hotel.Service.Application.Service;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Handler
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
