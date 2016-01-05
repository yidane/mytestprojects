using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WeixinPF.Application;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Infrastructure;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Service
{
    public class HotelOrderService
    {
        private readonly IHotelOrderRepository _repository;

        public HotelOrderService()//(//IHotelOrderRepository repository)
        {
            _repository = new HotelOrderRepository(new HotelDbContext());

        }
        public HotelOrderInfo GetOrderInfo(int orderId)
        {
            //using (var context = new HotelDbContext())
            //{
            //    IHotelOrderRepository repository = new HotelOrderRepository(context); //改造方向：依赖注入，彻底去除对Infrastructure层的依赖
            //    var result = repository.Get(item => item.id.Equals(orderId)).FirstOrDefault();

            //    return result;
            //}

            return _repository.Get(order => order.id.Equals(orderId)).FirstOrDefault();
        }

        public List<HotelOrderInfo> GetOrderList(Expression<Func<HotelOrderInfo, bool>> predicate)
        {
            return _repository.Get(predicate).ToList();
        }
    }
}
