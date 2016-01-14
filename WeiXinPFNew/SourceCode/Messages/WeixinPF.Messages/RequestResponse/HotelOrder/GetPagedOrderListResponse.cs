using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Messages.RequestResponse.Dtos;

namespace WeixinPF.Messages.RequestResponse
{
    public class GetPagedOrderListResponse
    {
        public int TotalCount { get; set; }
        public DataSet OrderList { get; set; }
    }
}
