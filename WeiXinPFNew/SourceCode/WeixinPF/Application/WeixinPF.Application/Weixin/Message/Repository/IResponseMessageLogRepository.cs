using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Model.WeiXin.Message;

namespace WeixinPF.Application.Weixin.Message.Repository
{
    public interface IResponseMessageLogRepository
    {
        void Add(ResponseMessageLog log);

        void Add(int wid, string openid, string requestType, string requestContent, string responseType, string responseContent, string toUserName);
    }
}
