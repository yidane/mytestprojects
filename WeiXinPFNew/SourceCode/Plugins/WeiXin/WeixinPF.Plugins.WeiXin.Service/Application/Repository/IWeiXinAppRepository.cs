using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.BaseRepository;
using WeixinPF.Plugins.WeiXin.Service.Models;

namespace WeixinPF.Plugins.WeiXin.Service.Application.Repository
{
    public interface IWeiXinAppRepository : IRepository<WeiXinAppInfo>
    {
        //WeiXinAppInfo GetWeiXinAppInfo(int appId);
    }
}
