﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Agent.Repository;
using WeixinPF.Model.Agent;

namespace WeixinPF.Application.Agent.Service
{
    public class WXManagerBillService
    {
        private readonly IManagerBillRepository _repository;
        public WXManagerBillService(IManagerBillRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ManagerBillInfo model)
        {
            return this._repository.Add(model);
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
		public List<ManagerBillInfo> DataTableToList(DataTable dt)
        {
            var modelList = new List<ManagerBillInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ManagerBillInfo model = null;
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
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return this._repository.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
    }
}
