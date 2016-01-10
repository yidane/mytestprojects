using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Agent.Repository;
using WeixinPF.Model.Agent;

namespace WeixinPF.Application.Agent.Service
{
    public class WXAgentService
    {
        private readonly IWXAgentRepository _repository;
        public WXAgentService(IWXAgentRepository repository)
        {
            this._repository = repository;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AgentInfo model)
        {
            return this._repository.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(AgentInfo model)
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
        public List<AgentInfo> DataTableToList(DataTable dt)
        {
            var modelList = new List<AgentInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                AgentInfo model = null;
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AgentInfo GetAgentModel(int managerid)
        {

            return this._repository.GetAgentModel(managerid);
        }
    }
}
