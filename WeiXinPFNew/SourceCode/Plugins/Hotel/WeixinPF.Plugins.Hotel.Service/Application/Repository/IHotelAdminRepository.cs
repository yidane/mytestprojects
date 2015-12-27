using System.Collections.Generic;
using WeixinPF.Application.BaseRepository;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Application.Repository
{
    public interface IHotelAdminRepository:IRepository<HotelAdminInfo>
    {
        IList<HotelAdminInfo> GetModelList(string strWhere);
    }
}