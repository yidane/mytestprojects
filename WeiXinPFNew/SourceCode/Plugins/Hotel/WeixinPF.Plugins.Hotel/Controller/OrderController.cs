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
        public AjaxResult Save(CreateOrderRequest request)
        {
            AjaxResult ajaxResult;
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

                ajaxResult = asyncResult.IsCompleted ?
                          AjaxResult.Succeed(responseData.OrderId) :
                          AjaxResult.Fail("保存订失败。");
            }
            catch
            {
                return AjaxResult.Fail("保存订失败。");
            }

            return ajaxResult;
        }

        public AjaxResult Get()
        {
            return AjaxResult.Succeed(1);
        }
    }
}