using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Model.Weixin;

namespace WeixinPF.Application.Weixin.Service
{
    public class WXPropertyService
    {
        private readonly IWXPropertyRepository _repository;

        public WXPropertyService(IWXPropertyRepository repository)
        {
            this._repository = repository;
        }
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(WX_PropertyInfo model)
        {
            return this._repository.Add(model);
        }

        /// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(WX_PropertyInfo model)
        {
            return this._repository.Update(model);
        }

        /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
        {
            return this._repository.GetList(strWhere);
        }

        /// <summary>
		/// 获得数据列表
		/// </summary>
		public List<WX_PropertyInfo> GetModelList(string strWhere)
        {
            DataSet ds = this._repository.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
		/// 获得数据列表
		/// </summary>
		public List<WX_PropertyInfo> DataTableToList(DataTable dt)
        {
            var modelList = new List<WX_PropertyInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                WX_PropertyInfo model = null;
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
        /// 添加access_token值
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="access_token"></param>
        /// <param name="expires_in">过期（秒）</param>
        /// <returns></returns>
        public string AddAccess_Token(int wid, string access_token, int expires_in = 1200)
        {
            string ret = "";
            try
            {
                if (expires_in == 0)
                {
                    expires_in = 1200;
                }
                var wxProperty = new WX_PropertyInfo
                {
                    iName = "access_token",
                    typeId = 1,
                    typeName = "base",
                    iContent = access_token,
                    expires_in = expires_in,
                    createDate = DateTime.Now,
                    count = 1,
                    wid = wid
                };
                Add(wxProperty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return "";
        }

        /// <summary>
        /// 添加属性值
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="key">对应这里的值 MXEnums.WXPropertyKeyName</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string AddProperty(int wid, string key, string value)
        {
            string ret = "";
            try
            {
                var wxProperty = new WX_PropertyInfo();
                if (!ExistsWid(wid, key))
                {
                    wxProperty.iName = key;
                    wxProperty.typeId = 1;
                    wxProperty.typeName = "base";
                    wxProperty.iContent = value;
                    wxProperty.expires_in = 0;
                    wxProperty.createDate = DateTime.Now;
                    wxProperty.count = 1;
                    wxProperty.wid = wid;
                    Add(wxProperty);
                }
                else
                {
                    wxProperty = GetModelList("iName='" + key + "' and wid=" + wid)[0];
                    wxProperty.iContent = value;
                    Update(wxProperty);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return "";
        }

        /// <summary>
        /// 该微帐号是否存在记录
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        public bool ExistsWid(int wid)
        {
            return this._repository.ExistsWid(wid);
        }

        /// <summary>
        /// 该微帐号是否存在记录
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="iName">键值</param>
        /// <returns></returns>
        public bool ExistsWid(int wid, string iName)
        {

            return this._repository.ExistsWid(wid, iName);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WX_PropertyInfo GetModelByIName(int wid, string iName)
        {
            return this._repository.GetModelByIName(wid, iName);
        }
    }
}
