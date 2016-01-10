using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeixinPF.Application;
using WeixinPF.Application.Weixin.Message.Repository;
using WeixinPF.Application.Weixin.User.Repository;
using WeixinPF.Common;
using WeixinPF.Common.Helper;

namespace WeiXinUnitTest
{
    [TestClass]
    public class DependencyManagerTest
    {
        [TestMethod]
        public void TestResolve()
        {
            var result = DependencyManager.Resolve<IUserRepository>();
        }


        [TestMethod]
        public void ShiZongTest()
        {
            var json = "{ \"total\": 9, \"count\": 9, \"data\": { \"openid\": [\"o4DoCwR3j0bLF0kEev3DsybnvxT8\", \"o4DoCwQuTLOxH182vGcm0Z_bgftE\", \"o4DoCwdGJyjbZhw3s6zbBvJ_RUy0\", \"o4DoCwY8KM3bcLd8-o-2wxZCbZwg\", \"o4DoCwXW2Ny-PE6nhe-4khelhWO8\", \"o4DoCwYmmtGdq3zCjqzklSM1Ct1I\", \"o4DoCwTqFGcJv4mJ9e4cU_BEVXr4\", \"o4DoCwW1r9F9ObIFuQEo6elJcrSU\", \"o4DoCwXy6BSB9krklwbnowSOzBk8\"] }, \"next_openid\": \"o4DoCwXy6BSB9krklwbnowSOzBk8\" }";

            var result = JSONHelper.Deserialize<WeiXinObject>(json);

        }
    }

    public class WeiXinObject
    {
        public int total { get; set; }
        public int count { get; set; }
        public data data { get; set; }

        public string next_openid { get; set; }
    }

    public class data
    {
        public List<string> openid { get; set; }
    }
}
