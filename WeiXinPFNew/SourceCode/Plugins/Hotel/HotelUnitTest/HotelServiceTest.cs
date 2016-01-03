using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeixinPF.Hotel.Plugins.Service.Application;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Infrastructure;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace HotelTest
{
    [TestClass]
    public class HotelServiceTest
    {
        [TestMethod]
        public void HotelAddUser_HotelUser_ReturnNewUserId()
        {
            var service=new HotelUserService(new HotelUserRepository(new HotelDbContext()));

            var result = service.Add(new HotelUserInfo() {AdminId = 1,HotelId = 1,ManagerId = 1});

            Assert.IsTrue(result.Equals(0));
        }
    }
}
