using System.Data;
using WeixinPF.Application.BaseRepository;
using WeixinPF.Model.Agent;

namespace WeixinPF.Application.Agent.Repository
{
    public interface IManagerInfoRepository //: IRepository<ManagerInfo>
    {
        bool Exists(int id);
        bool Exists(string userName);

        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        bool Exists(string userName, string email);

        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        string GetSalt(string userName);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(ManagerInfo model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(ManagerInfo model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int id);

        /// <summary>
        /// 删除一条数据(代理商)
        /// </summary>
        bool DeleteAgent(int managerid);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ManagerInfo GetModel(int id);

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        ManagerInfo GetModel(string userName, string password);

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        ManagerInfo GetModel(string userName, string password, bool is_encrypt);

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount);

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        int GetRecordCount(string strWhere);

        /// <summary>
        /// 取该管理员/代理商 的下线用户数量
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        int GetXiaXianManagerCount(int managerId);
    }
}