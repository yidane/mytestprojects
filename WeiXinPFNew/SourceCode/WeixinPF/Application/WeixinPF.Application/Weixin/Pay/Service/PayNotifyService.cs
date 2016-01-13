using OneGulp.WeChat.MP.TenPayLibV3.TenPayV3.Model;
using WeixinPF.Application.Weixin.Pay.Repository;
using WeixinPF.Common;

namespace WeixinPF.Application.Weixin.Pay.Service
{
    public class PayNotifyService
    {
        public bool PayNotify(string moudleName, PaymentNotify paymentNotify, out string message)
        {
            var payNotifyRepository = DependencyManager.ResolveByName<IPayNotifyRepository>(moudleName);
            if (payNotifyRepository != null)
                return payNotifyRepository.PayNotify(paymentNotify, out message);

            message = string.Format("此模块{0}尚未配置支付后通知业务逻辑", moudleName);
            return false;
        }
    }
}