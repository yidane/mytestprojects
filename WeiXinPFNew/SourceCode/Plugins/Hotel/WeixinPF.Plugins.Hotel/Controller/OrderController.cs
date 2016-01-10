using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Shared;

namespace WeixinPF.Hotel.Plugins.Controller
{
    public class OrderController : ApiController
    {
        public CreateOrderResponse Save(CreateOrderRequest request)
        {
            try
            {
                CreateOrderResponse responseData = null;

                IAsyncResult asyncResult = BusEntry.dictBus["hotel"].Send("WeixinPF.Hotel.Plugins.Service", request)
                        .Register(response =>
                        {
                            CompletionResult result = response.AsyncState as CompletionResult;
                            if (result != null)
                            {
                                responseData = result.Messages[0] as CreateOrderResponse;

                            }
                        }, this);

                WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
                asyncWaitHandle.WaitOne(1000000000);

               if(asyncResult.IsCompleted)
               {
                   return responseData;
               }
               else
               {
                   throw  new Exception("保存订失败。");
               }
            }
            catch
            {
                throw new Exception("保存订失败。");
            }
            
        }

        public AjaxResult Get()
        {
            return AjaxResult.Succeed(1);
        }
    }
}