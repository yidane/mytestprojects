using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Model.Agent
{
    /// <summary>
    /// 管理角色权限:实体类
    /// </summary>
    [Serializable]
    public partial class Manager_Role_ValueInfo
    {
        public Manager_Role_ValueInfo()
        { }
        #region Model
        private int _id;
        private int _role_id;
        private string _nav_name;
        private string _action_type;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int role_id
        {
            set { _role_id = value; }
            get { return _role_id; }
        }
        /// <summary>
        /// 导航名称
        /// </summary>
        public string nav_name
        {
            set { _nav_name = value; }
            get { return _nav_name; }
        }
        /// <summary>
        /// 权限类型
        /// </summary>
        public string action_type
        {
            set { _action_type = value; }
            get { return _action_type; }
        }
        #endregion Model

    }
}
