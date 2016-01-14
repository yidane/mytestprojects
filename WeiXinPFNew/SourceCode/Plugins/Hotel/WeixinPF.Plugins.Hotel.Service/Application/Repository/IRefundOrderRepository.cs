using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.BaseRepository;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Repository
{
    public interface IRefundOrderRepository : IRepository<RefundOrder>
    {
    }
}
