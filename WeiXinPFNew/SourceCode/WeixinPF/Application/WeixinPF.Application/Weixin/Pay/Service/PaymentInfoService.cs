using WeixinPF.Application.Weixin.Pay.Repository;
using WeixinPF.Common;
using WeixinPF.Model.WeiXin.Pay;

namespace WeixinPF.Application.Weixin.Pay.Service
{
    public class PaymentInfoService
    {
        private readonly IPaymentInfoRepository _paymentInfoRepository;

        public PaymentInfoService()
        {
            _paymentInfoRepository = DependencyManager.Resolve<IPaymentInfoRepository>();
        }

        public bool Add(PaymentInfo paymentInfo)
        {
            return _paymentInfoRepository.Add(paymentInfo);
        }

        public bool PaySuccess(string wxOrderNumber)
        {
            var paymentInfo = _paymentInfoRepository.GetPaymentInfoByWxOrderNo(wxOrderNumber);
            return _paymentInfoRepository.PaySuccess(paymentInfo);
        }

        public bool PayFail(PaymentInfo paymentInfo)
        {
            return _paymentInfoRepository.PayFail(paymentInfo);
        }
    }
}