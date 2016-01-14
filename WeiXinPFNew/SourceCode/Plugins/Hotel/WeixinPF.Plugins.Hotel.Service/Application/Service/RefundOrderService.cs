using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Infrastructure;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Service
{
    public class RefundOrderService
    {
        private readonly IRefundOrderRepository _refundOrderRepository;

        public RefundOrderService()
        {
            _refundOrderRepository = new RefundOrderRepository(new HotelDbContext());
        }

        public RefundOrder GetModel(Expression<Func<RefundOrder, bool>> predicate)
        {
            return _refundOrderRepository.Get(predicate).FirstOrDefault();
        }
    }
}
