using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse.Room
{
    public class GetRoomResponse
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 商家编号
        /// </summary>
        public int? hotelid { set; get; }

        /// <summary>
        /// 房间类型
        /// </summary>
        public string roomType { set; get; }

        /// <summary>
        /// 简要说明
        /// </summary>
        public string indroduce { set; get; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal? roomPrice { set; get; }

        /// <summary>
        /// 优惠价
        /// </summary>
        public decimal? salePrice { set; get; }

        /// <summary>
        /// 配套设施
        /// </summary>
        public string facilities { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? createDate { set; get; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int? sortid { set; get; }

        /// <summary>
        /// 商户编号
        /// </summary>
        public string RoomCode { get; set; }

        /// <summary>
        /// 使用说明
        /// </summary>
        public string UseInstruction { get; set; }

        /// <summary>
        /// 退单规则
        /// </summary>
        public string RefundRule { get; set; }
    }
}
