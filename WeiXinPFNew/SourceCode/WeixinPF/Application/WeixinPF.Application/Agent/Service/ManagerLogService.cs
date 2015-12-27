using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Agent.Repository;
using WeixinPF.Common;
using WeixinPF.Model.Agent;

namespace WeixinPF.Application.Agent.Service
{
    public class ManagerLogService
    {
        private readonly IManagerLogRepository _repository;
        public ManagerLogService(IManagerLogRepository repository)
        {
            this._repository = repository;
            //dal = new DAL.manager(siteConfig.sysdatabaseprefix);
        }
        #region 基本方法==============================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return this._repository.Exists(id);
        }

        /// <summary>
        /// 增加管理日志
        /// </summary>
        /// <param name="用户id"></param>
        /// <param name="用户名"></param>
        /// <param name="操作类型"></param>
        /// <param name="备注"></param>
        /// <returns></returns>
        public int Add(int user_id, string user_name, string action_type, string remark)
        {
            var manager_log_model = new Manager_LogInfo();
            manager_log_model.user_id = user_id;
            manager_log_model.user_name = user_name;
            manager_log_model.action_type = action_type;
            manager_log_model.remark = remark;
            manager_log_model.user_ip = MXRequest.GetIP();

            return this._repository.Add(manager_log_model);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Manager_LogInfo model)
        {
            return this._repository.Add(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Manager_LogInfo GetModel(int id)
        {
            return this._repository.GetModel(id);
        }

        /// <summary>
        /// 根据用户名返回上一次登录记录
        /// </summary>
        public Manager_LogInfo GetModel(string user_name, int top_num, string action_type)
        {
            return this._repository.GetModel(user_name, top_num, action_type);
        }

        /// <summary>
        /// 删除7天前的日志数据
        /// </summary>
        public int Delete(int dayCount)
        {
            return this._repository.Delete(dayCount);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this._repository.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return this._repository.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion
    }
}
