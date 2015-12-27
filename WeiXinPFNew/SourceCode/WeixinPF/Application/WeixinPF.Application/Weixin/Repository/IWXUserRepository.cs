using System.Data;
using WeixinPF.Model.Weixin;

namespace WeixinPF.Application.Weixin.Repository
{
    public interface IWXUserRepository
    {
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int id);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(WX_UserWeixinInfo model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(WX_UserWeixinInfo model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int id);

        /// <summary>
        /// 批量删除数据
        /// </summary>
        bool DeleteList(string idlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        WX_UserWeixinInfo DataRowToModel(DataRow row);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);

        /// <summary>
        /// 获取记录总数
        /// </summary>
        int GetRecordCount(string strWhere);

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 修改一列数据
        /// </summary>
        void UpdateField(int id, string strValue);

        /// <summary>
        /// 删除一条数据，假删除
        /// </summary>
        bool DeleteWeixin(int id);

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount);

        /// <summary>
        /// 获得用户的微信帐号信息【查询分页数据】
        /// </summary>
        DataSet GetUserWeiXinList(int pageSize, int pageIndex, string strWhere, string filedOrder,
            out int recordCount);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        string GetWeiXinToken(int id);

        /// <summary>
        /// 得到一个原始id
        /// </summary>
        string GetwxId(int id);

        /// <summary>
        /// 取该用户已经有的微信个数
        /// </summary>
        int GetUserWxNumCount(int uId);

        /// <summary>
        /// 判断该微帐号与原始Id号是否一致，如果不一致，则返回false，如果一致则返回true
        /// </summary>
        bool ExistsWidAndWxId(int id, string wxId);

        /// <summary>
        /// 判断微账号是否有效
        /// </summary>
        /// <param name="wid">微账号id</param>
        /// <returns></returns>
        bool wxCodeLegal(int wid);

        /// <summary>
        /// 判断微账号是否关闭了自动回复
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        bool wxCloseKW(int wid);

        /// <summary>
        /// 得到微信配置信息（使用SqlDataReader）
        /// </summary>
        /// <param name="rid">规则主键Id</param>
        /// <returns></returns>
        WX_UserWeixinInfo GetModel(int id);
    }
}