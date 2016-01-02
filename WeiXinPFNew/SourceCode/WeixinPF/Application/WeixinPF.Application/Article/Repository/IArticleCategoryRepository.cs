using System.Data;
using System.Data.SqlClient;
using WeixinPF.Model.Article;

namespace WeixinPF.Application.Article.Repository
{
    public interface IArticleCategoryRepository
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int id);

        /// <summary>
        /// 返回类别名称
        /// </summary>
        string GetTitle(int id);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(ArticleCategoryInfo model);

        /// <summary>
        /// 修改一列数据
        /// </summary>
        void UpdateField(int id, string strValue);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(ArticleCategoryInfo model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        void Delete(int id);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ArticleCategoryInfo GetModel(int id);

        /// <summary>
        /// 得到一个对象实体(重载，带事务)
        /// </summary>
        ArticleCategoryInfo GetModel(SqlConnection conn, SqlTransaction trans, int id);

        /// <summary>
        /// 取得指定类别下的列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        DataTable GetChildList(int parent_id, int channel_id);

        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        DataTable GetList(int parent_id, int channel_id);

        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        DataTable GetList(string str);

        int GetParentId(int id);
        DataTable GetWCodeList(int wid, int parent_id, int channel_id);
    }
}