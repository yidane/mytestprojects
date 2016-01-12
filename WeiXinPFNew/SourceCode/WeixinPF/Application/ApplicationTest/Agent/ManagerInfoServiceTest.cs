using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeixinPF.Application.Agent.Service;
using WeixinPF.Model.Agent;

namespace ApplicationTest.Agent
{
    [TestClass]
    public class ManagerInfoServiceTest
    {
        [TestMethod]
        public void Add()
        {
            var managerInfo = new ManagerInfo
            {
                Email = "yidane@outlook.com",
                Salt = "yidane",
                UserName = "yidane",
                AddTime = DateTime.Now,
                AgentId = 1,
                AgentLevel = -1,
                City = "北京",
                County = "西三旗",
                Province = "北京"
            };

            var managerInfoService = new ManagerInfoService();
            var managerInfoId = managerInfoService.Add(managerInfo);

            Assert.IsTrue(managerInfoId > 0);
        }

        [TestMethod]
        public void AddAdmin()
        {
            var managerInfo = new ManagerInfo
            {
                Email = "yidane@outlook.com",
                RealName = "超级管理员",
                Salt = "28LH48",
                UserName = "admin",
                Password = "EB51565598856A17",
                AddTime = DateTime.Now,
                AgentId = 1,
                AgentLevel = -1,
                City = "北京",
                County = "西三旗",
                Province = "北京",
                WxNum = 100
            };

            var managerInfoService = new ManagerInfoService();
            var managerInfoId = managerInfoService.Add(managerInfo);

            Assert.IsTrue(managerInfoId > 0);
        }

        [TestMethod]
        public void GetManagerInfo()
        {
            var managerService = new ManagerInfoService();
            var model = managerService.GetModel(1);

            Assert.IsTrue(true);
        }
    }
}
