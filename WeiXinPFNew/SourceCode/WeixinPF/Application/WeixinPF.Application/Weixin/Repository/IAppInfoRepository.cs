using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Application.Weixin.Repository
{
    public interface IAppInfoRepository
    {
        int Add(AppInfo appInfo);

        AppInfo GetAppInfo(int wid);

        bool Update(AppInfo appInfo);

        bool Exists(int appId);

        bool Delete(int appId);

        int GetUserWxNumCount(int uId);

        List<AppInfo> GetModelList(int uId, string keyWord);

        List<AppInfo> GetUserWeiXinListByUId(int pageSize, int pageIndex, int uId, out int total);

        List<AppInfo> GetAppInfoListByAgentIdAdnUId(int pageSize, int pageIndex, int uId, out int total);
    }
}
