using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Plugins.Hotel.Service.Application.Repository;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Application.Service
{
    public class HotelAdminService
    {
        public IHotelAdminRepository _repository;
        public HotelAdminService(IHotelAdminRepository repository)
        {
            this._repository = repository;
        }


        public int Add(HotelAdminInfo model)
        {
            if (this._repository.Add(model))
            {
                return model.Id;
            }
            else
            {
                return 0;
            }
        }

        public HotelAdminInfo GetModel(int ManagerId)
        {
            return this._repository.Get(item=>item.ManagerId.Equals(ManagerId)).FirstOrDefault();
        }

        public IList<HotelAdminInfo> GetModelList(string strWhere)
        {
            return this._repository.GetModelList(strWhere);
        }
    }
}
