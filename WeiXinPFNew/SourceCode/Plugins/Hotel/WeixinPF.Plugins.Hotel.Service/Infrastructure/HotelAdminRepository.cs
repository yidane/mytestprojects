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
    public class HotelAdminRepository:EFRepository<HotelAdminInfo>, IHotelAdminRepository
    {
        private DbContext _context;
        public HotelAdminRepository(DbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public IList<HotelAdminInfo> GetModelList(string strWhere)
        {
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = " Where " + strWhere;
            }

            var query = "Select * From [dbo].[wx_hotel_admin]" + strWhere;

            return Context.Set<HotelAdminInfo>().SqlQuery(query).ToList();
        }
    }
}
