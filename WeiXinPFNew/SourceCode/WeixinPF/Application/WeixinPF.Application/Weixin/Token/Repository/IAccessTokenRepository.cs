using WeixinPF.Model.WeiXin.Token;

namespace WeixinPF.Application.Weixin.Token.Repository
{
    public interface IAccessTokenRepository
    {
        AccessToken GetAccessToken(int wid);

        void SetAccessTokenExpire(AccessToken accessToken);

        void Add(AccessToken accessToken);
    }
}