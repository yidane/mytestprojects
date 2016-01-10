using System.Collections.Generic;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Application.Weixin.Service
{
    public class WxUserService
    {
        private readonly IUserRepository _repository;

        public WxUserService(IUserRepository repository)
        {
            _repository = repository;
        }

        #region  BasicMethod

        /// <summary>
        ///     得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return _repository.GetMaxId();
        }

        /// <summary>
        ///     是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return _repository.Exists(id);
        }

        /// <summary>
        ///     增加一条数据
        /// </summary>
        public int Add(AppInfo model)
        {
            return _repository.Add(model);
        }

        /// <summary>
        ///     更新一条数据
        /// </summary>
        public bool Update(AppInfo model)
        {
            return _repository.Update(model);
        }

        /// <summary>
        ///     删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        /// <summary>
        ///     删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return _repository.DeleteList(idlist);
        }

        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public AppInfo GetModel(int id)
        {
            return _repository.GetModel(id);
        }


        /// <summary>
        ///     获得数据列表
        /// </summary>
        public List<AppInfo> GetList(string strWhere)
        {
            return _repository.GetList(strWhere);
        }

        /// <summary>
        ///     获得前几行数据
        /// </summary>
        public List<AppInfo> GetList(int Top, string strWhere, string filedOrder)
        {
            return _repository.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        ///     获得数据列表
        /// </summary>
        public List<AppInfo> GetModelList(string strWhere)
        {
            return _repository.GetList(strWhere);
        }

        /// <summary>
        ///     获得数据列表
        /// </summary>
        public List<AppInfo> GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        ///     分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return _repository.GetRecordCount(strWhere);
        }

        /// <summary>
        ///     分页获取数据列表
        /// </summary>
        public List<AppInfo> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return _repository.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        ///     分页获取数据列表
        /// </summary>
        /// <summary>
        ///     修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            _repository.UpdateField(id, strValue);
        }


        /// <summary>
        ///     删除一条数据,假删除
        /// </summary>
        public bool DeleteWeixin(int id)
        {
            return _repository.DeleteWeixin(id);
        }

        /// <summary>
        ///     获得查询分页数据
        /// </summary>
        public List<AppInfo> GetList(int pageSize, int pageIndex, string strWhere, string filedOrder,
            out int recordCount)
        {
            return _repository.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }


        /// <summary>
        ///     获得用户的微信帐号信息【查询分页数据】
        /// </summary>
        public List<AppInfo> GetUserWeiXinList(int pageSize, int pageIndex, string strWhere, string filedOrder,
            out int recordCount)
        {
            return _repository.GetUserWeiXinList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }


        /// <summary>
        ///     得到一个token
        /// </summary>
        public string GetWeiXinToken(int id)
        {
            return _repository.GetWeiXinToken(id);
        }

        /// <summary>
        ///     得到一个原始id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetwxId(int id)
        {
            return _repository.GetwxId(id);
        }


        /// <summary>
        ///     取该用户已经有的微信个数
        ///     1e2124dd04e11d01b9df2865f85944be
        /// </summary>
        public int GetUserWxNumCount(int uId)
        {
            return _repository.GetUserWxNumCount(uId);
        }

        /// <summary>
        ///     判断微账号是否有效
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        public bool WxCodeLegal(int wid)
        {
            return _repository.WxCodeLegal(wid);
        }

        #endregion  ExtensionMethod
    }
}