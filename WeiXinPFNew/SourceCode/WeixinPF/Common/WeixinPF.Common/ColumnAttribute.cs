using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeixinPF.Common
{
    public class ColumnAttribute : Attribute
    {
        public string ColumnName { get; set; }
    }
}
