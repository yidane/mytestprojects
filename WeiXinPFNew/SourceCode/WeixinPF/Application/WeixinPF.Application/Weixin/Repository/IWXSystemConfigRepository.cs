using System.Data;
using WeixinPF.Model.Weixin;

namespace WeixinPF.Application.Weixin.Repository
{
    public interface IWXSystemConfigRepository
    {
        DataSet GetList(string strWhere);
        WX_SystemConfigInfo DataRowToModel(DataRow row);
        string GetConfigValue(string sysCode);
        bool EditSysValue(string sysCode, string sysValue);
    }
}