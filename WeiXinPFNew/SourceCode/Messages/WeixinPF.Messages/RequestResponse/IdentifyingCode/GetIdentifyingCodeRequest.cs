using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetIdentifyingCodeRequest
    {
        public int Wid { get; set; }
        public int ShopId { get; set; }
        public string Number { get; set; }
        public string ModuleName { get; set; }
    }
}
