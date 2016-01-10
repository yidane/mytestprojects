using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Model.WeiXin
{
    public class AppInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        /// <summary>
        /// 用户表主键id
        /// </summary>
        [Column("uId")]
        public int uId { get; set; }

        /// <summary>
        /// 公众帐号名称
        /// </summary>
        [Required, MaxLength(200)]
        [Column("wxName")]
        public string wxName { get; set; }

        /// <summary>
        /// 公众号原始id
        /// </summary>
        [Required, MaxLength(100)]
        [Column("wxId")]
        public string WxId { get; set; }

        /// <summary>
        /// 易信原始id
        /// </summary>
        [Required, MaxLength(100)]
        [Column("yixinId")]
        public string YixinId { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        [Required, MaxLength(100)]
        [Column("weixinCode")]
        public string WxCode { get; set; }

        /// <summary>
        /// 微信公众平台密码
        /// </summary>
        [Required, MaxLength(200)]
        [Column("wxPwd")]
        public string WxPwd { get; set; }

        /// <summary>
        /// 头像地址（url）
        /// </summary>
        [Required, MaxLength(1000)]
        [Column("headerpic")]
        public string headerpic { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        [Required, MaxLength(1000)]
        [Column("apiurl")]
        public string Apiurl { get; set; }

        /// <summary>
        /// EncodingAESKey值
        /// </summary>
        [Column("EncodingAESKey")]
        public string EncodingAESKey { get; set; }

        /// <summary>
        /// TOKEN值
        /// </summary>
        [Required, MaxLength(300)]
        [Column("wxToken")]
        public string WxToken { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [Required, MaxLength(200)]
        [Column("wxProvince")]
        public string WxProvince { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [Required, MaxLength(200)]
        [Column("wxCity")]
        public string WxCity { get; set; }

        /// <summary>
        /// 服务号的AppId
        /// </summary>
        [Required, MaxLength(200)]
        [Column("AppId")]
        public string AppId { get; set; }

        /// <summary>
        /// 服务号的AppSecret
        /// </summary>
        [Required, MaxLength(200)]
        [Column("AppSecret")]
        public string AppSecret { get; set; }

        /// <summary>
        /// Access_Token值
        /// </summary>
        [Required, MaxLength(200)]
        [Column("Access_Token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 关注用户openid字符串
        /// </summary>
        [Required, MaxLength(200)]
        [Column("openIdStr")]
        public string OpenIdStr { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column("createDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [Column("datetime")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 文本上限
        /// </summary>
        [Column("wenziMaxNum")]
        public int? WenziMaxNum { get; set; }

        /// <summary>
        /// 图文上限
        /// </summary>
        [Column("tuwenMaxNum")]
        public int? TuwenMaxNum { get; set; }

        /// <summary>
        /// 语音上限
        /// </summary>
        [Column("yuyinMaxNum")]
        public int? YuyinMaxNum { get; set; }

        /// <summary>
        /// 文本定义条数
        /// </summary>
        [Column("wenziDefineNum")]
        public int WenziDefineNum { get; set; }

        /// <summary>
        /// 图文定义条数
        /// </summary>
        [Column("tuwenDefineNum")]
        public int TuwenDefineNum { get; set; }

        /// <summary>
        /// 语音定义条数
        /// </summary>
        [Column("yuyinDefineNum")]
        public int YuyinDefineNum { get; set; }

        /// <summary>
        /// 总请求数
        /// </summary>
        [Column("requestTTNum")]
        public int RequestTtNum { get; set; }

        /// <summary>
        /// 已经使用的请求数
        /// </summary>
        [Column("requestUsedNum")]
        public int RequestUsedNum { get; set; }

        /// <summary>
        /// 短信总条数
        /// </summary>
        [Column("smsTTNum")]
        public int SmsTtNum { get; set; }

        /// <summary>
        /// 已经使用的短信条数
        /// </summary>
        [Column("smsUsedNum")]
        public int SmsUsedNum { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("isDelete")]
        public bool IsDelete { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("deleteDate")]
        public DateTime DeleteDate { get; set; }

        /// <summary>
        /// 微信公众帐号类型（1未认证的订阅号，2认证的订阅号，3未认证的服务号，4认证的服务号，5认证并且开通微支付的服务号）
        /// </summary>
        [Column("wxType")]
        public int? WxType { get; set; }

        [MaxLength(900)]
        [Column("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("seq")]
        public int Seq { get; set; }

        /// <summary>
        /// 扩展字段1
        /// </summary>
        [MaxLength(900)]
        [Column("extStr")]
        public string extStr { get; set; }

        /// <summary>
        /// 扩展字段2
        /// </summary>
        [MaxLength(900)]
        [Column("extStr2")]
        public string extStr2 { get; set; }

        /// <summary>
        /// 扩展字段3
        /// </summary>
        [MaxLength(900)]
        [Column("extStr3")]
        public string extStr3 { get; set; }

        /// <summary>
        /// 扩展字段1
        /// </summary>
        [Column("extInt")]
        public int extInt { get; set; }

        /// <summary>
        /// 扩展字段1
        /// </summary>
        [Column("extInt2")]
        public int extInt2 { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column("wStatus")]
        public bool wStatus { get; set; }

        /// <summary>
        /// 是否关闭自动恢复
        /// </summary>
        [Column("closeKW")]
        public bool closeKW { get; set; }
    }
}
