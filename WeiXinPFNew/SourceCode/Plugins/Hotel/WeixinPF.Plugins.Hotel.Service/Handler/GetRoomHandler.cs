using System.Collections.Generic;
using AutoMapper;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Handler.Base;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetRoomHandler : BaseHandler, IHandleMessages<GetRoomRequest>
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
