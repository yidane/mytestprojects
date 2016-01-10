using System.Data;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Application.Weixin.Repository
{
    public interface IPropertyRepository
    {
        int Add(PropertyInfo model);
        bool Update(PropertyInfo model);
        PropertyInfo DataRowToModel(DataRow row);
        DataSet GetList(string strWhere);
        bool ExistsWid(int wid);
        bool ExistsWid(int wid, string iName);
        PropertyInfo GetModelByIName(int wid, string iName);
    }
}