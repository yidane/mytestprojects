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
            try
            {
                var code = "02121dc6b290002f00bb7000d81c61bi";
                var userService = new UserService();
                var openId = userService.GetOpenId(1, code);
                Assert.IsTrue(true);
            }
            catch (Exception exception)
            {
                Assert.Inconclusive(string.Format("由于code只能使用一次，此处不好重复调用测试用例.[{0}]", exception.Message));
            }
        }
    }
}