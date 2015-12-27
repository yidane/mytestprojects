using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.BaseRepository;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Application.Repository
{
    public interface IHotelUserRepository : IRepository<HotelUserInfo>
    {
        IList<HotelUserInfo> GetModelList(string strWhere);
    }
}
