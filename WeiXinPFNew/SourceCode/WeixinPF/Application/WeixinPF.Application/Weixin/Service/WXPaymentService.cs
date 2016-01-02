using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Model.Weixin;

namespace WeixinPF.Application.Weixin.Service
{
    public class WXPaymentService
    {
        private readonly IWXPaymentRepository _repository;

        public WXPaymentService(IWXPaymentRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(WX_PaymentInfo model)
        {
            return this._repository.Add(model);
        }

        /// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(WX_PaymentInfo model)
        {
            return this._repository.Update(model);
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public WX_PaymentInfo GetModel(int id)
        {

            return this._repository.GetModel(id);
        }

        /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
        {
            return this._repository.GetList(strWhere);
        }

        /// <summary>
		/// 获得数据列表
		/// </summary>
		public List<WX_PaymentInfo> DataTableToList(DataTable dt)
        {
            var modelList = new List<WX_PaymentInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                WX_PaymentInfo model = null;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = this._repository.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 得到wid的微信支付配置信息一个对象实体
        /// </summary>
        public WX_PaymentInfo GetModelByWid(int wid)
        {
            return this._repository.GetModelByWid(wid);
        }        
    }
}
