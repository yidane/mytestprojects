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
using WeixinPF.Plugins.Hotel.Service.Infrastructure;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Handler
{
    public class GetRoomHandler : IHandleMessages<GetRoomRequest>
    {
        private readonly IBus _bus;

        public GetRoomHandler(IBus bus)
        {
            _bus = bus;
            Mapper.CreateMap<RoomInfo, GetRoomResponse>();
        }
        public void Handle(GetRoomRequest message)
        {
            var roomService = new RoomService();
            _bus.Reply(roomService.GetModel(message.RoomId).MapTo<GetRoomResponse>());
        }
    }
}
