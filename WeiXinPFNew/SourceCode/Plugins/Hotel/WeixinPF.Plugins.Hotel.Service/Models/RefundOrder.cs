using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Hotel.Plugins.Service.Models
{
    [Table("wx_hotel_tuidan_manage")]
    public class RefundOrder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }

        [Column("dingdanid")]
        public int OrderId { get; set; }

        [Column("hotelid")]
        public int HotelId { get; set; }

        [Column("openid")]
        public string OpendId { get; set; }

        [Column("wid")]
        public int Wid { get; set; }

        [Column("roomid")]
        public int RoomId { get; set; }

        [Column("refundTime")]
        public DateTime RefundTime { get; set; }

        [Column("refundAmount")]
        public double RefundAmount { get; set; }

        [Column("operateUser")]
        public int OperateUser { get; set; }

        [Column("remarks")]
        public string Remarks { get; set; }

        [Column("refundCode")]
        public string RefundCode { get; set; }

    }
}
