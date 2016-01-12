using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Model.Agent
{
    /// <summary>
    /// 代理商信息设置
    /// </summary>
    [Table("System_Agent")]
    [Serializable]
    public class AgentInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 代理商id
        /// </summary>
        public int ManagerId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司信息简介
        /// </summary>
        public string CompanyInfo { get; set; }

        /// <summary>
        /// 享受的价格
        /// </summary>
        public int? AgentPrice { get; set; }

        /// <summary>
        /// 享受的价格2
        /// </summary>
        public int? AgentPrice2 { get; set; }

        /// <summary>
        /// 代理商申请的费用
        /// </summary>
        public int? SqJine { get; set; }

        /// <summary>
        /// 充值总金额
        /// </summary>
        public int? CzTotalMoney { get; set; }

        /// <summary>
        /// 剩余金额
        /// </summary>
        public int? RemainMony { get; set; }

        /// <summary>
        /// 用户数量
        /// </summary>
        public int? UserNum { get; set; }

        /// <summary>
        /// 微帐号数量
        /// </summary>
        public int? WcodeNum { get; set; }

        /// <summary>
        /// 代理类型（区域1，行业代理2）
        /// </summary>
        public int? AgentType { get; set; }

        /// <summary>
        /// 代理级别
        /// </summary>
        public string AgentLevel { get; set; }

        /// <summary>
        /// 行业
        /// </summary>
        public string Industry { get; set; }

        /// <summary>
        /// 代理区域
        /// </summary>
        public string AgentArea { get; set; }

        /// <summary>
        /// 代理商截至日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string ARemark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}