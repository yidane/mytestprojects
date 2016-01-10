using System;
using OneGulp.WeChat.MP.AdvancedAPIs;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Application.Weixin.Token.Service;
using WeixinPF.Common;
using WeixinPF.Model.WeiXin.User;
using IUserRepository = WeixinPF.Application.Weixin.User.Repository.IUserRepository;

namespace WeixinPF.Application.Weixin.User.Service
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppInfoRepository _appInfoRepository;

        public UserService()
        {
            _appInfoRepository = DependencyManager.Resolve<IAppInfoRepository>();
            _userRepository = DependencyManager.Resolve<IUserRepository>();
        }

        /// <summary>
        /// 获取微信用户信息
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(int appId, string openId)
        {
            //判断appId是否合法
            var appInfo = _appInfoRepository.GetAppInfo(appId);
            if (appInfo == null)
                throw new Exception(string.Format("获取用户信息失败:通过参数{0}获取到的公众号信息为空", appId));

            var userInfoFromDb = _userRepository.GetUserInfo(appId, openId);
            //如果从数据库获取用户为空，则需要重新从微信更新
            string tokenErrorMessage;
            var accessToken = new AccessTokenService().GetAccessToken(appId, out tokenErrorMessage);

            if (accessToken == null || !string.IsNullOrEmpty(tokenErrorMessage))
                throw new Exception(string.Format("获取用户信息失败:", tokenErrorMessage));
            if (userInfoFromDb != null) return userInfoFromDb;

            var newUserInfoFromWeiXin = UserApi.Info(accessToken.Ticket, openId);
            if (newUserInfoFromWeiXin == null)
                throw new Exception("获取用户信息失败:从微信获取到的用户为空");
            var newUserInfo = new UserInfo
            {
                AppId = appId,
                Openid = newUserInfoFromWeiXin.openid,
                City = newUserInfoFromWeiXin.city,
                Country = newUserInfoFromWeiXin.country,
                Groupid = newUserInfoFromWeiXin.groupid,
                Headimgurl = newUserInfoFromWeiXin.headimgurl,
                Language = newUserInfoFromWeiXin.language,
                Nickname = newUserInfoFromWeiXin.nickname,
                Province = newUserInfoFromWeiXin.province,
                Remark = newUserInfoFromWeiXin.remark,
                Sex = newUserInfoFromWeiXin.sex,
                //Subscribe = newUserInfoFromWeiXin.subscribe,
                SubscribeTime = newUserInfoFromWeiXin.subscribe_time
            };

            //缓存到数据库
            _userRepository.Add(newUserInfo);

            return newUserInfo;
        }

        /// <summary>
        /// 获取用户OpenId
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetOpenId(int appId, string code)
        {
            //判断appId是否合法
            var appInfo = _appInfoRepository.GetAppInfo(appId);
            if (appInfo == null)
                throw new Exception(string.Format("获取用户OpenId失败:通过参数{0}获取到的公众号信息为空", appId));

            if (string.IsNullOrEmpty(appInfo.AppId) || string.IsNullOrEmpty(appInfo.AppSecret))
                throw new Exception(string.Format("获取用户OpenId失败:通过参数{0}获取到的公众号的AppId或AppSecret为空", appId));

            var result = OAuthApi.GetAccessToken(appInfo.AppId, appInfo.AppSecret, code);
            if (result == null)
                throw new Exception("获取用户OpenId失败:从微信获取OpenId为空");

            return result.openid;
        }
    }
}