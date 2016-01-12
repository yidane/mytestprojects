using System;
using System.Collections.Generic;
using System.Linq;
using WeixinPF.Application.Weixin;
using WeixinPF.Application.Weixin.User.Repository;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Model;
using WeixinPF.Model.WeiXin;
using WeixinPF.Model.WeiXin.User;

namespace WeixinPF.Infrastructure.Weixin.User
{
    public class UserRepository : IUserRepository
    {
        private readonly EFRepository<UserInfo> _efRepository = new EFRepository<UserInfo>(new WeiXinDbContext());

        public void Add(UserInfo userInfo)
        {
            if (userInfo == null)
                return;

            _efRepository.Add(userInfo);
        }
        public UserInfo GetUserInfo(int appId, string openId)
        {
            return _efRepository.Get(item => item.AppId == appId && item.Openid.Equals(openId)).FirstOrDefault();
        }

        public List<UserInfo> GetUserInfoByGroupId(int appId, int groupId)
        {
            throw new NotImplementedException();
        }
    }
}
