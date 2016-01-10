using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Model.Agent
{
    /// <summary>
    /// 管理员信息表:实体类
    /// </summary>
    [Serializable]
    [Table("System_Manager")]
    public class ManagerInfo
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
        /// 管理员类型1超管2系管
        /// </summary>
        public int RoleType { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 6位随机字符串,加密用到
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public int IsLock { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 最大微信数量
        /// </summary>
        public int WxNum { get; set; }

        /// <summary>
        /// 代理商Id
        /// </summary>
        public int AgentId { get; set; }

        /// <summary>
        /// 注册的ip地址
        /// </summary>
        public string RegIp { get; set; }

        /// <summary>
        /// 常用qq
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 县城（区）
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int? SortId { get; set; }

        /// <summary>
        /// 代理商级别，如果非代理商则为-1
        /// </summary>
        public int AgentLevel { get; set; }
    }
}