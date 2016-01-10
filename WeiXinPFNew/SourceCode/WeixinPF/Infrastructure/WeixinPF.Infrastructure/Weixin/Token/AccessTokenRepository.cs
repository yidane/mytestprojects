using System;
using System.Linq;
using WeixinPF.Application.Weixin.Token.Repository;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Model.WeiXin;
using WeixinPF.Model.WeiXin.Token;

namespace WeixinPF.Infrastructure.Weixin.Token
{
    public class AccessTokenRepository : IAccessTokenRepository
    {
        private readonly EFRepository<AccessToken> _efRepository = new EFRepository<AccessToken>(new WeiXinDbContext());

        public AccessToken GetAccessToken(int wid)
        {
            var result = _efRepository.Get(item => item.AppId == wid && !item.Expired)
                .OrderByDescending(item => item.SysDateTime)
                .Take(1);

            return result.FirstOrDefault();
        }

        public void SetAccessTokenExpire(AccessToken accessToken)
        {
            if (accessToken == null)
                return;

            accessToken.Expired = true;
            accessToken.ExpireDateTime = DateTime.Now;
            _efRepository.Update(accessToken);
        }

        public void Add(AccessToken accessToken)
        {
            if (accessToken == null)
                throw new Exception("保存的AccessToken对象为空");

            _efRepository.Add(accessToken);
        }
    }
}