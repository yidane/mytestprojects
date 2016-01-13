using WeixinPF.Model.WeiXin.Pay;

namespace WeixinPF.Application.Weixin.Pay.Repository
{
    public interface IPaymentInfoRepository
    {
        bool Add(PaymentInfo paymentInfo);

        PaymentInfo GetPaymentInfoByWxOrderNo(string wxOrderNo);
        PaymentInfo GetPaymentInfoBySysOrderNo(string moudleName, string sysOrderNo);
        bool PaySuccess(PaymentInfo paymentInfo);
        bool PayFail(PaymentInfo paymentInfo);
    }
}