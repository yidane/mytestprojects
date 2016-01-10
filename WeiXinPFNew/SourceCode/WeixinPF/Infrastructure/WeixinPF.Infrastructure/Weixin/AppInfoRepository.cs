using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Model.WeiXin;
using System.Linq;
using System.Linq.Expressions;

namespace WeixinPF.Infrastructure.Weixin
{
    public class AppInfoRepository : IAppInfoRepository
    {
        private readonly EFRepository<AppInfo> _efRepository = new EFRepository<AppInfo>(new WeiXinDbContext());

        public AppInfo GetAppInfo(int wid)
        {
            return new AppInfo
            {
                id = 1,
                AppId = "wxdd6127bdb5e7611c",
                AppSecret = "78fb32f17d30a6ade836319283ccf118"
            };
        }

        public bool Exists(int appId)
        {
            return _efRepository.Get(item => item.id == appId).Any();
        }

        public int GetUserWxNumCount(int uId)
        {
            return _efRepository.Get(item => item.uId == uId).Count();
        }


        public bool Update(AppInfo appInfo)
        {
            if (appInfo == null)
                return false;

            _efRepository.Update(appInfo);
            return true;
        }

        public int Add(AppInfo appInfo)
        {
            if (appInfo == null)
                return 0;

            _efRepository.Add(appInfo);
            return appInfo.id;
        }


        public bool Delete(int appId)
        {
            if (appId <= 0)
                return false;

            var appInfo = this.GetAppInfo(appId);
            if (appInfo == null)
                return false;
            appInfo.IsDelete = true;
            appInfo.DeleteDate = DateTime.Now;

            _efRepository.Update(appInfo);
            return true;
        }


        public List<AppInfo> GetModelList(int uId, string keyWord)
        {
            var query = _efRepository.Get(item => item.IsDelete && item.uId == uId);
            if (!string.IsNullOrEmpty(keyWord))
                query = query.Where(item => item.wxName.Contains(keyWord) || item.WxCode.Contains(keyWord));

            return query.ToList();
        }

        public List<AppInfo> GetUserWeiXinListByUId(int pageSize, int pageIndex, int uId, out int total)
        {
            var query = _efRepository.Get(item => item.uId == uId && item.IsDelete == false);
            total = query.Count();
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<AppInfo> GetAppInfoListByAgentIdAdnUId(int pageSize, int pageIndex, int uId, out int total)
        {
            var weiXinContext = _efRepository.Context as WeiXinDbContext;


            var query = from s in weiXinContext.AppInfoContext
                        join sd in weiXinContext.ManagerInfoContext on s.uId equals sd.id into g
                        from stuDesc in g.DefaultIfEmpty()
                        where s.IsDelete == false
                        where s.uId == uId
                        orderby s.uId ascending
                        orderby s.CreateDate descending
                        select s;

            total = query.Count();
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}