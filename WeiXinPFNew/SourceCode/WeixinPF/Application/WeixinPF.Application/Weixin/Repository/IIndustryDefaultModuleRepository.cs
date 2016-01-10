using System.Data;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Application.Weixin.Repository
{
    public interface IIndustryDefaultModuleRepository
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        IndustryDefaultModuleInfo DataRowToModel(DataRow row);
    }
}