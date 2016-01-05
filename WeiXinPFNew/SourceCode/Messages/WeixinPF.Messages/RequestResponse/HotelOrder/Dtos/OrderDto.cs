using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public OrderUserDto OrderUser { get; set; }
        public DateTime ArriveTime { get; set; }
        public DateTime LeaveTime { get; set; }
        public DateTime OrderTime { get; set; }
        public int OrderNum { get; set; }
        public string Remark { get; set; }
        public double OrderPrice { get; set; }

        public int Status { get; set; }
        public string StatusName { get; set; }

        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public int RoomId { get; set; }
        public string RoomType { get; set; }
    }
}
