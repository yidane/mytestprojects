using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Model.WeiXin
{
    /// <summary>
    /// 微支付接口表
    /// </summary>
    [Serializable]
    public class PaymentInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 微帐号id
        /// </summary>
        [Required]
        public int AppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [Required, MaxLength(30)]
        public string MchId { get; set; }

        /// <summary>
        /// 商户支付密钥key
        /// </summary>
        [Required, MaxLength(900)]
        public string Paykey { get; set; }

        /// <summary>
        /// 证书地址
        /// </summary>
        [Required, MaxLength(900)]
        public string CertInfoPath { get; set; }

        /// <summary>
        /// 证书密码
        /// </summary>
        [Required, MaxLength(900)]
        public string CerInfoPwd { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required, MaxLength(900)]
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required, MaxLength(900)]
        public DateTime CreateDate { get; set; }
    }
}