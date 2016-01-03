using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Plugins.WeiXin.Service.Application.Repository;
using WeixinPF.Plugins.WeiXin.Service.Models;

namespace WeixinPF.Plugins.WeiXin.Service.Application.Service
{
    public class WeiXinAppService
    {
        private IWeiXinAppRepository _AppRepository;

        public WeiXinAppService(IWeiXinAppRepository repository)
        {
            _AppRepository = repository;
        }

        public List<WeiXinAppInfo> GetAppInfoList()
        {
            return _AppRepository.Get(item => item.id > 0).ToList();
        }

        public bool Add(WeiXinAppInfo appInfo)
        {
            return _AppRepository.Add(appInfo);
        }
    }
}
