using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Model.Agent
{
    /// <summary>
    /// 管理角色:实体类
    /// </summary>
    [Serializable]
    [Table("System_ManagerRole")]
    public class ManagerRoleInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色类型
        /// </summary>
        public int RoleType { get; set; }

        /// <summary>
        /// 是否系统默认0否1是
        /// </summary>
        public int IsSys { get; set; }

        /// <summary>
        /// 所属的代理商Id
        /// </summary>
        public int AgentId { get; set; }

        /// <summary>
        /// 权限子类
        /// </summary>
        public virtual ICollection<ManagerRoleValueInfo> ManagerRoleValues { get; set; }
    }
}