using System.Collections.Generic;
using System.Data.Entity;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Plugins.Hotel.Service.Application;
using WeixinPF.Plugins.Hotel.Service.Application.Repository;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Infrastructure
{
    public class IdentifyingCodeRepository:EFRepository<IdentifyingCodeInfo>,IIdentifyingCodeRepository
    {
        public IdentifyingCodeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
