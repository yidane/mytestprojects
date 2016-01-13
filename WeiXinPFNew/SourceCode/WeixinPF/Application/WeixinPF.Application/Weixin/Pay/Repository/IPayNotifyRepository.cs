using OneGulp.WeChat.MP.TenPayLibV3.TenPayV3.Model;

namespace WeixinPF.Application.Weixin.Pay.Repository
{
    public interface IPayNotifyRepository
    {
        bool PayNotify(PaymentNotify paymentNotify, out string message);
    }
}