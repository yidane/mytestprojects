using System.Collections.Generic;
using WeixinPF.Application.BaseRepository;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Repository
{
    public interface IHotelUserRepository : IRepository<HotelUserInfo>
    {
        IList<HotelUserInfo> GetModelList(string strWhere);
    }
}
