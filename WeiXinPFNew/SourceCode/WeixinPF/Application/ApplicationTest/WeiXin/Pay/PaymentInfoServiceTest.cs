using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeixinPF.Application.Weixin.Pay.Service;
using WeixinPF.Model.WeiXin.Pay;

namespace ApplicationTest.WeiXin.Pay
{
    [TestClass]
    public class PaymentInfoServiceTest
    {
        private readonly PaymentInfoService _paymentService = new PaymentInfoService();

        [TestMethod]
        public void Add()
        {
            try
            {
                var newPaymentInfo = new PaymentInfo
                {
                    ModuleName = "testModule",
                    OrderId = "123456789",
                    OrderCode = "ordercode123456789",
                    Status = 0,
                    WxOrderCode = "wx123456789",
                    CreateTime = DateTime.Now,
                    AppId = 1,
                    Description = "description",
                    OpenId = "openidtest",
                    PayAmount = 100,
                    PaymentId = Guid.NewGuid(),
                    Body = "买四个萝卜切吧切吧剁了吧"
                };

                Assert.IsTrue(_paymentService.Add(newPaymentInfo));

            }
            catch (Exception exception)
            {
                Assert.IsFalse(true, exception.Message);
            }
        }

        [TestMethod]
        public void PaySucceed()
        {
            Assert.IsTrue(_paymentService.PaySuccess("wx123456789"));
        }

        [TestMethod]
        public void PayFaild()
        {
            //TODO:暂时不做测试
            Assert.IsTrue(true);
        }
    }
}