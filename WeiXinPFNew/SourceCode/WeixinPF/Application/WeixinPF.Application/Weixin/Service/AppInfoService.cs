using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Common;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Application.Weixin.Service
{
    public class AppInfoService
    {
        private IAppInfoRepository _appInfoRepository = null;

        public AppInfoService()
        {
            _appInfoRepository = DependencyManager.Resolve<IAppInfoRepository>();
        }

        public AppInfo GetAppInfo(int wid)
        {
            return wid == 0 ? null : _appInfoRepository.GetAppInfo(wid);
        }

        public int Add(AppInfo appInfo)
        {
            return _appInfoRepository.Add(appInfo);
        }

        public bool Update(AppInfo appInfo)
        {
            return _appInfoRepository.Update(appInfo);
        }

        public bool Exists(int appId)
        {
            return _appInfoRepository.Exists(appId);
        }

        public bool Delete(int appId)
        {
            return _appInfoRepository.Delete(appId);
        }

        public List<AppInfo> GetModelList(int uId, string keyWord)
        {
            return _appInfoRepository.GetModelList(uId, keyWord);
        }

        public int GetUserWxNumCount(int uId)
        {
            return _appInfoRepository.GetUserWxNumCount(uId);
        }

        public List<AppInfo> GetUserWeiXinListByUId(int pageSize, int pageIndex, int uId, out int total)
        {
            return _appInfoRepository.GetUserWeiXinListByUId(pageSize, pageIndex, uId, out total);
        }

        public List<AppInfo> GetAppInfoListByAgentIdAdnUId(int pageSize, int pageIndex, int uId,
            out int total)
        {
            return _appInfoRepository.GetAppInfoListByAgentIdAdnUId(pageSize, pageIndex, uId, out total);
        }
    }
}
