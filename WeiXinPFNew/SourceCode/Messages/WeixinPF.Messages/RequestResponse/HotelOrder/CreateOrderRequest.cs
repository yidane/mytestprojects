using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Messages.RequestResponse.Dtos;

namespace WeixinPF.Messages.RequestResponse
{
    public class CreateOrderRequest
    {
        public int Wid { get; set; }
        public string OpenId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }

        public string RoomType { get; set; }
        public OrderDto Order { get; set; }
    }
}
