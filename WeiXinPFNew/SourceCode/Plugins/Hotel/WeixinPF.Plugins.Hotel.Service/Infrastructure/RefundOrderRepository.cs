using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Infrastructure.BaseRepository;

namespace WeixinPF.Hotel.Plugins.Service.Infrastructure
{
    public class RefundOrderRepository : EFRepository<RefundOrder>, IRefundOrderRepository
    {
        private DbContext _context;

        public RefundOrderRepository(DbContext context) : base(context)
        {
            _context = context;
        }
    }
}
