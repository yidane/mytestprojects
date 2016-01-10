using System;
using System.Collections.Generic;
using System.Data;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Common;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Application.Weixin.Service
{
    public class WXPropertyService
    {
        private readonly IPropertyRepository _repository;

        public WXPropertyService()
        {
            _repository = DependencyManager.Resolve<IPropertyRepository>();
        }

        public WXPropertyService(IPropertyRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PropertyInfo model)
        {
            return _repository.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(PropertyInfo model)
        {
            return _repository.Update(model);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return _repository.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PropertyInfo> GetModelList(string strWhere)
        {
            var ds = _repository.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PropertyInfo> DataTableToList(DataTable dt)
        {
            var modelList = new List<PropertyInfo>();
            var rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                PropertyInfo model = null;
                for (var n = 0; n < rowsCount; n++)
                {
                    model = _repository.DataRowToModel(dt.Rows[n]);
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
            var ret = "";
            try
            {
                if (expires_in == 0)
                {
                    expires_in = 1200;
                }
                var wxProperty = new PropertyInfo
                {
                    iName = "access_token",
                    TypeId = 1,
                    typeName = "base",
                    iContent = access_token,
                    expires_in = expires_in,
                    createDate = DateTime.Now,
                    count = 1,
                    Wid = wid
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
            var ret = "";
            try
            {
                var wxProperty = new PropertyInfo();
                if (!ExistsWid(wid, key))
                {
                    wxProperty.iName = key;
                    wxProperty.TypeId = 1;
                    wxProperty.typeName = "base";
                    wxProperty.iContent = value;
                    wxProperty.expires_in = 0;
                    wxProperty.createDate = DateTime.Now;
                    wxProperty.count = 1;
                    wxProperty.Wid = wid;
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
            return _repository.ExistsWid(wid);
        }

        /// <summary>
        /// 该微帐号是否存在记录
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="iName">键值</param>
        /// <returns></returns>
        public bool ExistsWid(int wid, string iName)
        {
            return _repository.ExistsWid(wid, iName);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PropertyInfo GetModelByIName(int wid, string iName)
        {
            return _repository.GetModelByIName(wid, iName);
        }
    }
}