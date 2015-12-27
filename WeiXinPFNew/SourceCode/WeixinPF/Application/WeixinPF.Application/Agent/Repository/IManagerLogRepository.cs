using System.Data;
using WeixinPF.Model.Agent;

namespace WeixinPF.Application.Agent.Repository
{
    public interface IManagerLogRepository
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int id);

        /// <summary>
        /// 返回数据数
        /// </summary>
        int GetCount(string strWhere);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Manager_LogInfo model);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Manager_LogInfo GetModel(int id);

        /// <summary>
        /// 根据用户名返回上一次登录记录
        /// </summary>
        Manager_LogInfo GetModel(string user_name, int top_num, string action_type);
        ///// <summary>
        ///// 删除7天前的日志数据
        ///// </summary>
        int Delete(int dayCount);

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount);
    }
}