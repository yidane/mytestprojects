using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Model.WeiXin.Pay
{
    [Table("WeiXin_Pay_PaymentInfo")]
    public class PaymentInfo
    {
        [Key]
        public Guid PaymentId { get; set; }

        [Required]
        public int AppId { get; set; }

        [Required, MaxLength(50)]
        public string OpenId { get; set; }

        [Required, MaxLength(50)]
        public string OrderId { get; set; }

        [Required, MaxLength(50)]
        public string OrderCode { get; set; }

        [Required]
        public decimal PayAmount { get; set; }

        [Required, MaxLength(512)]
        public string Description { get; set; }

        [Required, MaxLength(512)]
        public string Body { get; set; }

        [Required, MaxLength(50)]
        public string ModuleName { get; set; }

        [Required, MaxLength(50)]
        public string WxOrderCode { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        public DateTime? ModifyTime { get; set; }

        [Required]
        public int Status { get; set; }
    }
}