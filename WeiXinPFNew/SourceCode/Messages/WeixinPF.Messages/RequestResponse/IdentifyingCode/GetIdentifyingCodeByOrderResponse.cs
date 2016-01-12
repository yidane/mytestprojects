using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetIdentifyingCodeByOrderResponse
    {
        public List<QrCodeDto> Codes { get; set; }
    }


    public class QrCodeDto
    {
        public string Code { get; set; }

        public int Status { get; set; }
    }
}
