using System.Collections.Generic;
using AutoMapper;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Plugins.Hotel.Service.Application.Service;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetRoomHandler : IHandleMessages<GetRoomRequest>
    {
        private readonly IBus _bus;

        public GetRoomHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(GetRoomRequest message)
        {
            var roomService = new RoomService();
            var roomPictureService = new RoomPictureService();

            var room = roomService.GetModel(message.RoomId);
            var roomPictures = roomPictureService.GetModelList(string.Format("RoomId={0}", message.RoomId));

            var response = room.MapTo<GetRoomResponse>();
            response.RoomPictures = roomPictures.MapTo<List<RoomPictureDto>>();
            _bus.Reply(response);
        }
    }
}
