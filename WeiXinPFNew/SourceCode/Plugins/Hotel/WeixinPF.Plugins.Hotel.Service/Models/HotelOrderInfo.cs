using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Hotel.Plugins.Service.Models
{
    [Table("wx_hotel_dingdan")]
    public class HotelOrderInfo
    {
        public int id { get; set; }
        public int hotelid { get; set; }
        public string openid { get; set; }

        /// <summary>
        /// 预定人
        /// </summary>
        public string oderName { get; set; }

        public string tel { get; set; }

        public DateTime arriveTime { get; set; }

        public DateTime leaveTime { get; set; }

        public string roomType { get; set; }

        public DateTime orderTime { get; set; }

        /// <summary>
        /// 预订数量
        /// </summary>
        public int orderNum { get; set; }

        public double price { get; set; }

        public int orderStatus { get; set; }

        public int isDelete { get; set; }

        public DateTime createDate { get; set; }

        public int roomid { get; set; }

        public double yuanjia { get; set; }

        public string remark { get; set; }

        public string identityNumber { get; set; }

        public string wxOrderNumber { get; set; }

        /// <summary>
        /// 系统内部订单号
        /// </summary>
        public string orderNumber { get; set; }

        public string HotelName { get; set; }
        public string RoomPicture { get; set; }
    }
}
