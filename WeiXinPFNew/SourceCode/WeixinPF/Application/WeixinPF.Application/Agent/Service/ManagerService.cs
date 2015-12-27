using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.Agent.Repository;
using WeixinPF.Model.Agent;
using WeixinPF.Common;

namespace WeixinPF.Application.Agent
{
    public class ManagerService
    {
        //private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        //private readonly DAL.manager dal;
        private readonly IManagerRepository _repository;
        public ManagerService(IManagerRepository repository)
        {
            this._repository = repository;
            //dal = new DAL.manager(siteConfig.sysdatabaseprefix);
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
        public bool Exists(string user_name)
        {
            return this._repository.Exists(user_name);
        }

        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string user_name)
        {
            return this._repository.GetSalt(user_name);
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
        public ManagerInfo GetModel(string user_name, string password)
        {
            return this._repository.GetModel(user_name, password);
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public ManagerInfo GetModel(string user_name, string password, bool is_encrypt)
        {
            //检查一下是否需要加密
            if (is_encrypt)
            {
                //先取得该用户的随机密钥
                string salt = this._repository.GetSalt(user_name);
                if (string.IsNullOrEmpty(salt))
                {
                    return null;
                }
                //把明文进行加密重新赋值
                password = DESEncrypt.Encrypt(password, salt);
            }
            return this._repository.GetModel(user_name, password);
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


        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool Exists(string user_name, string email)
        {
            return this._repository.Exists(user_name, email);
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
        public int GetXiaXianManagerCount(int managerId)
        {
            return this._repository.GetRecordCount("agentId=" + managerId);
        }


        #endregion  Method
    }
}
