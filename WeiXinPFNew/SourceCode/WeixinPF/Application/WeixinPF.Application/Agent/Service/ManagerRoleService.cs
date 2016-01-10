using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Agent.Repository;
using WeixinPF.Common;
using WeixinPF.Model.Agent;

namespace WeixinPF.Application.Agent.Service
{
    public class ManagerRoleService
    {
        private readonly IManagerRoleRepository _repository;
        public ManagerRoleService()
        {
            this._repository = DependencyManager.Resolve<IManagerRoleRepository>();
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
                if (model.RoleType == 1)
                {
                    return true;
                }


                var modelt = model.ManagerRoleValues.Where(p => p.NavName == nav_name && p.ActionType == action_type);

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
        public int Add(ManagerRoleInfo model)
        {
            return this._repository.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ManagerRoleInfo model)
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
        public ManagerRoleInfo GetModel(int id)
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
