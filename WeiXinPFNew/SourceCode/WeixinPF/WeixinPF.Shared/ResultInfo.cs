using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Shared
{
    public class ResultInfo<TResult>
    {
        public bool IsSuccess { get; set; }
        public TResult Data { get; set; }

        public string Message { get; set; }
    }
}
