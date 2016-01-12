using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Agent;
using WeixinPF.Application.Agent.Repository;
using WeixinPF.DBUtility;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Model.Agent;

namespace WeixinPF.Infrastructure.Agent
{
    public class ManagerRoleRepository : IManagerRoleRepository
    {
        private readonly EFRepository<ManagerRoleInfo> _efRepository = new EFRepository<ManagerRoleInfo>(new WeiXinDbContext());
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return _efRepository.Get(item => item.Id.Equals(id)).Any();
        }

        /// <summary>
        /// 该角色是否为自己创建的
        /// </summary>
        public bool Exists(int id, int agentId)
        {
            return _efRepository.Get(item => item.Id.Equals(id) && item.AgentId.Equals(agentId)).Any();
        }


        /// <summary>
        /// 返回角色名称
        /// </summary>
        public string GetTitle(int id)
        {
            var managerRole = _efRepository.Get(item => item.Id.Equals(id)).FirstOrDefault();
            return managerRole != null ? managerRole.RoleName : string.Empty;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ManagerRoleInfo model)
        {
            _efRepository.Add(model);

            return model.Id;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ManagerRoleInfo model)
        {
            //先删除旧数据，再插入操作

            return false;
        }

        /// <summary>
        /// 删除一条数据，及子表所有相关数据
        /// </summary>
        public bool Delete(int id)
        {
            //List<CommandInfo> sqllist = new List<CommandInfo>();
            ////删除管理角色权限
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("delete from " + databaseprefix + "manager_role_value ");
            //strSql.Append(" where role_id=@role_id");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@role_id", SqlDbType.Int,4)};
            //parameters[0].Value = id;
            //CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            //sqllist.Add(cmd);
            ////删除管理角色
            //StringBuilder strSql2 = new StringBuilder();
            //strSql2.Append("delete from " + databaseprefix + "manager_role ");
            //strSql2.Append(" where id=@id");
            //SqlParameter[] parameters2 = {
            //        new SqlParameter("@id", SqlDbType.Int,4)};
            //parameters2[0].Value = id;
            //cmd = new CommandInfo(strSql2.ToString(), parameters2);
            //sqllist.Add(cmd);

            //int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            //if (rowsAffected > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            return false;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ManagerRoleInfo GetModel(int id)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select  top 1 id,role_name,role_type,is_sys,agentId from " + databaseprefix + "manager_role ");
            //strSql.Append(" where id=@id");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@id", SqlDbType.Int,4)};
            //parameters[0].Value = id;

            //ManagerRoleInfo model = new ManagerRoleInfo();
            //DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    #region 父表信息
            //    if (ds.Tables[0].Rows[0]["id"].ToString() != "")
            //    {
            //        model.Id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            //    }
            //    model.RoleName = ds.Tables[0].Rows[0]["role_name"].ToString();
            //    if (ds.Tables[0].Rows[0]["role_type"].ToString() != "")
            //    {
            //        model.RoleType = int.Parse(ds.Tables[0].Rows[0]["role_type"].ToString());
            //    }
            //    if (ds.Tables[0].Rows[0]["is_sys"].ToString() != "")
            //    {
            //        model.IsSys = int.Parse(ds.Tables[0].Rows[0]["is_sys"].ToString());
            //    }
            //    if (ds.Tables[0].Rows[0]["agentId"].ToString() != "")
            //    {
            //        model.AgentId = int.Parse(ds.Tables[0].Rows[0]["agentId"].ToString());
            //    }

            //    #endregion

            //    #region 子表信息
            //    StringBuilder strSql2 = new StringBuilder();
            //    strSql2.Append("select id,role_id,nav_name,action_type from dt_manager_role_value ");
            //    strSql2.Append(" where role_id=@role_id");
            //    SqlParameter[] parameters2 = {
            //        new SqlParameter("@role_id", SqlDbType.Int,4)};
            //    parameters2[0].Value = id;
            //    DataSet ds2 = DbHelperSQL.Query(strSql2.ToString(), parameters2);
            //    if (ds2.Tables[0].Rows.Count > 0)
            //    {
            //        List<ManagerRoleValueInfo> models = new List<ManagerRoleValueInfo>();
            //        ManagerRoleValueInfo modelt;
            //        foreach (DataRow dr in ds2.Tables[0].Rows)
            //        {
            //            modelt = new ManagerRoleValueInfo();
            //            if (dr["id"].ToString() != "")
            //            {
            //                modelt.Id = int.Parse(dr["id"].ToString());
            //            }
            //            if (dr["role_id"].ToString() != "")
            //            {
            //                modelt.RoleId = int.Parse(dr["role_id"].ToString());
            //            }
            //            modelt.NavName = dr["nav_name"].ToString();
            //            modelt.ActionType = dr["action_type"].ToString();
            //            models.Add(modelt);
            //        }
            //        model.ManagerRoleValues = models;
            //    }
            //    #endregion

            //    return model;
            //}
            //else
            //{
            //    return null;
            //}

            return _efRepository.Get(item => item.Id.Equals(id)).FirstOrDefault();
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select id,role_name,role_type,is_sys,agentId ");
            //strSql.Append(" FROM " + databaseprefix + "manager_role ");
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            //return DbHelperSQL.Query(strSql.ToString());

            return null;
        }
    }
}
