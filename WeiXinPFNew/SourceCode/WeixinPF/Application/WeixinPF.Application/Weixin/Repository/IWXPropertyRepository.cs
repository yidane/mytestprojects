using System.Data;
using WeixinPF.Model.Weixin;

namespace WeixinPF.Application.Weixin.Repository
{
    public interface IWXPropertyRepository
    {
        int Add(WX_PropertyInfo model);
        bool Update(WX_PropertyInfo model);
        WX_PropertyInfo DataRowToModel(DataRow row);
        DataSet GetList(string strWhere);
        bool ExistsWid(int wid);
        bool ExistsWid(int wid, string iName);
        WX_PropertyInfo GetModelByIName(int wid, string iName);
    }
}