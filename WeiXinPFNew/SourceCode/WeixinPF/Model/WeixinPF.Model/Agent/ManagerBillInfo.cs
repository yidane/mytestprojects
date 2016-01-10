using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Model.Agent
{
    /// <summary>
    /// 管理员（代理商）缴费记录
    /// </summary>
    [Serializable]
    [Table("System_ManagerBill")]
    public class ManagerBillInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 管理员id
        /// </summary>
        public int? ManagerId { get; set; }

        /// <summary>
        /// 金额类型（充值，扣减）
        /// </summary>
        public string MoneyType { get; set; }

        /// <summary>
        /// 缴费金额
        /// </summary>
        public int? BillMoney { get; set; }

        /// <summary>
        /// 缴费内容
        /// </summary>
        public string BillUsed { get; set; }

        /// <summary>
        /// 代缴费人员Id
        /// </summary>
        public int? OperPersonId { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OperDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}