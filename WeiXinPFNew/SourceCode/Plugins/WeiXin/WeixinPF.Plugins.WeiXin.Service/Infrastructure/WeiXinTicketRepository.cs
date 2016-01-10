using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneGulp.WeChat.MP.CommonAPIs;
using OneGulp.WeChat.MP.Entities;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Plugins.WeiXin.Service.Application.Repository;
using WeixinPF.Plugins.WeiXin.Service.Models;
using WeixinPF.Plugins.WeiXin.Service.Models.Common;

namespace WeixinPF.Plugins.WeiXin.Service.Infrastructure
{
    public class WeiXinTicketRepository : EFRepository<WeiXinAccessToken>, IWeiXinTicketRepository
    {
        private DbContext _context;
        public WeiXinTicketRepository(DbContext dbContext)
            : base(dbContext)
        {
            this._context = dbContext;
        }

        public WeiXinJsApiTicket GetJsApiTicket(WeiXinAppInfo appInfo)
        {
            return null;
        }

        public WeiXinAccessToken GetAccessToken(WeiXinAppInfo appInfo)
        {
            if (string.IsNullOrEmpty(appInfo.AppId) || string.IsNullOrEmpty(appInfo.AppSecret))
                throw new Exception(string.Format("系统中 {0} 的微信配置不完全，AppID或AppSecret为空.", appInfo.wxName));

            //获取缓存的Access_Token
            var accessTokenList = this.Get(item => !item.Expired);
            if (accessTokenList.Any(item => !item.IsExpired()))
            {
                return accessTokenList.FirstOrDefault(item => !item.IsExpired());
            }
            else
            {
                if (accessTokenList.Count > 0)
                {
                    //this.Update(accessTokenList.ToList().SelectMany<WeiXinAccessToken,WeiXinAccessToken>());
                }

                //数据库不存在相应的AccessToken，则调用服务，再次获取
                var newAccessToken = new WeiXinAccessToken();
                newAccessToken.AppId = appInfo.id;
                newAccessToken.Id = Guid.NewGuid();
                newAccessToken.Expired = false;
                AccessTokenResult result = CommonApi.GetToken(appInfo.AppId, appInfo.AppSecret);
                newAccessToken.SysDateTime = DateTime.Now;
                newAccessToken.Ticket = result.access_token;
                newAccessToken.Expires = result.expires_in;
            }

            return null;
        }
    }
}
