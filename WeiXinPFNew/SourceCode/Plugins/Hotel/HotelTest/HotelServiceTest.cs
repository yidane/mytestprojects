﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WeixinPF.Plugins.Hotel.Service.Application;
using WeixinPF.Plugins.Hotel.Service.Application.Service;
using WeixinPF.Plugins.Hotel.Service.Infrastructure;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace HotelTest
{
    [TestFixture]
    public class HotelServiceTest
    {
        [Test]
        public void HotelAddUser_HotelUser_ReturnNewUserId()
        {
            var service=new HotelUserService(new HotelUserRepository(new HotelDbContext()));

            var result = service.Add(new HotelUserInfo() {AdminId = 1,HotelId = 1,ManagerId = 1});

            Assert.IsTrue(result.Equals(0));
        }
    }
}
