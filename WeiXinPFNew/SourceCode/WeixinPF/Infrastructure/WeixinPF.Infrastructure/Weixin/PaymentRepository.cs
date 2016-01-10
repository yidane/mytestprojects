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
    public class PaymentRepository: IPaymentRepository
    {
        public PaymentRepository()
        {
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PaymentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_payment_wxpay(");
            strSql.Append("wid,mch_id,paykey,certInfoPath,cerInfoPwd,remark,quicklyFH,createDate)");
            strSql.Append(" values (");
            strSql.Append("@wid,@mch_id,@paykey,@certInfoPath,@cerInfoPwd,@remark,@quicklyFH,@createDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@wid", SqlDbType.Int,4),
                    new SqlParameter("@mch_id", SqlDbType.VarChar,200),
                    new SqlParameter("@paykey", SqlDbType.VarChar,500),
                    new SqlParameter("@certInfoPath", SqlDbType.VarChar,1000),
                    new SqlParameter("@cerInfoPwd", SqlDbType.VarChar,100),
                    new SqlParameter("@remark", SqlDbType.VarChar,2000),
                    new SqlParameter("@quicklyFH", SqlDbType.Bit,1),
                    new SqlParameter("@createDate", SqlDbType.DateTime)};
            parameters[0].Value = model.wid;
            parameters[1].Value = model.mch_id;
            parameters[2].Value = model.paykey;
            parameters[3].Value = model.certInfoPath;
            parameters[4].Value = model.cerInfoPwd;
            parameters[5].Value = model.remark;
            parameters[6].Value = model.quicklyFH;
            parameters[7].Value = model.createDate;

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
        public bool Update(PaymentInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_payment_wxpay set ");
            strSql.Append("wid=@wid,");
            strSql.Append("mch_id=@mch_id,");
            strSql.Append("paykey=@paykey,");
            strSql.Append("certInfoPath=@certInfoPath,");
            strSql.Append("cerInfoPwd=@cerInfoPwd,");
            strSql.Append("remark=@remark,");
            strSql.Append("quicklyFH=@quicklyFH,");
            strSql.Append("createDate=@createDate");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@wid", SqlDbType.Int,4),
                    new SqlParameter("@mch_id", SqlDbType.VarChar,200),
                    new SqlParameter("@paykey", SqlDbType.VarChar,500),
                    new SqlParameter("@certInfoPath", SqlDbType.VarChar,1000),
                    new SqlParameter("@cerInfoPwd", SqlDbType.VarChar,100),
                    new SqlParameter("@remark", SqlDbType.VarChar,2000),
                    new SqlParameter("@quicklyFH", SqlDbType.Bit,1),
                    new SqlParameter("@createDate", SqlDbType.DateTime),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.wid;
            parameters[1].Value = model.mch_id;
            parameters[2].Value = model.paykey;
            parameters[3].Value = model.certInfoPath;
            parameters[4].Value = model.cerInfoPwd;
            parameters[5].Value = model.remark;
            parameters[6].Value = model.quicklyFH;
            parameters[7].Value = model.createDate;
            parameters[8].Value = model.id;

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
        public PaymentInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,wid,mch_id,paykey,certInfoPath,cerInfoPwd,remark,quicklyFH,createDate from wx_payment_wxpay ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            var model = new PaymentInfo();
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PaymentInfo DataRowToModel(DataRow row)
        {
            var model = new PaymentInfo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["wid"] != null && row["wid"].ToString() != "")
                {
                    model.wid = int.Parse(row["wid"].ToString());
                }
                if (row["mch_id"] != null)
                {
                    model.mch_id = row["mch_id"].ToString();
                }
                if (row["paykey"] != null)
                {
                    model.paykey = row["paykey"].ToString();
                }
                if (row["certInfoPath"] != null)
                {
                    model.certInfoPath = row["certInfoPath"].ToString();
                }
                if (row["cerInfoPwd"] != null)
                {
                    model.cerInfoPwd = row["cerInfoPwd"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["quicklyFH"] != null && row["quicklyFH"].ToString() != "")
                {
                    if ((row["quicklyFH"].ToString() == "1") || (row["quicklyFH"].ToString().ToLower() == "true"))
                    {
                        model.quicklyFH = true;
                    }
                    else
                    {
                        model.quicklyFH = false;
                    }
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
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
            strSql.Append("select id,wid,mch_id,paykey,certInfoPath,cerInfoPwd,remark,quicklyFH,createDate ");
            strSql.Append(" FROM wx_payment_wxpay ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PaymentInfo GetModelByWid(int wid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 *  from wx_payment_wxpay ");
            strSql.Append(" where wid=@wid");
            SqlParameter[] parameters = {
                    new SqlParameter("@wid", SqlDbType.Int,4)
            };
            parameters[0].Value = wid;

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
