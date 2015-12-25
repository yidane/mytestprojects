using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Plugins.Hotel.Service.Models
{
    /// <summary>
    /// 酒店信息
    /// </summary>
    [Table("wx_Hotel_HotelInfo")]
    public class HotelInfo
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 公众号Id
        /// </summary>
        public int Wid { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string HotelName { get; set; }
        /// <summary>
        /// 商家地址
        /// </summary>
        public string HotelAddress { get; set; }
        /// <summary>
        /// 商家电话
        /// </summary>
        public string HotelPhone { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 通知邮箱
        /// </summary>
        public string NoticeEmail { get; set; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string EmailPassword { get; set; }
        /// <summary>
        /// smtp服务器
        /// </summary>
        public string Smtp { get; set; }
        /// <summary>
        /// 封面图片
        /// </summary>
        public string CoverPic { get; set; }
        /// <summary>
        /// 订单页头部图片
        /// </summary>
        public string TopPic { get; set; }
        /// <summary>
        /// 每人每天提交订单次数
        /// </summary>
        public int? OrderLimit { get; set; }
        /// <summary>
        /// 列表模式
        /// </summary>
        public bool ListMode { get; set; }
        /// <summary>
        /// 短信提醒
        /// </summary>
        public int? MessageNotice { get; set; }
        /// <summary>
        /// 订单确认密码
        /// </summary>
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// 商家介绍
        /// </summary>
        public string HotelIntroduct { get; set; }
        /// <summary>
        /// 订房说明
        /// </summary>
        public string OrderRemark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SortId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Xplace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Yplace { get; set; }

        /// <summary>
        /// 房间编号
        /// </summary>
        public string HotelCode { get; set; }

        /// <summary>
        /// 运营人
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 星级
        /// </summary>
        public string HotelLevel { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool Recommend { get; set; }
    }
}
