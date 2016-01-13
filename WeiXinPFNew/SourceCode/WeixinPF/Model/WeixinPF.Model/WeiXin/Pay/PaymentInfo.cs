using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Model.WeiXin.Pay
{
    public class PaymentInfo
    {
        [Key]
        public Guid PaymentId { get; set; }

        [Required]
        public int AppId { get; set; }

        [Required, MaxLength(50)]
        public string OpenId { get; set; }

        [Required, MaxLength(100)]
        public string ShopName { get; set; }

        [Required, MaxLength(50)]
        public string OrderId { get; set; }

        [Required, MaxLength(50)]
        public string OrderCode { get; set; }

        [Required]
        public decimal PayAmount { get; set; }

        [Required, MaxLength(512)]
        public string Description { get; set; }

        [Required, MaxLength(50)]
        public string ModuleName { get; set; }

        [Required, MaxLength(50)]
        public string WXOrderCode { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        public DateTime ModifyTime { get; set; }

        [Required]
        public int Status { get; set; }
    }
}
