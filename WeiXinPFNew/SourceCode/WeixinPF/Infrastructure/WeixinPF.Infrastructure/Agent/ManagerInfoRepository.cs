using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WeixinPF.Application.Agent;
using WeixinPF.Application.Agent.Repository;
using WeixinPF.Common;
using WeixinPF.DBUtility;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Model.Agent;

namespace WeixinPF.Infrastructure.Agent
{
    public class ManagerInfoRepository : IManagerInfoRepository
    {
        private readonly EFRepository<ManagerInfo> _efRepository = new EFRepository<ManagerInfo>(new AgentDbContext());

        #region 基本方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return _efRepository.Get(item => item.Id == id).Any();
        }

        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool Exists(string userName)
        {
            return _efRepository.Get(item => item.UserName.Equals(userName)).Any();
        }

        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool Exists(string userName, string email)
        {
            return _efRepository.Get(item => item.UserName.Equals(userName) && item.Email.Equals(email)).Any();
        }


        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string userName)
        {
            var managerInfo = _efRepository.Get(item => item.UserName.Equals(userName)).FirstOrDefault();
            return managerInfo != null ? managerInfo.Salt : string.Empty;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ManagerInfo model)
        {
            _efRepository.Add(model);
            return model.Id;
        }

        //bool IRepository<ManagerInfo>.Add(ManagerInfo entity)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ManagerInfo model)
        {
            if (model == null)
                return false;

            _efRepository.Update(model);
            return true;
        }

        /// <summary>
        /// 删除一条数据管理员/代理商/用户
        /// 如果下面有子用户，则无法删除
        /// </summary>
        public bool Delete(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from " + "manager ");
            strSql.Append(" where id=@id and (select count(id) from dt_manager where agentid=@id)<=0");
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            var rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除一条数据管理员/代理商/用户
        /// 如果下面有子用户，则无法删除
        /// </summary>
        public bool DeleteAgent(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from " + "manager ");
            strSql.Append(" where id=@id and (select count(id) from dt_manager where agentid=@id)<=0;");
            strSql.Append("delete from wx_agent_info where managerId=@id");
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            var rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ManagerInfo GetModel(int id)
        {
            return _efRepository.Get(item => item.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public ManagerInfo GetModel(string userName, string password)
        {
            return _efRepository.Get(item => item.UserName.Equals(userName) &&
                                             item.Password.Equals(password) &&
                                             item.IsLock == 0)
                                .FirstOrDefault();
        }

        public ManagerInfo GetModel(string userName, string password, bool is_encrypt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top);
            }
            strSql.Append(
                " id,role_id,role_type,user_name,password,salt,real_name,telephone,email,is_lock,add_time,wxNum,agentId,reg_ip,qq,province,city,county,remark,sort_id,agentLevel ");
            strSql.Append(" FROM " + "manager ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (!string.IsNullOrEmpty(filedOrder))
            {
                strSql.Append(" order by " + filedOrder);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            var strSql = new StringBuilder();
            strSql.Append(
                "select   hasnum=(select  count(1) as hn from wx_userweixin where isDelete=0 and    uid=m.id), m.*  FROM " +
                "manager m");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where id!=1 and " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return
                DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(),
                    filedOrder));
        }


        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append("select count(1) FROM dt_manager ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }

        public int GetXiaXianManagerCount(int managerId)
        {
            throw new NotImplementedException();
        }

        #endregion  Method
    }
}