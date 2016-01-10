using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Weixin;
using WeixinPF.Application.Weixin.Token.Repository;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Model;
using WeixinPF.Model.WeiXin;
using WeixinPF.Model.WeiXin.Token;

namespace WeixinPF.Infrastructure.Weixin.Token
{
    public class JsApiTicketRepository : IJsApiTicketRepository
    {
        private readonly EFRepository<JsApiTicket> _efRepository = new EFRepository<JsApiTicket>(new WeiXinDbContext());
        public JsApiTicket GetJsApiTicket(AppInfo appInfo)
        {
            var result = _efRepository.Get(item => item.AppId == appInfo.id && !item.Expired)
                                      .OrderByDescending(item => item.SysDateTime)
                                      .Take(1);

            return result.FirstOrDefault();
        }


        public void SetJsApiTicketExpire(JsApiTicket jsApiTicket)
        {
            if (jsApiTicket == null)
                return;

            jsApiTicket.Expired = true;
            jsApiTicket.ExpireDateTime = DateTime.Now;

            _efRepository.Update(jsApiTicket);
        }

        public void Add(JsApiTicket jsApiTicket)
        {
            if (jsApiTicket == null)
                return;

            _efRepository.Add(jsApiTicket);
        }
    }
}
