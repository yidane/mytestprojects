using System.Data;
using WeixinPF.Model.Agent;

namespace WeixinPF.Application.Agent.Repository
{
    public interface IWXManagerBillRepository
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(ManagerBillInfo model);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ManagerBillInfo DataRowToModel(DataRow row);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount);
    }
}