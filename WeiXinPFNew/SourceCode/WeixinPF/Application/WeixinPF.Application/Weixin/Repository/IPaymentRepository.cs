using WeixinPF.Model.WeiXin;

namespace WeixinPF.Application.Weixin.Repository
{
    public interface IPaymentInfoRepository
    {
        int Add(PaymentInfo model);
        bool Update(PaymentInfo model);
        PaymentInfo GetModel(int id);
        PaymentInfo GetModelByAppId(int appId);
    }
}