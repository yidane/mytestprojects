using System;
using System.Collections.Generic;
using System.Linq;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Infrastructure.Weixin
{
    public class AppInfoRepository : IAppInfoRepository
    {
        private readonly EFRepository<AppInfo> _efRepository = new EFRepository<AppInfo>(new WeiXinDbContext());

        public AppInfo GetAppInfo(int wid)
        {
            return new AppInfo
            {
                Id = 1,
                AppId = "wxdd6127bdb5e7611c",
                AppSecret = "78fb32f17d30a6ade836319283ccf118"
            };
        }

        public bool Exists(int appId)
        {
            return _efRepository.Get(item => item.Id == appId).Any();
        }

        public int GetUserWxNumCount(int uId)
        {
            return _efRepository.Get(item => item.UId == uId).Count();
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

            appInfo.CreateDate = DateTime.Now;

            _efRepository.Add(appInfo);
            return appInfo.Id;
        }

        public bool Delete(int appId)
        {
            if (appId <= 0)
                return false;

            var appInfo = GetAppInfo(appId);
            if (appInfo == null)
                return false;
            appInfo.IsDelete = true;
            appInfo.DeleteDate = DateTime.Now;

            _efRepository.Update(appInfo);
            return true;
        }

        public List<AppInfo> GetModelList(int uId, string keyWord)
        {
            var query = _efRepository.Get(item => !item.IsDelete && item.UId == uId);
            //使用关键字过滤
            if (!string.IsNullOrEmpty(keyWord))
                query = query.Where(item => item.WxName.Contains(keyWord) || item.WxCode.Contains(keyWord));

            //排序
            query = query.OrderByDescending(item => item.CreateDate).ThenByDescending(item => item.Id);

            return query.ToList();
        }

        public List<AppInfo> GetUserWeiXinListByUId(int pageSize, int pageIndex, int uId, out int total)
        {
            var query = _efRepository.Get(item => item.UId == uId && item.IsDelete == false);
            total = query.Count();
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<AppInfo> GetAppInfoListByAgentIdAdnUId(int pageSize, int pageIndex, int uId, out int total)
        {
            var weiXinContext = _efRepository.Context as WeiXinDbContext;


            var query = from s in weiXinContext.AppInfoContext
                        join sd in weiXinContext.ManagerInfoContext on s.UId equals sd.Id into g
                        from stuDesc in g.DefaultIfEmpty()
                        where s.IsDelete == false
                        where s.UId == uId
                        orderby s.UId ascending
                        orderby s.CreateDate descending
                        select s;

            total = query.Count();
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}