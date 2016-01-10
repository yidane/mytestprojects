using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Plugins.WeiXin.Service.Models;
using WeixinPF.Plugins.WeiXin.Service.Models.Common;

namespace WeixinPF.Plugins.WeiXin.Service.Application.Repository
{
    public interface IWeiXinTicketRepository
    {
        WeiXinJsApiTicket GetJsApiTicket(WeiXinAppInfo appInfo);

        WeiXinAccessToken GetAccessToken(WeiXinAppInfo appInfo);
    }
}
