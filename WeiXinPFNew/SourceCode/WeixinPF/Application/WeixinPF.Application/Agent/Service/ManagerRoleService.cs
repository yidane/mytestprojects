using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Agent.Repository;
using WeixinPF.Model.Agent;

namespace WeixinPF.Application.Agent.Service
{
    public class ManagerRoleService
    {
        private readonly IManagerRoleRepository _repository;
        public ManagerRoleService(IManagerRoleRepository repository)
        {
            this._repository = repository;
            //dal = new DAL.manager(siteConfig.sysdatabaseprefix);
        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return this._repository.Exists(id);
        }
        public bool Exists(int id, int agentId)
        {
            return this._repository.Exists(id, agentId);
        }
        /// <summary>
        /// 检查是否有权限
        /// </summary>
        public bool Exists(int role_id, string nav_name, string action_type)
        {
            var model = this._repository.GetModel(role_id);
            if (model != null)
            {
                if (model.role_type == 1)
                {
                    return true;
                }
                var modelt = model.manager_role_values.Find(p => p.nav_name == nav_name && p.action_type == action_type);
                if (modelt != null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 返回角色名称
        /// </summary>
        public string GetTitle(int id)
        {
            return this._repository.GetTitle(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Manager_RoleInfo model)
        {
            return this._repository.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Manager_RoleInfo model)
        {
            return this._repository.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return this._repository.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Manager_RoleInfo GetModel(int id)
        {
            return this._repository.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return this._repository.GetList(strWhere);
        }
        #endregion  Method
    }
}
