using System.Data;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Application.Weixin.Repository
{
    public interface ISystemConfigRepository
    {
        DataSet GetList(string strWhere);
        SystemConfigInfo DataRowToModel(DataRow row);
        string GetConfigValue(string sysCode);
        bool EditSysValue(string sysCode, string sysValue);
    }
}