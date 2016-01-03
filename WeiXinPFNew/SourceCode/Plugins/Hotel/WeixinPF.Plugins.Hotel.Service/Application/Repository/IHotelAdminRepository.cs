using System.Collections.Generic;
using WeixinPF.Application.BaseRepository;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Repository
{
    public interface IHotelAdminRepository:IRepository<HotelAdminInfo>
    {
        IList<HotelAdminInfo> GetModelList(string strWhere);
    }
}