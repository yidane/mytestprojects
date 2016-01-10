using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeixinPF.Application.Weixin.User.Service;

namespace ApplicationTest
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void GetUserInfo()
        {
            var appId = 1;
            var openId = "obzTsw5qxlbwGYYZJC9b-91J-X1Y";
            UserService userService = new UserService();
            var result = userService.GetUserInfo(appId, openId);

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void GetOpenId()
        {
            var code = "0016f539a0f3689e71115826811e0b9M";
            var userService = new UserService();
            var openId = userService.GetOpenId(1, code);
        }
    }
}