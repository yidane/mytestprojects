using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Model.Weixin;

namespace WeixinPF.Application.Weixin.Service
{
    public class WXUserService
    {
        private readonly IWXUserRepository _repository;

        public WXUserService(IWXUserRepository repository)
        {
            this._repository = repository;
        }

        #region  BasicMethod

            /// <summary>
            /// 得到最大ID
            /// </summary>
        public int GetMaxId()
        {
            return this._repository.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return this._repository.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(WX_UserWeixinInfo model)
        {
            return this._repository.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(WX_UserWeixinInfo model)
        {
            return this._repository.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return this._repository.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return this._repository.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WX_UserWeixinInfo GetModel(int id)
        {

            return this._repository.GetModel(id);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return this._repository.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this._repository.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<WX_UserWeixinInfo> GetModelList(string strWhere)
        {
            DataSet ds = this._repository.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<WX_UserWeixinInfo> DataTableToList(DataTable dt)
        {
            var modelList = new List<WX_UserWeixinInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                WX_UserWeixinInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = this._repository.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return this._repository.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this._repository.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod




        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            this._repository.UpdateField(id, strValue);
        }



        /// <summary>
        /// 删除一条数据,假删除
        /// </summary>
        public bool DeleteWeixin(int id)
        {
            return this._repository.DeleteWeixin(id);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return this._repository.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }


        /// <summary>
        /// 获得用户的微信帐号信息【查询分页数据】
        /// </summary>
        public DataSet GetUserWeiXinList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return this._repository.GetUserWeiXinList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }


        /// <summary>
        /// 得到一个token
        /// </summary>
        public string GetWeiXinToken(int id)
        {

            return this._repository.GetWeiXinToken(id);
        }

        /// <summary>
        /// 得到一个原始id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetwxId(int id)
        {

            return this._repository.GetwxId(id);
        }


        /// <summary>
        /// 取该用户已经有的微信个数
        /// 1e2124dd04e11d01b9df2865f85944be
        /// </summary>
        public int GetUserWxNumCount(int uId)
        {
            return this._repository.GetUserWxNumCount(uId);
        }

        /// <summary>
        /// 判断微账号是否有效
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        public bool wxCodeLegal(int wid)
        {
            return this._repository.wxCodeLegal(wid);
        }

        #endregion  ExtensionMethod
    }
}
