using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetOrderRequest
    {
        public int Wid { get; set; }
        public string OpenId { get; set; }
        public int OrderId { get; set; }
    }
}
