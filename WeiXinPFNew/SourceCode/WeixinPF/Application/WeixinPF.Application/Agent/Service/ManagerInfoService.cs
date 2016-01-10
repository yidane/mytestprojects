using System.Data;
using WeixinPF.Application.Agent.Repository;
using WeixinPF.Common;
using WeixinPF.Model.Agent;

namespace WeixinPF.Application.Agent.Service
{
    public class ManagerInfoService
    {
        private readonly IManagerInfoRepository _repository;
        public ManagerInfoService()
        {
            this._repository = DependencyManager.Resolve<IManagerInfoRepository>();
        }

        #region 基本方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return this._repository.Exists(id);
        }

        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool Exists(string userName)
        {
            return this._repository.Exists(userName);
        }

        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string userName)
        {
            return this._repository.GetSalt(userName);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ManagerInfo model)
        {
            return this._repository.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ManagerInfo model)
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
        /// 删除一条数据(代理商)
        /// </summary>
        public bool DeleteAgent(int managerid)
        {
            return this._repository.DeleteAgent(managerid);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ManagerInfo GetModel(int id)
        {
            return this._repository.GetModel(id);
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public ManagerInfo GetModel(string userName, string password)
        {
            return this._repository.GetModel(userName, password);
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public ManagerInfo GetModel(string userName, string password, bool isEncrypt)
        {
            //检查一下是否需要加密
            if (isEncrypt)
            {
                //先取得该用户的随机密钥
                string salt = this._repository.GetSalt(userName);
                if (string.IsNullOrEmpty(salt))
                {
                    return null;
                }
                //把明文进行加密重新赋值
                password = DESEncrypt.Encrypt(password, salt);
            }
            return this._repository.GetModel(userName, password);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int top, string strWhere, string filedOrder)
        {
            return this._repository.GetList(top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return this._repository.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }


        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool Exists(string userName, string email)
        {
            return this._repository.Exists(userName, email);
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetRecordCount(string strWhere)
        {
            return this._repository.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 取该管理员/代理商 的下线用户数量
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public int GetXiaXianManagerCount(int agentId)
        {
            return this._repository.GetRecordCount("agentId=" + agentId);
        }

        #endregion  Method
    }
}
