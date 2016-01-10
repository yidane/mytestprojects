using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Weixin.Message.Repository;

namespace WeixinPF.Application.Weixin.Message.Service
{
    public class ResponseMessageLogRepositoryService : IResponseMessageLogRepository
    {
        public void Add(Model.WeiXin.Message.ResponseMessageLog log)
        {
            
        }


        public void Add(int wid, string openid, string requestType, string requestContent, string responseType, string responseContent, string toUserName)
        {
            
        }
    }
}
