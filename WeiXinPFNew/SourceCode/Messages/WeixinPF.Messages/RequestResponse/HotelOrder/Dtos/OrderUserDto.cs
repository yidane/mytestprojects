using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse.Dtos
{
    public class OrderUserDto
    {
        public string UserName { get; set; }
        public string UserIdcard { get; set; }
        public string UserMobile { get; set; }

        public static OrderUserDto Empty()
        {
            return new OrderUserDto()
            {
                UserName = string.Empty,
                UserIdcard = string.Empty,
                UserMobile = string.Empty,
            };
        }
    }
}
