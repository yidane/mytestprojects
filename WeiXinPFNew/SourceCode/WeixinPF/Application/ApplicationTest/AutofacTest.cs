using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeixinPF.Application.Agent.Repository;
using WeixinPF.Common;

namespace ApplicationTest
{
    [TestClass]
    public class AutofacTest
    {
        [TestMethod]
        public void GetIntanceByNameTest()
        {
            var instance = DependencyManager.ResolveByName<IAgentRepository>("pay");

            Assert.IsTrue(instance != null);
        }
    }
}
