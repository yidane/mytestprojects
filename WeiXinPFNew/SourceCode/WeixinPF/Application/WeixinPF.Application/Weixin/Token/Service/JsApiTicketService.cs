using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Application.Weixin.Token.Repository;
using WeixinPF.Common;
using WeixinPF.Model.WeiXin.Token;

namespace WeixinPF.Application.Weixin.Token.Service
{
    public class JsApiTicketService
    {
        private readonly IJsApiTicketRepository _jsApiTicketRepository = null;
        private readonly IAppInfoRepository _appInfoRepository = null;
        private static readonly object Obj = new object();

        public JsApiTicketService()
        {
            _jsApiTicketRepository = DependencyManager.Resolve<IJsApiTicketRepository>();
            _appInfoRepository = DependencyManager.Resolve<IAppInfoRepository>();
        }

        public JsApiTicket GetJsApiTicket(int wid, out string errorMessage)
        {
            lock (Obj)
            {
                try
                {
                    var appInfo = _appInfoRepository.GetAppInfo(wid);
                    if (appInfo == null)
                    {
                        errorMessage = string.Format("当前系统无wid为{0}相应公众号的配置", wid);
                        return null;
                    }
                    if (string.IsNullOrEmpty(appInfo.AppId) || string.IsNullOrEmpty(appInfo.AppSecret))
                    {
                        errorMessage = string.Format("当前系统wid为{0}相应公众号的配置不完整,AppId或者AppSecret未填写完全,请在[我的公众帐号]里补全信息！", wid);
                        return null;
                    }

                    var jsApiTicketFromDb = _jsApiTicketRepository.GetJsApiTicket(appInfo);

                    //判断是否存在或者是否过期
                    if (jsApiTicketFromDb == null || jsApiTicketFromDb.IsExpired())
                    {
                        //更改已过期的数据
                        _jsApiTicketRepository.SetJsApiTicketExpire(jsApiTicketFromDb);

                        //重新从微信申请ticket
                        string tokenErrorMessage;
                        var token = new AccessTokenService().GetAccessToken(wid, out tokenErrorMessage);
                        if (token == null || !string.IsNullOrEmpty(tokenErrorMessage))
                            throw new Exception(string.Format("获取JsApiTicket失败：{0}", tokenErrorMessage));

                        var result = OneGulp.WeChat.MP.CommonAPIs.CommonApi.GetTicket(token.Ticket);
                        var newJsApiTicket = new JsApiTicket()
                        {
                            Id = Guid.NewGuid(),
                            AppId = wid,
                            Ticket = result.ticket,
                            Expires = result.expires_in,
                            SysDateTime = DateTime.Now
                        };

                        _jsApiTicketRepository.Add(newJsApiTicket);

                        errorMessage = string.Empty;
                        return newJsApiTicket;
                    }

                    errorMessage = string.Empty;
                    return jsApiTicketFromDb;
                }
                catch (Exception exception)
                {
                    errorMessage = exception.Message;
                    return null;
                }
            }
        }
    }
}
