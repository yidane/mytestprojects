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
        public int Id { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required, MaxLength(50)]
        public string OpenId { get; set; }

        /// <summary>
        /// 预定人
        /// </summary>
        [Required, MaxLength(50)]
        public string OrderPersonName { get; set; }

        [Required, MaxLength(20)]
        public string Tel { get; set; }
        public DateTime ArriveDate { get; set; }
        public DateTime LeaveDate { get; set; }

        [Required, MaxLength(50)]
        public string RoomType { get; set; }
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 预订数量
        /// </summary>
        public int OrderNum { get; set; }

        public decimal Price { get; set; }
        public int OrderStatus { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
        public int RoomId { get; set; }
        public decimal yuanjia { get; set; }

        [Required, MaxLength(1024)]
        public string Remark { get; set; }

        [Required, MaxLength(50)]
        public string IdentityCode { get; set; }

        [Required, MaxLength(50)]
        public string WXOrderNumber { get; set; }

        /// <summary>
        /// 系统内部订单号
        /// </summary>
        [Required, MaxLength(50)]
        public string OrderNumber { get; set; }
    }
}
