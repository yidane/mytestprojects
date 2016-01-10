using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Plugins.WeiXin.Service.Application.Repository;
using WeixinPF.Plugins.WeiXin.Service.Models;
using WeixinPF.Plugins.WeiXin.Service.Models.Common;

namespace WeixinPF.Plugins.WeiXin.Service.Application.Service
{
    public class TicketRepository : IWeiXinTicketRepository
    {
        public WeiXinJsApiTicket GetJsApiTicket(WeiXinAppInfo appInfo)
        {
            return null;
        }

        public WeiXinAccessToken GetAccessToken(WeiXinAppInfo appInfo)
        {
            return null;
        }
    }
}