using System;

namespace WeixinPF.Model.Agent
{
    /// <summary>
    ///     管理员信息表:实体类
    /// </summary>
    [Serializable]
    public class ManagerInfo
    {
        /// <summary>
        ///     自增ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        ///     角色ID
        /// </summary>
        public int role_id { get; set; }

        /// <summary>
        ///     管理员类型1超管2系管
        /// </summary>
        public int role_type { get; set; }

        /// <summary>
        ///     角色名
        /// </summary>
        public string role_name { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        ///     登录密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        ///     6位随机字符串,加密用到
        /// </summary>
        public string salt { get; set; }

        /// <summary>
        ///     用户昵称
        /// </summary>
        public string real_name { get; set; }

        /// <summary>
        ///     联系电话
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        ///     电子邮箱
        /// </summary>
        public string email { get; set; }

        /// <summary>
        ///     是否锁定
        /// </summary>
        public int is_lock { get; set; }

        /// <summary>
        ///     添加时间
        /// </summary>
        public DateTime add_time { get; set; }

        /// <summary>
        ///     最大微信数量
        /// </summary>
        public int wxNum { get; set; }

        /// <summary>
        ///     代理商Id
        /// </summary>
        public int agentId { get; set; }

        /// <summary>
        ///     注册的ip地址
        /// </summary>
        public string reg_ip { get; set; }

        /// <summary>
        ///     常用qq
        /// </summary>
        public string qq { get; set; }

        /// <summary>
        ///     省份
        /// </summary>
        public string province { get; set; }

        /// <summary>
        ///     城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        ///     县城（区）
        /// </summary>
        public string county { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        ///     排序号
        /// </summary>
        public int? sort_id { get; set; }

        /// <summary>
        ///     代理商级别，如果非代理商则为-1
        /// </summary>
        public int agentLevel { get; set; }
    }
}