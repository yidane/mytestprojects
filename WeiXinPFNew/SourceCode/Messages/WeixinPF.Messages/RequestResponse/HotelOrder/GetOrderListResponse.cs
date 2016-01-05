using System.Collections.Generic;
using WeixinPF.Messages.RequestResponse.Dtos;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetOrderListResponse
    {
        public List<OrderDto> Orders { get; set; }
    }
}
