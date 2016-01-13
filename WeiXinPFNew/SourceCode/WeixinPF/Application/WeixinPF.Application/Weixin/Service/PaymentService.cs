using System.Collections.Generic;
using System.Data;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Common;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Application.Weixin.Service
{
    public class PaymentService
    {
        private readonly IPaymentInfoRepository _repository;

        public PaymentService()
        {
            _repository = DependencyManager.Resolve<IPaymentInfoRepository>();
        }

        public PaymentService(IPaymentInfoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PaymentInfo model)
        {
            return _repository.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(PaymentInfo model)
        {
            return _repository.Update(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PaymentInfo GetModel(int id)
        {
            return _repository.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PaymentInfo> DataTableToList(DataTable dt)
        {
            var modelList = new List<PaymentInfo>();
            //var rowsCount = dt.Rows.Count;
            //if (rowsCount > 0)
            //{
            //    PaymentInfo model = null;
            //    for (var n = 0; n < rowsCount; n++)
            //    {
            //        model = _repository.DataRowToModel(dt.Rows[n]);
            //        if (model != null)
            //        {
            //            modelList.Add(model);
            //        }
            //    }
            //}
            return modelList;
        }

        /// <summary>
        /// 得到wid的微信支付配置信息一个对象实体
        /// </summary>
        public PaymentInfo GetModelByAppId(int appId)
        {
            return _repository.GetModelByAppId(appId);
        }
    }
}