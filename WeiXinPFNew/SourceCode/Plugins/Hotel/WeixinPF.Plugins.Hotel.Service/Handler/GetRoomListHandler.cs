using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetRoomListHandler : IHandleMessages<GetRoomListRequest>
    {
        private readonly IBus _bus;

        public GetRoomListHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(GetRoomListRequest message)
        {

            var roomService = new RoomService();
            var roomPictureService = new RoomPictureService();

            var roomList = roomService.GetModelList(string.Format("HotelId={0}", message.HotelId)).MapTo<List<GetRoomResponse>>();

            if (roomList.Any())
            {
                roomList.ForEach(r =>
                {
                    r.RoomPictures = roomPictureService.GetModelList(string.Format("RoomId={0}", r.Id)).MapTo<List<RoomPictureDto>>();
                });
            }
            _bus.Reply(new GetRoomListResponse()
            {
                Rooms = roomList
            });
        }
    }
}
