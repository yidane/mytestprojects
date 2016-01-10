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
    public class PropertyRepository: IPropertyRepository
    {
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(PropertyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_property_info(");
            strSql.Append("wid,typeId,typeName,iName,iContent,expires_in,createDate,count,categoryId,categoryName,remark)");
            strSql.Append(" values (");
            strSql.Append("@wid,@typeId,@typeName,@iName,@iContent,@expires_in,@createDate,@count,@categoryId,@categoryName,@remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@wid", SqlDbType.Int,4),
                    new SqlParameter("@typeId", SqlDbType.Int,4),
                    new SqlParameter("@typeName", SqlDbType.VarChar,100),
                    new SqlParameter("@iName", SqlDbType.VarChar,200),
                    new SqlParameter("@iContent", SqlDbType.VarChar,-1),
                    new SqlParameter("@expires_in", SqlDbType.Int,4),
                    new SqlParameter("@createDate", SqlDbType.DateTime),
                    new SqlParameter("@count", SqlDbType.Int,4),
                    new SqlParameter("@categoryId", SqlDbType.Int,4),
                    new SqlParameter("@categoryName", SqlDbType.VarChar,50),
                    new SqlParameter("@remark", SqlDbType.VarChar,1000)};
            parameters[0].Value = model.Wid;
            parameters[1].Value = model.TypeId;
            parameters[2].Value = model.typeName;
            parameters[3].Value = model.iName;
            parameters[4].Value = model.iContent;
            parameters[5].Value = model.expires_in;
            parameters[6].Value = model.createDate;
            parameters[7].Value = model.count;
            parameters[8].Value = model.categoryId;
            parameters[9].Value = model.categoryName;
            parameters[10].Value = model.remark;

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
        public bool Update(PropertyInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_property_info set ");
            strSql.Append("wid=@wid,");
            strSql.Append("typeId=@typeId,");
            strSql.Append("typeName=@typeName,");
            strSql.Append("iName=@iName,");
            strSql.Append("iContent=@iContent,");
            strSql.Append("expires_in=@expires_in,");
            strSql.Append("createDate=@createDate,");
            strSql.Append("count=@count,");
            strSql.Append("categoryId=@categoryId,");
            strSql.Append("categoryName=@categoryName,");
            strSql.Append("remark=@remark");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@wid", SqlDbType.Int,4),
                    new SqlParameter("@typeId", SqlDbType.Int,4),
                    new SqlParameter("@typeName", SqlDbType.VarChar,100),
                    new SqlParameter("@iName", SqlDbType.VarChar,200),
                    new SqlParameter("@iContent", SqlDbType.VarChar,-1),
                    new SqlParameter("@expires_in", SqlDbType.Int,4),
                    new SqlParameter("@createDate", SqlDbType.DateTime),
                    new SqlParameter("@count", SqlDbType.Int,4),
                    new SqlParameter("@categoryId", SqlDbType.Int,4),
                    new SqlParameter("@categoryName", SqlDbType.VarChar,50),
                    new SqlParameter("@remark", SqlDbType.VarChar,1000),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.Wid;
            parameters[1].Value = model.TypeId;
            parameters[2].Value = model.typeName;
            parameters[3].Value = model.iName;
            parameters[4].Value = model.iContent;
            parameters[5].Value = model.expires_in;
            parameters[6].Value = model.createDate;
            parameters[7].Value = model.count;
            parameters[8].Value = model.categoryId;
            parameters[9].Value = model.categoryName;
            parameters[10].Value = model.remark;
            parameters[11].Value = model.Id;

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
		public PropertyInfo DataRowToModel(DataRow row)
        {
            var model = new PropertyInfo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.Id = int.Parse(row["id"].ToString());
                }
                if (row["wid"] != null && row["wid"].ToString() != "")
                {
                    model.Wid = int.Parse(row["wid"].ToString());
                }
                if (row["typeId"] != null && row["typeId"].ToString() != "")
                {
                    model.TypeId = int.Parse(row["typeId"].ToString());
                }
                if (row["typeName"] != null)
                {
                    model.typeName = row["typeName"].ToString();
                }
                if (row["iName"] != null)
                {
                    model.iName = row["iName"].ToString();
                }
                if (row["iContent"] != null)
                {
                    model.iContent = row["iContent"].ToString();
                }
                if (row["expires_in"] != null && row["expires_in"].ToString() != "")
                {
                    model.expires_in = int.Parse(row["expires_in"].ToString());
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
                }
                if (row["count"] != null && row["count"].ToString() != "")
                {
                    model.count = int.Parse(row["count"].ToString());
                }
                if (row["categoryId"] != null && row["categoryId"].ToString() != "")
                {
                    model.categoryId = int.Parse(row["categoryId"].ToString());
                }
                if (row["categoryName"] != null)
                {
                    model.categoryName = row["categoryName"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
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
            strSql.Append("select id,wid,typeId,typeName,iName,iContent,expires_in,createDate,count,categoryId,categoryName,remark ");
            strSql.Append(" FROM wx_property_info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 该微帐号是否存在记录
        /// </summary>
        public bool ExistsWid(int wid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_property_info");
            strSql.Append(" where wid=@wid");
            SqlParameter[] parameters = {
                    new SqlParameter("@wid", SqlDbType.Int,4)
            };
            parameters[0].Value = wid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 该微帐号是否存在记录
        /// </summary>
        public bool ExistsWid(int wid, string iName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_property_info");
            strSql.Append(" where wid=@wid and iName=@iName");
            SqlParameter[] parameters = {
                    new SqlParameter("@wid", SqlDbType.Int,4),
                    new SqlParameter("@iName", SqlDbType.VarChar,200)
            };
            parameters[0].Value = wid;
            parameters[1].Value = iName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PropertyInfo GetModelByIName(int wid, string iName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,wid,typeId,typeName,iName,iContent,expires_in,createDate,count,categoryId,categoryName,remark from wx_property_info ");
            strSql.Append(" where wid=@wid and iName=@iName");
            SqlParameter[] parameters = {
                    new SqlParameter("@wid", SqlDbType.Int,4),
                    new SqlParameter("@iName", SqlDbType.VarChar,200)
            };
            parameters[0].Value = wid;
            parameters[1].Value = iName;

            var model = new PropertyInfo();
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
