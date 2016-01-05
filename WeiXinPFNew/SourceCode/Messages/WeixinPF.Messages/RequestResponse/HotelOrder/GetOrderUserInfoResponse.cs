using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Messages.RequestResponse.Dtos;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetOrderUserInfoResponse
    {
        public OrderUserDto User { get; set; }
    }
}
