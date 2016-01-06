using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Hotel.Plugins.Service.AutoMapper;

namespace WeixinPF.Hotel.Plugins.Service.Handler.Base
{
    public class BaseHandler
    {
        static BaseHandler()
        {
            //zhuc
            AutoMapperConfiguration.Configure();
        }
    }
}
