using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetRoomResponse
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// 商家编号
        /// </summary>
        public int HotelId { set; get; }

        /// <summary>
        /// 房间类型
        /// </summary>
        public string RoomType { set; get; }

        /// <summary>
        /// 简要说明
        /// </summary>
        public string Instruction { set; get; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal CostPrice { set; get; }

        /// <summary>
        /// 优惠价
        /// </summary>
        public decimal TotalPrice { set; get; }

        /// <summary>
        /// 配套设施
        /// </summary>
        public string Detail { set; get; }

        /// <summary>
        /// 退单规则
        /// </summary>
        public string RefundRule { get; set; }

        public List<RoomPictureDto> RoomPictures { get; set; }
    }

    public class RoomPictureDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
