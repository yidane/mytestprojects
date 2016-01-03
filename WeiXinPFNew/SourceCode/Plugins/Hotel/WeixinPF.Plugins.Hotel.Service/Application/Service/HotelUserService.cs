using System.Collections.Generic;
using System.Linq;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Service
{
    public class HotelUserService
    {
        private readonly IHotelUserRepository _repository;
        public HotelUserService(IHotelUserRepository repository)
        {
            this._repository = repository;
            //dal = new DAL.manager(siteConfig.sysdatabaseprefix);
        }

        public int Add(HotelUserInfo model)
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

        public IList<HotelUserInfo> GetModelList(string strWhere)
        {
            return this._repository.GetModelList(strWhere);
        }

        public HotelUserInfo GetModel(int managerId)
        {
            return this._repository.Get(item=>item.ManagerId.Equals(managerId)).FirstOrDefault();
        }
    }
}
