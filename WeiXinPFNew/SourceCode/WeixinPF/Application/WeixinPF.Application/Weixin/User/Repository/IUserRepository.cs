using System.Collections.Generic;
using WeixinPF.Model.WeiXin.User;

namespace WeixinPF.Application.Weixin.User.Repository
{
    public interface IUserRepository
    {
        void Add(UserInfo userInfo);
        UserInfo GetUserInfo(int appId, string openId);
        List<UserInfo> GetUserInfoByGroupId(int appId, int groupId);
    }
}