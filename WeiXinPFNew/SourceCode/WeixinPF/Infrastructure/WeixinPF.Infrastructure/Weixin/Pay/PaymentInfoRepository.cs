using System;
using System.Linq;
using WeixinPF.Application.Weixin.Pay.Repository;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Model.WeiXin.Pay;

namespace WeixinPF.Infrastructure.Weixin.Pay
{
    public class PaymentInfoRepository : IPaymentInfoRepository
    {
        private readonly EFRepository<PaymentInfo> _efRepository = new EFRepository<PaymentInfo>(new WeiXinDbContext());

        public bool Add(PaymentInfo paymentInfo)
        {
            if (paymentInfo == null)
                return false;

            _efRepository.Add(paymentInfo);
            return true;
        }

        public bool PaySuccess(PaymentInfo paymentInfo)
        {
            if (paymentInfo == null)
                return false;

            paymentInfo.Status = 1;
            paymentInfo.ModifyTime = DateTime.Now;
            _efRepository.Update(paymentInfo);
            return true;
        }

        public bool PayFail(PaymentInfo paymentInfo)
        {
            if (paymentInfo == null)
                return false;

            paymentInfo.Status = 2;
            paymentInfo.ModifyTime = DateTime.Now;
            _efRepository.Update(paymentInfo);
            return true;
        }


        public PaymentInfo GetPaymentInfoByWxOrderNo(string wxOrderNo)
        {
            return _efRepository.Get(item => item.WxOrderCode.Equals(wxOrderNo)).FirstOrDefault();
        }

        public PaymentInfo GetPaymentInfoBySysOrderNo(string moudleName, string sysOrderNo)
        {
            return _efRepository.Get(item => item.OrderId.Equals(sysOrderNo) && item.ModuleName.Equals(moudleName)).FirstOrDefault();
        }
    }
}