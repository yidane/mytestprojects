using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetIdentifyingCodeDetailResponse
    {
        public List<IdentifyingCodeDetailEntity> Details { get; set; }
    }

    public class IdentifyingCodeDetailEntity
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Status { get; set; }

        public int Number { get; set; }

        public string LeaveTime { get; set; }

        public string ArriveTime { get; set; }

        public double TotelPrice { get; set; }
    }
}
