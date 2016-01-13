using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Weixin.Pay.Repository;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Common;
using WeixinPF.Model.WeiXin.Pay;

namespace WeixinPF.Application.Weixin.Pay.Service
{
    public class UnifiedOrderService
    {
        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="unifiedOrderInfo"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string UnifiedOrder(UnifiedOrderInfo unifiedOrderInfo, out string message)
        {
            if (unifiedOrderInfo == null)
            {
                message = "下单数据为空";
                return string.Empty;
            }

            if (unifiedOrderInfo.CheckRequired(out message))
            {
                return string.Empty;
            }

            //调用微信统一下单接口
            var appInfo = DependencyManager.Resolve<IAppInfoRepository>().GetAppInfo(unifiedOrderInfo.AppId);
            var payment = DependencyManager.Resolve<IPaymentRepository>().GetModelByWid(unifiedOrderInfo.AppId);
            //if(string.IsNullOrEmpty(appInfo.AppId)||string.IsNullOrEmpty(appInfo.AppSecret)||string.IsNullOrEmpty(app)


            var unifiedOrderRepository = DependencyManager.Resolve<IUnifiedOrderRepository>();

            return string.Empty;
        }
    }
}
