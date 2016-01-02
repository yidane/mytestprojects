using System.Data;
using WeixinPF.Model.Weixin;

namespace WeixinPF.Application.Weixin.Repository
{
    public interface IWXPaymentRepository
    {
        int Add(WX_PaymentInfo model);
        bool Update(WX_PaymentInfo model);
        WX_PaymentInfo GetModel(int id);
        WX_PaymentInfo DataRowToModel(DataRow row);
        DataSet GetList(string strWhere);
        WX_PaymentInfo GetModelByWid(int wid);
    }
}