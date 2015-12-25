using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Plugins.Hotel.Service.Application.Repository;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Infrastructure
{
    public class HotelRepository : EFRepository<HotelInfo>, IHotelRepository
    {
        public HotelRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
