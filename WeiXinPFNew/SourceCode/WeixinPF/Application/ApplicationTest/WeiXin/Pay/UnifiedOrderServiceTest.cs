using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeixinPF.Application.Weixin.Pay.Service;
using WeixinPF.Model.WeiXin.Pay;

namespace ApplicationTest.WeiXin.Pay
{
    [TestClass]
    public class UnifiedOrderServiceTest
    {
        private UnifiedOrderService _unifiedOrderService = new UnifiedOrderService();

        [TestMethod]
        public void UnifiedOrderTest()
        {
            var unifiedOrderInfo = new UnifiedOrderInfo();
            unifiedOrderInfo.AppId = 1;
            unifiedOrderInfo.Openid = "obzTsw5qxlbwGYYZJC9b-91J-X1Y";
            unifiedOrderInfo.OutTradeNo = "outcode1234567890";
            unifiedOrderInfo.PayModuleName = "test";
            unifiedOrderInfo.TotalFee = 1;
            unifiedOrderInfo.Body = "测试支付";
            unifiedOrderInfo.OrderId = Guid.NewGuid().ToString();
            unifiedOrderInfo.PayComplete = "http://www.baidu.com";

            string unifiedErrorMessage;
            if (unifiedOrderInfo.CheckRequired(out unifiedErrorMessage))
            {
                var result = _unifiedOrderService.UnifiedOrder(unifiedOrderInfo, out unifiedErrorMessage);
            }


            Assert.IsFalse(true);
        }
    }
}
