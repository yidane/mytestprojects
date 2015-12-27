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
