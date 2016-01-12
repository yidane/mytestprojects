using System;
using System.Data;
using WeixinPF.Application.Agent.Repository;
using WeixinPF.Common;
using WeixinPF.Model.Agent;

namespace WeixinPF.Application.Agent.Service
{
    public class ManagerLogService
    {
        private readonly IManagerLogRepository _repository;

        public ManagerLogService()
        {
            _repository = DependencyManager.Resolve<IManagerLogRepository>();
        }

        #region 基本方法==============================

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return _repository.Exists(id);
        }

        /// <summary>
        /// 增加管理日志
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sessionId"></param>
        /// <param name="userName"></param>
        /// <param name="actionType"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public int Add(int userId, string sessionId, string userName, string actionType, string remark)
        {
            var managerLogInfo = new ManagerLogInfo
            {
                UserId = userId,
                SessionId = sessionId,
                UserName = userName,
                ActionType = actionType,
                Remark = remark,
                AddTime = DateTime.Now,
#if DEBUG
                UserIp = "127.0.0.1"
#else
                UserIp = MXRequest.GetIP()
#endif
            };

            return _repository.Add(managerLogInfo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ManagerLogInfo model)
        {
            return _repository.Add(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ManagerLogInfo GetModel(int id)
        {
            return _repository.GetModel(id);
        }

        /// <summary>
        /// 根据用户名返回上一次登录记录
        /// </summary>
        public ManagerLogInfo GetModel(string userName, int topNum, string actionType)
        {
            return _repository.GetModel(userName, topNum, actionType);
        }

        /// <summary>
        /// 删除7天前的日志数据
        /// </summary>
        public int Delete(int dayCount)
        {
            return _repository.Delete(dayCount);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return _repository.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return _repository.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion
    }
}