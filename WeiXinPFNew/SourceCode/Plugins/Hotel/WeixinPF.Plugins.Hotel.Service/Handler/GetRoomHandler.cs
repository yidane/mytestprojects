using AutoMapper;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse.Room;

namespace WeixinPF.Hotel.Plugins.Service.Handler
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
