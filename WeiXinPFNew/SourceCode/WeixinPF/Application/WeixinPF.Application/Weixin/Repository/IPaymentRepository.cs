using System.Data;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Application.Weixin.Repository
{
    public interface IPaymentRepository
    {
        int Add(PaymentInfo model);
        bool Update(PaymentInfo model);
        PaymentInfo GetModel(int id);
        PaymentInfo DataRowToModel(DataRow row);
        DataSet GetList(string strWhere);
        PaymentInfo GetModelByWid(int wid);
    }
}