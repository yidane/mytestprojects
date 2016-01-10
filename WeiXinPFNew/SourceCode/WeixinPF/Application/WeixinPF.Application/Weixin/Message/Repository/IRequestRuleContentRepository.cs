using System.Collections.Generic;
using WeixinPF.Model.WeiXin.Message;

namespace WeixinPF.Application.Weixin.Message.Repository
{
    public interface IRequestRuleContentRepository
    {
        #region  BasicMethod

        /// <summary>
        ///     得到最大ID
        /// </summary>
        int GetMaxId();

        /// <summary>
        ///     是否存在该记录
        /// </summary>
        bool Exists(int id);

        /// <summary>
        ///     增加一条数据
        /// </summary>
        int Add(RequestRuleContent model);

        /// <summary>
        ///     更新一条数据
        /// </summary>
        bool Update(RequestRuleContent model);

        /// <summary>
        ///     删除一条数据
        /// </summary>
        bool Delete(int id);

        /// <summary>
        ///     删除一条数据
        /// </summary>
        bool DeleteList(string idlist);

        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        RequestRuleContent GetModel(int id);

        /// <summary>
        ///     获得数据列表
        /// </summary>
        List<RequestRuleContent> GetList(string strWhere);

        /// <summary>
        ///     获得前几行数据
        /// </summary>
        List<RequestRuleContent> GetList(int top, string strWhere, string filedOrder);

        /// <summary>
        ///     获得数据列表
        /// </summary>
        List<RequestRuleContent> GetModelList(string strWhere);

        /// <summary>
        ///     获得数据列表
        /// </summary>
        List<RequestRuleContent> GetAllList();

        /// <summary>
        ///     分页获取数据列表
        /// </summary>
        int GetRecordCount(string strWhere);

        /// <summary>
        ///     分页获取数据列表
        /// </summary>
        List<RequestRuleContent> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        ///     分页获取数据列表
        /// </summary>
        /// <summary>
        ///     得到回复规则的纯文本信息
        /// </summary>
        /// <param name="rid">规则主键Id</param>
        /// <returns></returns>
        string GetTxtContent(int rid);


        /// <summary>
        ///     得到回复规则的语音信息
        /// </summary>
        /// <param name="rid">规则主键Id</param>
        /// <returns></returns>
        RequestRuleContent GetMusicContent(int rid);

        /// <summary>
        ///     得到回复规则的【图文】信息
        /// </summary>
        /// <param name="rid">规则主键Id</param>
        /// <returns></returns>
        List<RequestRuleContent> GetTuWenContent(int rid);

        #endregion

        #region  ExtensionMethod

        /// <summary>
        ///     获得前几行数据
        /// </summary>
        List<RequestRuleContent> GetModelList(int Top, string strWhere, string filedOrder);

        /// <summary>
        ///     如果有该openid已经注册过会员卡信息，则拼接cardno=卡号
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        string CardnoStr(int wid, string openid);

        #endregion  ExtensionMethod
    }
}