using System;
using OneGulp.WeChat.MP.CommonAPIs;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Application.Weixin.Token.Repository;
using WeixinPF.Common;
using WeixinPF.Model.WeiXin.Token;

namespace WeixinPF.Application.Weixin.Token.Service
{
    public class AccessTokenService
    {
        private static readonly object _obj = new object();
        private readonly IAccessTokenRepository _accessTokenRepository;
        private readonly IAppInfoRepository _appInfoRepository;

        public AccessTokenService()
        {
            _accessTokenRepository = DependencyManager.Resolve<IAccessTokenRepository>();
            _appInfoRepository = DependencyManager.Resolve<IAppInfoRepository>();
        }

        /// <summary>
        /// 获取当前账号的AccessToken
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public AccessToken GetAccessToken(int wid, out string errorMessage)
        {
            lock (_obj)
            {
                try
                {
                    var appInfo = _appInfoRepository.GetAppInfo(wid);
                    if (appInfo == null)
                    {
                        errorMessage = string.Format("当前系统无wid为{0}相应公众号的配置", wid);
                        return null;
                    }
                    if (string.IsNullOrEmpty(appInfo.AppId) || string.IsNullOrEmpty(appInfo.AppSecret))
                    {
                        errorMessage = string.Format("当前系统wid为{0}相应公众号的配置不完整,AppId或者AppSecret未填写完全,请在[我的公众帐号]里补全信息！",
                            wid);
                        return null;
                    }

                    //从数据库中获取Token
                    var tokenFromDb = _accessTokenRepository.GetAccessToken(appInfo.id);
                    //如果不存在
                    if (tokenFromDb == null || tokenFromDb.IsExpired())
                    {
                        //设置已存的AccessToken过期
                        _accessTokenRepository.SetAccessTokenExpire(tokenFromDb);

                        //从微信重新获取AccessToken
                        var accessTokenResult = CommonApi.GetToken(appInfo.AppId, appInfo.AppSecret);
                        var newAccessToken = new AccessToken
                        {
                            Id = Guid.NewGuid(),
                            AppId = appInfo.id,
                            SysDateTime = DateTime.Now,
                            Ticket = accessTokenResult.access_token,
                            Expires = accessTokenResult.expires_in
                        };

                        //缓存新获取到的AccessToken到数据库
                        _accessTokenRepository.Add(newAccessToken);

                        errorMessage = string.Empty;
                        return newAccessToken;
                    }

                    errorMessage = string.Empty;

                    return tokenFromDb;
                }
                catch (Exception exception)
                {
                    errorMessage = exception.Message;
                    return null;
                }
            }
        }
    }
}