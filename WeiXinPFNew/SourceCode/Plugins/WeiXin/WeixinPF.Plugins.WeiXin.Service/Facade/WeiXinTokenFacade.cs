using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Plugins.WeiXin.Service.Application;
using WeixinPF.Plugins.WeiXin.Service.Infrastructure;
using WeixinPF.Plugins.WeiXin.Service.Models.Common;

namespace WeixinPF.Plugins.WeiXin.Service.Facade
{
    public class WeiXinTokenFacade
    {
        private readonly WeiXinDbContext _dbContext = new WeiXinDbContext();
        public WeiXinJsApiTicket GetJsApiTicket(int appId)
        {
            var appRepository = new WeiXinAppRepository(_dbContext);
            var appInfo = appRepository.GetWeiXinAppInfo(appId);
            if (appInfo == null)
                throw new Exception(string.Format("系统中不存在ID为 {0} 的微信配置", appId));

            if (string.IsNullOrEmpty(appInfo.AppId) || string.IsNullOrEmpty(appInfo.AppSecret))
                throw new Exception(string.Format("系统中 {0} 的微信配置不完全，AppID或AppSecret为空.", appInfo.wxName));

            return null;
        }
    }
}
