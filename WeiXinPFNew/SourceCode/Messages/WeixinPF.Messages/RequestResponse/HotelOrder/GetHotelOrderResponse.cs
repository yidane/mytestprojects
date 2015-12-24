using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetHotelOrderResponse
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string OpenId { get; set; }

        /// <summary>
        /// 预定人
        /// </summary>
        public string OrderPersonName { get; set; }
        public string Tel { get; set; }
        public DateTime ArriveDate { get; set; }
        public DateTime LeaveDate { get; set; }
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
        public string Remark { get; set; }
        public string IdentityCode { get; set; }
        public string WXOrderNumber { get; set; }

        /// <summary>
        /// 系统内部订单号
        /// </summary>
        public string OrderNumber { get; set; }
    }
}
