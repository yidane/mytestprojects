﻿using System;
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
    public class AgentRepository: IAgentRepository
    {
        public AgentRepository()
        {
        }

        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(AgentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_agent_info(");
            strSql.Append("managerId,companyName,companyInfo,agentPrice,agentPrice2,sqJine,czTotMoney,remainMony,userNum,wcodeNum,agentType,agentLevel,industry,agentArea,expiryDate,aRemark,createDate)");
            strSql.Append(" values (");
            strSql.Append("@managerId,@companyName,@companyInfo,@agentPrice,@agentPrice2,@sqJine,@czTotMoney,@remainMony,@userNum,@wcodeNum,@agentType,@agentLevel,@industry,@agentArea,@expiryDate,@aRemark,@createDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@managerId", SqlDbType.Int,4),
                    new SqlParameter("@companyName", SqlDbType.VarChar,200),
                    new SqlParameter("@companyInfo", SqlDbType.VarChar,800),
                    new SqlParameter("@agentPrice", SqlDbType.Int,4),
                    new SqlParameter("@agentPrice2", SqlDbType.Int,4),
                    new SqlParameter("@sqJine", SqlDbType.Int,4),
                    new SqlParameter("@czTotMoney", SqlDbType.Int,4),
                    new SqlParameter("@remainMony", SqlDbType.Int,4),
                    new SqlParameter("@userNum", SqlDbType.Int,4),
                    new SqlParameter("@wcodeNum", SqlDbType.Int,4),
                    new SqlParameter("@agentType", SqlDbType.Int,4),
                    new SqlParameter("@agentLevel", SqlDbType.VarChar,50),
                    new SqlParameter("@industry", SqlDbType.VarChar,200),
                    new SqlParameter("@agentArea", SqlDbType.VarChar,300),
                    new SqlParameter("@expiryDate", SqlDbType.DateTime),
                    new SqlParameter("@aRemark", SqlDbType.VarChar,1500),
                    new SqlParameter("@createDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ManagerId;
            parameters[1].Value = model.CompanyName;
            parameters[2].Value = model.CompanyInfo;
            parameters[3].Value = model.AgentPrice;
            parameters[4].Value = model.AgentPrice2;
            parameters[5].Value = model.SqJine;
            parameters[6].Value = model.CzTotalMoney;
            parameters[7].Value = model.RemainMony;
            parameters[8].Value = model.UserNum;
            parameters[9].Value = model.WcodeNum;
            parameters[10].Value = model.AgentType;
            parameters[11].Value = model.AgentLevel;
            parameters[12].Value = model.Industry;
            parameters[13].Value = model.AgentArea;
            parameters[14].Value = model.ExpiryDate;
            parameters[15].Value = model.ARemark;
            parameters[16].Value = model.CreateDate;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(AgentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_agent_info set ");
            strSql.Append("managerId=@managerId,");
            strSql.Append("companyName=@companyName,");
            strSql.Append("companyInfo=@companyInfo,");
            strSql.Append("agentPrice=@agentPrice,");
            strSql.Append("agentPrice2=@agentPrice2,");
            strSql.Append("sqJine=@sqJine,");
            strSql.Append("czTotMoney=@czTotMoney,");
            strSql.Append("remainMony=@remainMony,");
            strSql.Append("userNum=@userNum,");
            strSql.Append("wcodeNum=@wcodeNum,");
            strSql.Append("agentType=@agentType,");
            strSql.Append("agentLevel=@agentLevel,");
            strSql.Append("industry=@industry,");
            strSql.Append("agentArea=@agentArea,");
            strSql.Append("expiryDate=@expiryDate,");
            strSql.Append("aRemark=@aRemark,");
            strSql.Append("createDate=@createDate");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@managerId", SqlDbType.Int,4),
                    new SqlParameter("@companyName", SqlDbType.VarChar,200),
                    new SqlParameter("@companyInfo", SqlDbType.VarChar,800),
                    new SqlParameter("@agentPrice", SqlDbType.Int,4),
                    new SqlParameter("@agentPrice2", SqlDbType.Int,4),
                    new SqlParameter("@sqJine", SqlDbType.Int,4),
                    new SqlParameter("@czTotMoney", SqlDbType.Int,4),
                    new SqlParameter("@remainMony", SqlDbType.Int,4),
                    new SqlParameter("@userNum", SqlDbType.Int,4),
                    new SqlParameter("@wcodeNum", SqlDbType.Int,4),
                    new SqlParameter("@agentType", SqlDbType.Int,4),
                    new SqlParameter("@agentLevel", SqlDbType.VarChar,50),
                    new SqlParameter("@industry", SqlDbType.VarChar,200),
                    new SqlParameter("@agentArea", SqlDbType.VarChar,300),
                    new SqlParameter("@expiryDate", SqlDbType.DateTime),
                    new SqlParameter("@aRemark", SqlDbType.VarChar,1500),
                    new SqlParameter("@createDate", SqlDbType.DateTime),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.ManagerId;
            parameters[1].Value = model.CompanyName;
            parameters[2].Value = model.CompanyInfo;
            parameters[3].Value = model.AgentPrice;
            parameters[4].Value = model.AgentPrice2;
            parameters[5].Value = model.SqJine;
            parameters[6].Value = model.CzTotalMoney;
            parameters[7].Value = model.RemainMony;
            parameters[8].Value = model.UserNum;
            parameters[9].Value = model.WcodeNum;
            parameters[10].Value = model.AgentType;
            parameters[11].Value = model.AgentLevel;
            parameters[12].Value = model.Industry;
            parameters[13].Value = model.AgentArea;
            parameters[14].Value = model.ExpiryDate;
            parameters[15].Value = model.ARemark;
            parameters[16].Value = model.CreateDate;
            parameters[17].Value = model.Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public AgentInfo DataRowToModel(DataRow row)
        {
            var model = new AgentInfo();
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
                if (row["companyName"] != null)
                {
                    model.CompanyName = row["companyName"].ToString();
                }
                if (row["companyInfo"] != null)
                {
                    model.CompanyInfo = row["companyInfo"].ToString();
                }
                if (row["agentPrice"] != null && row["agentPrice"].ToString() != "")
                {
                    model.AgentPrice = int.Parse(row["agentPrice"].ToString());
                }
                if (row["agentPrice2"] != null && row["agentPrice2"].ToString() != "")
                {
                    model.AgentPrice2 = int.Parse(row["agentPrice2"].ToString());
                }
                if (row["sqJine"] != null && row["sqJine"].ToString() != "")
                {
                    model.SqJine = int.Parse(row["sqJine"].ToString());
                }
                if (row["czTotMoney"] != null && row["czTotMoney"].ToString() != "")
                {
                    model.CzTotalMoney = int.Parse(row["czTotMoney"].ToString());
                }
                if (row["remainMony"] != null && row["remainMony"].ToString() != "")
                {
                    model.RemainMony = int.Parse(row["remainMony"].ToString());
                }
                if (row["userNum"] != null && row["userNum"].ToString() != "")
                {
                    model.UserNum = int.Parse(row["userNum"].ToString());
                }
                if (row["wcodeNum"] != null && row["wcodeNum"].ToString() != "")
                {
                    model.WcodeNum = int.Parse(row["wcodeNum"].ToString());
                }
                if (row["agentType"] != null && row["agentType"].ToString() != "")
                {
                    model.AgentType = int.Parse(row["agentType"].ToString());
                }
                if (row["agentLevel"] != null)
                {
                    model.AgentLevel = row["agentLevel"].ToString();
                }
                if (row["industry"] != null)
                {
                    model.Industry = row["industry"].ToString();
                }
                if (row["agentArea"] != null)
                {
                    model.AgentArea = row["agentArea"].ToString();
                }
                if (row["expiryDate"] != null && row["expiryDate"].ToString() != "")
                {
                    model.ExpiryDate = DateTime.Parse(row["expiryDate"].ToString());
                }
                if (row["aRemark"] != null)
                {
                    model.ARemark = row["aRemark"].ToString();
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(row["createDate"].ToString());
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
            strSql.Append(
                "select id,managerId,companyName,companyInfo,agentPrice,agentPrice2,sqJine,czTotMoney,remainMony,userNum,wcodeNum,agentType,agentLevel,industry,agentArea,expiryDate,aRemark,createDate ");
            strSql.Append(" FROM wx_agent_info ");
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
            strSql.Append(" select  *   FROM  view_agent_list ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where id!=1 and " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AgentInfo GetAgentModel(int managerId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,managerId,companyName,companyInfo,agentPrice,agentPrice2,sqJine,czTotMoney,remainMony,userNum,wcodeNum,agentType,agentLevel,industry,agentArea,expiryDate,aRemark,createDate from wx_agent_info ");
            strSql.Append(" where managerId=@managerId");
            SqlParameter[] parameters = {
                    new SqlParameter("@managerId", SqlDbType.Int,4)
            };
            parameters[0].Value = managerId;

            var model = new AgentInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
    }
    }
