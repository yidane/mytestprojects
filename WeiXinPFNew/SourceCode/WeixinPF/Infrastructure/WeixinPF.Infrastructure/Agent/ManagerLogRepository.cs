using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Agent;
using WeixinPF.Application.Agent.Repository;
using WeixinPF.Common;
using WeixinPF.DBUtility;
using WeixinPF.Infrastructure.BaseRepository;
using WeixinPF.Model.Agent;

namespace WeixinPF.Infrastructure.Agent
{
    public class ManagerLogRepository : IManagerLogRepository
    {
        private readonly EFRepository<ManagerLogInfo> _efRepository = new EFRepository<ManagerLogInfo>(new AgentDbContext());

        #region 基本方法==============================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return _efRepository.Get(item => item.Id == id).Any();
        }

        /// <summary>
        /// 返回数据数
        /// </summary>
        public int GetCountByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return 0;
            return _efRepository.Get(item => item.UserName.Equals(userName)).Count();
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ManagerLogInfo model)
        {
            if (model == null)
                return 0;

            _efRepository.Add(model);
            return model.Id;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ManagerLogInfo GetModel(int id)
        {
            return _efRepository.Get(item => item.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据用户名返回上一次登录记录
        /// </summary>
        public ManagerLogInfo GetModel(string userName, int topNum, string actionType)
        {
            //int rows = GetCount("user_name='" + userName + "'");
            //if (topNum == 1)
            //{
            //    rows = 2;
            //}
            //if (rows > 1)
            //{
            //    StringBuilder strSql = new StringBuilder();
            //    strSql.Append("select top 1 id from (select top " + topNum + " id from " + databaseprefix + "manager_log where user_name=@user_name and action_type=@action_type order by id desc) as T ");
            //    strSql.Append(" order by id asc");
            //    SqlParameter[] parameters = {
            //        new SqlParameter("@user_name", SqlDbType.NVarChar,100),
            //        new SqlParameter("@action_type", SqlDbType.NVarChar,100)};
            //    parameters[0].Value = userName;
            //    parameters[1].Value = actionType;

            //    object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            //    if (obj != null)
            //    {
            //        return GetModel(Convert.ToInt32(obj));
            //    }
            //}
            //return null;

            return null;
        }

        ///// <summary>
        ///// 删除7天前的日志数据
        ///// </summary>
        public int Delete(int dayCount)
        {
            //TODO:删除方法未实现
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + "manager_log ");
            strSql.Append(" where DATEDIFF(day, add_time, getdate()) > " + dayCount);

            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int top, string strWhere, string filedOrder)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select ");
            //if (Top > 0)
            //{
            //    strSql.Append(" top " + Top.ToString());
            //}
            //strSql.Append(" id,user_id,user_name,action_type,remark,user_ip,add_time ");
            //strSql.Append(" FROM " + databaseprefix + "manager_log ");
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            //strSql.Append(" order by " + filedOrder);
            //return DbHelperSQL.Query(strSql.ToString());

            return null;
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select * FROM " + databaseprefix + "manager_log");
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            //recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            //return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));

            recordCount = 0;
            return null;
        }

        #endregion


        public int GetCount(string strWhere)
        {
            throw new NotImplementedException();
        }
    }
}
