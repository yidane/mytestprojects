using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Model.WeiXin.Message;

namespace WeixinPF.Application.Weixin.Message.Repository
{
    public interface IRequestRuleRepository
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
        int Add(RequestRule model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(RequestRule model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int id);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool DeleteList(string idlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        RequestRule GetModel(int id);



        /// <summary>
        /// 获得数据列表
        /// </summary>
        List<RequestRule> GetList(string strWhere);

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        List<RequestRule> GetList(int top, string strWhere, string filedOrder);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        List<RequestRule> GetModelList(string strWhere);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        List<RequestRule> GetAllList();

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        int GetRecordCount(string strWhere);

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        List<RequestRule> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);



        /// <summary>
        /// 获得查询分页数据(文本和语音)
        /// </summary>
        List<RequestRule> GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount);


        /// <summary>
        /// 获得查询分页数据(图文的关键词列表)
        /// </summary>
        RequestRule GetTWKeyWordList(int pageSize, int pageIndex, string strWhere, string filedOrder,
            out int recordCount);



        /// <summary>
        ///关注时，非图文内容，则删除该条微信回复的规则，并且将其对应的内容删除
        /// </summary>
        bool DeleteRule(int id);

        /// <summary>
        /// 通过试图获得回复规则以及规则对应的内容
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        List<RequestRule> GetRuleContent(string strWhere);

        /// <summary>
        /// 通过试图获得回复规则以及规则对应的内容
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        List<RequestRule> GetRuleContent(int top, string strWhere);


        /// <summary>
        /// 功能模块添加图文回复规则【关键词匹配为精确查询】
        /// </summary>
        /// <param name="wid">微帐号主键</param>
        /// <param name="modelName">模块名称</param>
        /// <param name="modelId">模块的主键</param>
        /// <param name="keyword">关键词</param>
        void AddModeltxtPicRule(int wid, string modelName, int modelId, string keyword);


        #region 微信前端使用，需要高效率

        /// <summary>
        /// 得到回复规则的主键Id
        /// </summary>
        /// <param name="wid">微帐号主键Id</param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        int GetRuleId(int wid, string strWhere);

        int GetRuleIdAndResponseType(int wid, string strWhere, out int responseType);

        /// <summary>
        /// 关键词查询，找到该规则对应的主键ID,使用存储过程
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        int GetRuleIdByKeyWords(int wid, string keywords, out int responseType, out string modelFunctionName,
           out int modelFunctionId);


        #endregion
    }
}
