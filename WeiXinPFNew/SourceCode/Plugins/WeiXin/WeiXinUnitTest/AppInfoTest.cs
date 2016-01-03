using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeixinPF.Plugins.WeiXin.Service.Application;
using WeixinPF.Plugins.WeiXin.Service.Application.Service;
using WeixinPF.Plugins.WeiXin.Service.Infrastructure;

namespace WeiXinUnitTest
{
    [TestClass]
    public class AppInfoTest
    {
        [TestMethod]
        public void TestGetAllAPPInfoList()
        {
            using (WeiXinDbContext context = new WeiXinDbContext())
            {
                WeiXinAppService service = new WeiXinAppService(new WeiXinAppRepository(context));
                var result = service.GetAppInfoList();
            }
        }

        [TestMethod]
        public void AddAppInfo()
        {
            using (WeiXinDbContext context = new WeiXinDbContext())
            {
                WeiXinAppService service = new WeiXinAppService(new WeiXinAppRepository(context));
                var result = service.GetAppInfoList();
            }
        }
    }
}
