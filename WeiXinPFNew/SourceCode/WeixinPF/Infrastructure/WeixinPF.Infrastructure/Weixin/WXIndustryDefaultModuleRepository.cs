using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.DBUtility;
using WeixinPF.Model.Weixin;

namespace WeixinPF.Infrastructure.Weixin
{
    public class WXIndustryDefaultModuleRepository: IWXIndustryDefaultModuleRepository
    {
        public WXIndustryDefaultModuleRepository()
        {
        }

        /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,role_id,typeName,mName,isArticle,url,sort_id,createDate,remark ");
            strSql.Append(" FROM wx_industry_defaultModule ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public WX_IndustryDefaultModuleInfo DataRowToModel(DataRow row)
        {
            var model = new WX_IndustryDefaultModuleInfo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["role_id"] != null && row["role_id"].ToString() != "")
                {
                    model.role_id = int.Parse(row["role_id"].ToString());
                }
                if (row["typeName"] != null)
                {
                    model.typeName = row["typeName"].ToString();
                }
                if (row["mName"] != null)
                {
                    model.mName = row["mName"].ToString();
                }
                if (row["isArticle"] != null && row["isArticle"].ToString() != "")
                {
                    if ((row["isArticle"].ToString() == "1") || (row["isArticle"].ToString().ToLower() == "true"))
                    {
                        model.isArticle = true;
                    }
                    else
                    {
                        model.isArticle = false;
                    }
                }
                if (row["url"] != null)
                {
                    model.url = row["url"].ToString();
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
            }
            return model;
        }
    }
}
