using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace WeixinPF.Model.Agent
{
    /// <summary>
    /// 管理日志:实体类
    /// </summary>
    [Serializable]
    [Table("System_ManagerLog")]
    public class ManagerLogInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// 当前会话Id
        /// </summary>
        [Required, MaxLength(50)]
        public string SessionId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required, MaxLength(100)]
        public string UserName { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [Required, MaxLength(100)]
        public string ActionType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(900)]
        public string Remark { get; set; }

        /// <summary>
        /// 用户IP
        /// </summary>
        [MaxLength(20)]
        public string UserIp { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}