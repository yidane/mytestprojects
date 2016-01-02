using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Common;
using WeixinPF.DBUtility;
using WeixinPF.Model.Article;

namespace WeixinPF.Infrastructure.Article
{
    public class ArticleImageSizeRepository
    {
        private string databaseprefix; //数据库表名前缀
        public ArticleImageSizeRepository(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        /// <summary>
        /// 查找不存在的图片并删除已删除的图片及数据
        /// </summary>
        public void DeleteList(SqlConnection conn, SqlTransaction trans, List<ArticleImagesSizeInfo> models, int category_id)
        {
            StringBuilder idList = new StringBuilder();
            if (models != null)
            {
                foreach (var modelt in models)
                {
                    if (modelt.id > 0)
                    {
                        idList.Append(modelt.id + ",");
                    }
                }
            }
            string id_list = Utils.DelLastChar(idList.ToString(), ",");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,height,width,category_id from " + databaseprefix + "article_images_size where category_id=" + category_id);
            if (!string.IsNullOrEmpty(id_list))
            {
                strSql.Append(" and id not in(" + id_list + ")");
            }
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DbHelperSQL.ExecuteSql(conn, trans, "delete from " + databaseprefix + "article_images_size where id=" + dr["id"].ToString()); //删除数据库           
            }
        }
    }
}
