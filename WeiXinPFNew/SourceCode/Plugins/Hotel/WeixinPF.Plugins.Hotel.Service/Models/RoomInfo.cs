using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WeixinPF.Plugins.Hotel.Service.Models
{
    [Table("wx_hotel_room")]
    public class RoomInfo
    {
        #region Model

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

        /// <summary>
        /// 状态
        /// </summary>
        public RoomStatus Status { get; set; }

        public DateTime? ExpiryDate_Begin { get; set; }
        public DateTime? ExpiryDate_End { get; set; }
        #endregion Model

    }

    public enum RoomStatus
    {
        /// <summary>
        /// 状态不确定
        /// </summary>
        None = 0,
        /// <summary>
        /// 提交审核
        /// </summary>
        Submit = 1,
        /// <summary>
        /// 审核通过
        /// </summary>
        Agree = 2,

        /// <summary>
        /// 审核不通过
        /// </summary>
        Refuse = 3,

        /// <summary>
        ///  发布
        /// </summary>
        Publish = 4,

        /// <summary>
        /// 下架
        /// </summary>
        SoldOut = 5
    }
}
