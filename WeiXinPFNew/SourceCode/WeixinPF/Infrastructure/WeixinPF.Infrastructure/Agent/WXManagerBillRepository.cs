using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Agent.Repository;
using WeixinPF.Common;
using WeixinPF.DBUtility;
using WeixinPF.Model.Agent;

namespace WeixinPF.Infrastructure.Agent
{
    public class WXManagerBillRepository: IWXManagerBillRepository
    {
        public WXManagerBillRepository()
        {
        }

        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ManagerBillInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_manager_bill(");
            strSql.Append("managerId,moneyType,billMoney,billUsed,operPersonId,operDate,remark)");
            strSql.Append(" values (");
            strSql.Append("@managerId,@moneyType,@billMoney,@billUsed,@operPersonId,@operDate,@remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@managerId", SqlDbType.Int,4),
                    new SqlParameter("@moneyType", SqlDbType.VarChar,30),
                    new SqlParameter("@billMoney", SqlDbType.Int,4),
                    new SqlParameter("@billUsed", SqlDbType.VarChar,500),
                    new SqlParameter("@operPersonId", SqlDbType.Int,4),
                    new SqlParameter("@operDate", SqlDbType.DateTime),
                    new SqlParameter("@remark", SqlDbType.VarChar,1500)};
            parameters[0].Value = model.ManagerId;
            parameters[1].Value = model.MoneyType;
            parameters[2].Value = model.BillMoney;
            parameters[3].Value = model.BillUsed;
            parameters[4].Value = model.OperPersonId;
            parameters[5].Value = model.OperDate;
            parameters[6].Value = model.Remark;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ManagerBillInfo DataRowToModel(DataRow row)
        {
            var model = new ManagerBillInfo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.Id = int.Parse(row["id"].ToString());
                }
                if (row["managerId"] != null && row["managerId"].ToString() != "")
                {
                    model.ManagerId = int.Parse(row["managerId"].ToString());
                }
                if (row["moneyType"] != null)
                {
                    model.MoneyType = row["moneyType"].ToString();
                }
                if (row["billMoney"] != null && row["billMoney"].ToString() != "")
                {
                    model.BillMoney = int.Parse(row["billMoney"].ToString());
                }
                if (row["billUsed"] != null)
                {
                    model.BillUsed = row["billUsed"].ToString();
                }
                if (row["operPersonId"] != null && row["operPersonId"].ToString() != "")
                {
                    model.OperPersonId = int.Parse(row["operPersonId"].ToString());
                }
                if (row["operDate"] != null && row["operDate"].ToString() != "")
                {
                    model.OperDate = DateTime.Parse(row["operDate"].ToString());
                }
                if (row["remark"] != null)
                {
                    model.Remark = row["remark"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,managerId,moneyType,billMoney,billUsed,operPersonId,operDate,remark ");
            strSql.Append(" FROM wx_manager_bill ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,(select top 1 [user_name] from dt_manager  m where m.id=b.managerId ) as agentName,(select top 1 real_name from dt_manager  m where m.id=b.managerId ) as agentreal_name,(select top 1 [user_name] from dt_manager  m where m.id=b.operPersonId ) as operPersonName from wx_manager_bill  b ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where  1=1 " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
    }
}
