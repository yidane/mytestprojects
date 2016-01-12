using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeixinPF.Application.Agent.Service;
using WeixinPF.Model.Agent;

namespace ApplicationTest.Agent
{
    [TestClass]
    public class ManagerRoleServiceTest
    {
        private readonly ManagerRoleService _service = new ManagerRoleService();
        [TestMethod]
        public void Add()
        {
            var managerRoleInfo = new ManagerRoleInfo();
            managerRoleInfo.AgentId = 1;
            managerRoleInfo.RoleName = "测试角色";
            managerRoleInfo.IsSys = 1;
            managerRoleInfo.RoleType = 1;
            managerRoleInfo.ManagerRoleValues = new List<ManagerRoleValueInfo>()
            {
                new ManagerRoleValueInfo(){RoleId = 1,NavName = "测试角色值1",ActionType = "testActionType1"},
                new ManagerRoleValueInfo(){RoleId = 2,NavName = "测试角色值2",ActionType = "testActionType2"}
            };

            var result = _service.Add(managerRoleInfo);

            Assert.IsTrue(result > 0);
        }
    }
}
