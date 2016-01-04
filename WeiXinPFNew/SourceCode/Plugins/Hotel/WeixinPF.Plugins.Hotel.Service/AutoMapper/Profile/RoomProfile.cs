using AutoMapper;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.AutoMapper.Profile
{
    public class RoomProfile:global::AutoMapper.Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<RoomInfo, GetRoomResponse>()
                .ForMember(des => des.Id, source => source.MapFrom(s => s.id))
                .ForMember(des => des.Id, source => source.MapFrom(s => s.id))
                .ForMember(des => des.HotelId, source => source.MapFrom(s => s.hotelid))
                .ForMember(des => des.RoomType, source => source.MapFrom(s => s.roomType))
                .ForMember(des => des.Instruction, source => source.MapFrom(s => s.UseInstruction))
                .ForMember(des => des.CostPrice, source => source.MapFrom(s => s.roomPrice))
                .ForMember(des => des.TotalPrice, source => source.MapFrom(s => s.salePrice))
                .ForMember(des => des.Detail, source => source.MapFrom(s => s.facilities))
                .ForMember(des => des.RefundRule, source => source.MapFrom(s => s.RefundRule));

            Mapper.CreateMap<RoomPictureInfo, RoomPictureDto>()
                .ForMember(des => des.Name, source => source.MapFrom(s => s.title))
                .ForMember(des => des.Url, source => source.MapFrom(s => s.roomPic));
        }
    }
}
