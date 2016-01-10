using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using WeiXinPF.Plugins.WeiXinPay.Service.Application;

namespace WeiXinPF.Plugins.WeiXinPay.Service.Models
{
    [Table("WeiXin_Payment_PayNotifyInfo")]
    public class WeiXinPayNotifyInfo
    {
        [Key]
        public Guid NotifyId { get; set; }
        [Required, MaxLength(20)]
        public string ModuleName { get; set; }
        [MaxLength(50)]
        public string OutTradeNo { get; set; }
        [MaxLength(50)]
        public string Appid { get; set; }
        [MaxLength(500)]
        public string Attach { get; set; }
        [Required]
        public string BankType { get; set; }
        public int CashFee { get; set; }
        [MaxLength(50)]
        public string CashFeeType { get; set; }
        public int CouponCount { get; set; }
        public int CouponFee { get; set; }
        [MaxLength(50)]
        public string DeviceInfo { get; set; }
        [MaxLength(50)]
        public string ErrCode { get; set; }
        [MaxLength(50)]
        public string ErrCodeDes { get; set; }
        [MaxLength(50)]
        public string FeeType { get; set; }
        [MaxLength(50)]
        public string IsSubscribe { get; set; }
        [MaxLength(50)]
        public string MchId { get; set; }
        [MaxLength(50)]
        public string NonceStr { get; set; }
        [MaxLength(50)]
        public string Openid { get; set; }
        [MaxLength(50)]
        public string ResultCode { get; set; }
        [MaxLength(50)]
        public string ReturnCode { get; set; }
        [MaxLength(50)]
        public string ReturnMsg { get; set; }
        [MaxLength(50)]
        public string Sign { get; set; }
        [MaxLength(50)]
        public string TimeEnd { get; set; }
        [Required]
        public int TotalFee { get; set; }
        [MaxLength(50)]
        public string TradeType { get; set; }
        [MaxLength(50)]
        public string TransactionId { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }

        public void Add()
        {
            using (var db = new WeiXinPayDbContext())
            {
                db.PayNotifyInfoContext.Add(this);
                db.SaveChanges();
            }
        }

        public void Modify()
        {
            using (var db = new WeiXinPayDbContext())
            {
                db.PayNotifyInfoContext.Attach(this);
                db.Entry(this).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public IList<WeiXinPayNotifyInfo> Get(Func<WeiXinPayNotifyInfo, bool> condition)
        {
            using (var db = new WeiXinPayDbContext())
            {
                return db.PayNotifyInfoContext.Where(condition).ToList();
            }
        }

        /// <summary>
        /// 防止存在相同参数
        /// </summary>
        /// <param name="moudleName"></param>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
        public bool ContainPayNotufy(string moudleName, string outTradeNo)
        {
            using (var db = new WeiXinPayDbContext())
            {
                return
                    db.PayNotifyInfoContext.Any(
                        item =>
                        string.Equals(item.ModuleName, moudleName) &&
                        string.Equals(OutTradeNo, item.OutTradeNo));
            }
        }
    }
}
