using System.Data;
using WeixinPF.Model.Weixin;

namespace WeixinPF.Application.Weixin.Repository
{
    public interface IWXIndustryDefaultModuleRepository
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        WX_IndustryDefaultModuleInfo DataRowToModel(DataRow row);
    }
}