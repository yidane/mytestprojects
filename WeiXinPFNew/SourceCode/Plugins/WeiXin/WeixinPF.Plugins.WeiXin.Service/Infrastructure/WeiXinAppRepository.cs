using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Plugins.WeiXin.Service.Application.Repository;
using WeixinPF.Plugins.WeiXin.Service.Models;

namespace WeixinPF.Plugins.WeiXin.Service.Infrastructure
{
    public class WeiXinAppRepository : EFRepository<WeiXinAppInfo>, IWeiXinAppRepository
    {
        private DbContext _context;
        public WeiXinAppRepository(DbContext dbContext)
            : base(dbContext)
        {
            this._context = dbContext;
        }
    }
}