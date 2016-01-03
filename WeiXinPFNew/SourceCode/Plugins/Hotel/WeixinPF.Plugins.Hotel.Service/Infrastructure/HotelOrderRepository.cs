using System.Data.Entity;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Infrastructure.BaseRepository;

namespace WeixinPF.Hotel.Plugins.Service.Infrastructure
{
    public class HotelOrderRepository:EFRepository<HotelOrderInfo>,IHotelOrderRepository
    {
        public HotelOrderRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
