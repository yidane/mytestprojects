using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.AutoMapper;
using WeixinPF.Messages.RequestResponse.Dtos;

namespace HotelUnitTest
{
    [TestClass]
    public class OrderUnitTest
    {
        [TestMethod]
        public void GetOrderList()
        {
            AutoMapperConfiguration.Configure();
            var service = new HotelOrderService();
            var list = service.GetModelList(string.Format("openid='{0}'", "test"));

            list.MapTo<List<OrderDto>>();

        }
        [TestMethod]
        public void GetOrderUserInfo()
        {
            var service = new HotelOrderService();
            var order = service
                .GetModelList(string.Format("openid='{0}'", "test"))
                .OrderByDescending(o => o.createDate)
                .FirstOrDefault();

            var user = new OrderUserDto()
            {
                UserIdcard = string.Empty,
                UserMobile = string.Empty,
                UserName = string.Empty
            };

            if (order != null)
            {
                user.UserName = order.oderName;
                user.UserMobile = order.tel;
                user.UserIdcard = order.identityNumber;
            }

            Assert.IsTrue(!string.IsNullOrEmpty(user.UserName));
        }
    }
}
