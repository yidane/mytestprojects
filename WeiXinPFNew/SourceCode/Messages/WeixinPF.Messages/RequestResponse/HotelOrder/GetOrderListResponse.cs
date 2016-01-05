using System.Collections.Generic;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetOrderListResponse
    {
        public List<GetOrderResponse> Orders { get; set; }
    }
}
