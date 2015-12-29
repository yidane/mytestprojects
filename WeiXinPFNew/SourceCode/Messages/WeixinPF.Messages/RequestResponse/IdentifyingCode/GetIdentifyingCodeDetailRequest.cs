using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetIdentifyingCodeDetailRequest
    {
        public Guid IdentifyingCodeId { get; set; }
        public string ModuleName { get; set; }
    }
}
