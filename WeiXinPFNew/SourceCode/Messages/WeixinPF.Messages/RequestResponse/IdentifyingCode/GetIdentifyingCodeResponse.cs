using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetIdentifyingCodeResponse
    {
        public Guid IdentifyingCodeId { get; set; }

        public int Wid { get; set; }

        public string ShopId { get; set; }

        public string ModuleName { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单编码
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 单件商品的Id
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 单件商品的编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 商品识别码
        /// </summary>
        public string IdentifyingCode { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ModifyTime { get; set; }

        public int Status { get; set; }
    }
}
