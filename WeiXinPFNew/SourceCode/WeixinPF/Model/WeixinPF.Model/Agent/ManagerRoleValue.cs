using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Model.Agent
{
    /// <summary>
    /// 管理角色权限:实体类
    /// </summary>
    [Serializable]
    [Table("System_ManagerRoleValue")]
    public class ManagerRoleValueInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 导航名称
        /// </summary>
        public string NavName { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public string ActionType { get; set; }
    }
}