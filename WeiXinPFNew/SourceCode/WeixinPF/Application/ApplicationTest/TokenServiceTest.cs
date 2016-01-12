using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeixinPF.Application.Weixin.Token.Repository;
using WeixinPF.Application.Weixin.Token.Service;
using WeixinPF.Common;

namespace ApplicationTest
{
    [TestClass]
    public class TokenServiceTest
    {
        [TestMethod]
        public void GetAccessToken()
        {
            var iAccessToken = DependencyManager.Resolve<IAccessTokenRepository>();
            var token = iAccessToken.GetAccessToken(1);

            AccessTokenService accessTokenService = new AccessTokenService();
            string errorMessage;
            var result = accessTokenService.GetAccessToken(1, out errorMessage);

            Assert.IsTrue(result != null && !result.IsExpired());
        }

        [TestMethod]
        public void GetJsApiTicket()
        {
            JsApiTicketService jsApiTicketService = new JsApiTicketService();
            string errorMessage;
            var result = jsApiTicketService.GetJsApiTicket(1, out errorMessage);

            Assert.IsTrue(result != null && !result.IsExpired());
        }
    }
}
