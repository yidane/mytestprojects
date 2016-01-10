using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.BaseRepository;
using WeixinPF.Model.Agent;

namespace WeixinPF.Application.Agent.Repository
{
    public interface IManagerRoleRepository
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int id);

        /// <summary>
        /// 该角色是否为自己创建的
        /// </summary>
        bool Exists(int id, int agentId);

        /// <summary>
        /// 返回角色名称
        /// </summary>
        string GetTitle(int id);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(ManagerRoleInfo model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(ManagerRoleInfo model);

        /// <summary>
        /// 删除一条数据，及子表所有相关数据
        /// </summary>
        bool Delete(int id);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ManagerRoleInfo GetModel(int id);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
    }
}
