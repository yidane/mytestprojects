using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeixinPF.Application.Agent.Service;

namespace ApplicationTest.Agent
{
    [TestClass]
    public class ManagerLogServiceTest
    {
        private readonly ManagerLogService service = new ManagerLogService();
        [TestMethod]
        public void Exists()
        {
            var result = service.Exists(1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Add()
        {
            var result = service.Add(1, "testSessionid", "yidane", "test", "remark");
            Assert.IsTrue(result > 0);
        }
    }
}
