using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Model.WeiXin;
using WeixinPF.Model.WeiXin.Token;

namespace WeixinPF.Application.Weixin.Token.Repository
{
    public interface IJsApiTicketRepository
    {
        JsApiTicket GetJsApiTicket(AppInfo appInfo);
        void SetJsApiTicketExpire(JsApiTicket jsApiTicket);
        void Add(JsApiTicket jsApiTicket);
    }
}
