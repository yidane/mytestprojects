using System.Linq;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Infrastructure.Weixin
{
    public class PaymentInfoRepository : IPaymentInfoRepository
    {
        private readonly EFRepository<PaymentInfo> _efRepository = new EFRepository<PaymentInfo>(new WeiXinDbContext());

        /// <summary>
        ///     增加一条数据
        /// </summary>
        public int Add(PaymentInfo model)
        {
            if (model != null)
            {
                _efRepository.Add(model);

                return model.Id;
            }

            return 0;
        }

        /// <summary>
        ///     更新一条数据
        /// </summary>
        public bool Update(PaymentInfo model)
        {
            if (model == null)
                return false;

            _efRepository.Update(model);
            return true;
        }

        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public PaymentInfo GetModel(int id)
        {
            if (id < 0)
                return null;

            return _efRepository.Get(item => item.Id.Equals(id)).FirstOrDefault();
        }

        public PaymentInfo GetModelByAppId(int appId)
        {
            return new PaymentInfo()
            {
                MchId = "1266087601",
                Paykey = "4A5E7B87F3324A6DA22E55FDC12150B6"
            };

            if (appId < 0)
                return null;

            return _efRepository.Get(item => item.AppId.Equals(appId)).FirstOrDefault();
        }
    }
}