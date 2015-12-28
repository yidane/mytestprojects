using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Plugins.Hotel.Service.Models
{
    [Table("wx_hotel_dingdan")]
    public class HotelOrderInfo
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required]
        [Column("hotelid")]
        public int hotelid { get; set; }

        [Required, MaxLength(50)]
        [Column("openid")]
        public string openid { get; set; }

        /// <summary>
        /// 预定人
        /// </summary>
        [Required, MaxLength(50)]
        [Column("oderName")]
        public string oderName { get; set; }

        [Required, MaxLength(20)]
        [Column("tel")]
        public string tel { get; set; }

        [Column("arriveTime")]
        public DateTime arriveTime { get; set; }

        [Column("leaveTime")]
        public DateTime leaveTime { get; set; }

        [Required, MaxLength(50)]
        [Column("roomType")]
        public string roomType { get; set; }

        [Column("orderTime")]
        public DateTime orderTime { get; set; }

        /// <summary>
        /// 预订数量
        /// </summary>
        [Column("orderNum")]
        public int orderNum { get; set; }

        [Column("price")]
        public double price { get; set; }

        [Column("orderStatus")]
        public int orderStatus { get; set; }

        [Column("isDelete")]
        public int isDelete { get; set; }

        [Column("createDate")]
        public DateTime createDate { get; set; }

        [Column("roomid")]
        public int roomid { get; set; }

        [Column("yuanjia")]
        public double yuanjia { get; set; }

        [Required, MaxLength(1024)]
        [Column("remark")]
        public string remark { get; set; }

        [Required, MaxLength(50)]
        [Column("identityNumber")]
        public string identityNumber { get; set; }

        [Required, MaxLength(50)]
        [Column("wxOrderNumber")]
        public string wxOrderNumber { get; set; }

        /// <summary>
        /// 系统内部订单号
        /// </summary>
        [Required, MaxLength(50)]
        [Column("orderNumber")]
        public string orderNumber { get; set; }
    }
}
