using System.Linq;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Infrastructure;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Service
{
    public class HotelOrderService
    {
        public static HotelOrderInfo GetOrderInfo(int orderId)
        {
            using (var context = new HotelDbContext())
            {
                IHotelOrderRepository repository = new HotelOrderRepository(context); //改造方向：依赖注入，彻底去除对Infrastructure层的依赖
                var result = repository.Get(item => item.id.Equals(orderId)).FirstOrDefault();

                return result;
            }
        }
    }
}
