using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Infrastructure.BaseRepository;

namespace WeixinPF.Hotel.Plugins.Service.Infrastructure
{
    public class HotelUserRepository : EFRepository<HotelUserInfo>,IHotelUserRepository
    {
        private DbContext _context;
        public HotelUserRepository(DbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public IList<HotelUserInfo> GetModelList(string strWhere)
        {
            if(!string.IsNullOrEmpty(strWhere))
            {
                strWhere = " Where " + strWhere;
            }

            string query = "Select * From [dbo].[wx_hotel_user]" + strWhere;
            return Context.Set<HotelUserInfo>().SqlQuery(query).ToList();
        }
    }
}
