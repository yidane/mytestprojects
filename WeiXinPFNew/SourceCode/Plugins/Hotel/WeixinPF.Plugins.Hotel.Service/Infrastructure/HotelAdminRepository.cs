using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Infrastructure.BaseRepository;

namespace WeixinPF.Hotel.Plugins.Service.Infrastructure
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
