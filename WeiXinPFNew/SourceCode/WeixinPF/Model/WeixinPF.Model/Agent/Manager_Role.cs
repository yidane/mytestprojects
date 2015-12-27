﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Model.Agent
{
    /// <summary>
    /// 管理角色:实体类
    /// </summary>
    [Serializable]
    public partial class Manager_RoleInfo
    {
        public Manager_RoleInfo()
        { }
        #region Model
        private int _id;
        private string _role_name;
        private int _role_type = 1;
        private int _is_sys = 0;

        private int _agentid = -1;

        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string role_name
        {
            set { _role_name = value; }
            get { return _role_name; }
        }
        /// <summary>
        /// 角色类型
        /// </summary>
        public int role_type
        {
            set { _role_type = value; }
            get { return _role_type; }
        }
        /// <summary>
        /// 是否系统默认0否1是
        /// </summary>
        public int is_sys
        {
            set { _is_sys = value; }
            get { return _is_sys; }
        }

        /// <summary>
        /// 所属的代理商Id
        /// </summary>
        public int agentId
        {
            set { _agentid = value; }
            get { return _agentid; }
        }

        #endregion Model

        private List<Manager_Role_ValueInfo> _manager_role_values;
        /// <summary>
        /// 权限子类 
        /// </summary>
        public List<Manager_Role_ValueInfo> manager_role_values
        {
            set { _manager_role_values = value; }
            get { return _manager_role_values; }
        }
    }
}
