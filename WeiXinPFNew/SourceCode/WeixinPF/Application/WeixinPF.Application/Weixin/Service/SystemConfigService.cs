﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Model.WeiXin;

namespace WeixinPF.Application.Weixin.Service
{
    public class SystemConfigService
    {
        private readonly ISystemConfigRepository _repository;

        public SystemConfigService(ISystemConfigRepository repository)
        {
            this._repository = repository;
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
		public List<SystemConfigInfo> DataTableToList(DataTable dt)
        {
            var modelList = new List<SystemConfigInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SystemConfigInfo model = null;
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
        /// 通过配置节点编码找对应的值
        /// </summary>
        /// <param name="sysCode"></param>
        /// <returns></returns>
        public string GetConfigValue(string sysCode)
        {
            return this._repository.GetConfigValue(sysCode);
        }

        /// <summary>
        /// 修改值
        /// </summary>
        /// <param name="sysCode"></param>
        /// <param name="sysValue"></param>
        /// <returns></returns>
        public bool EditSysValue(string sysCode, string sysValue)
        {
            return this._repository.EditSysValue(sysCode, sysValue);

        }
    }
}
