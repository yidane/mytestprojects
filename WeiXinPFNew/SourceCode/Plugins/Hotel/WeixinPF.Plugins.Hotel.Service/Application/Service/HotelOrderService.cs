using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Plugins.Hotel.Service.Application.Repository;
using WeixinPF.Plugins.Hotel.Service.Infrastructure;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Application.Service
{
    public class HotelOrderService
    {
        public static HotelOrderInfo GetOrderInfo(int orderId)
        {
            using (var context = new HotelDbContext())
            {
                IHotelOrderRepository repository = new HotelOrderRepository(context); //改造方向：依赖注入，彻底去除对Infrastructure层的依赖

                return repository.Get(item =>item.Id.Equals(orderId)).FirstOrDefault();
            }
        }
    }
}
