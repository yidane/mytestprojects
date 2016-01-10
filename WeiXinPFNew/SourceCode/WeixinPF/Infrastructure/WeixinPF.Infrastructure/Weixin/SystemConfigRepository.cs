using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.DBUtility;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Infrastructure.Weixin
{
    public class SystemConfigRepository: ISystemConfigRepository
    {
        /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,sysCode,sysName,sysValue,sysTypeId,sysTypeName,createDate,parentId,remark,sort_id ");
            strSql.Append(" FROM wx_sysConfig ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SystemConfigInfo DataRowToModel(DataRow row)
        {
            var model = new SystemConfigInfo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["sysCode"] != null)
                {
                    model.sysCode = row["sysCode"].ToString();
                }
                if (row["sysName"] != null)
                {
                    model.sysName = row["sysName"].ToString();
                }
                if (row["sysValue"] != null)
                {
                    model.sysValue = row["sysValue"].ToString();
                }
                if (row["sysTypeId"] != null && row["sysTypeId"].ToString() != "")
                {
                    model.sysTypeId = int.Parse(row["sysTypeId"].ToString());
                }
                if (row["sysTypeName"] != null)
                {
                    model.sysTypeName = row["sysTypeName"].ToString();
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
                }
                if (row["parentId"] != null && row["parentId"].ToString() != "")
                {
                    model.parentId = int.Parse(row["parentId"].ToString());
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 通过配置节点编码找对应的值
        /// </summary>
        public string GetConfigValue(string sysCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 sysValue from  wx_sysConfig where sysCode=@sysCode ");
            SqlParameter[] parameters = {
                    new SqlParameter("@sysCode", SqlDbType.VarChar,50)
            };
            parameters[0].Value = sysCode;

            SqlDataReader sr = DbHelperSQL.ExecuteReader(strSql.ToString(), parameters);
            string ret = "";
            while (sr.Read())
            {
                ret = sr["sysValue"].ToString();
            }
            sr.Close();

            return ret;
        }

        /// <summary>
        /// 修改值
        /// </summary>
        /// <param name="sysCode"></param>
        /// <param name="sysValue"></param>
        /// <returns></returns>
        public bool EditSysValue(string sysCode, string sysValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_sysConfig set  sysValue=@sysValue where sysCode=@sysCode");
            SqlParameter[] parameters = {
                    new SqlParameter("@sysValue", SqlDbType.VarChar,500),
                    new SqlParameter("@sysCode", SqlDbType.VarChar,50)};

            parameters[0].Value = sysValue;
            parameters[1].Value = sysCode;

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
    }
}
