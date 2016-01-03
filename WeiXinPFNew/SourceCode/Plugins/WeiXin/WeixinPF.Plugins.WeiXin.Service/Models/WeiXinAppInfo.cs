using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Plugins.WeiXin.Service.Models
{
    [Table("WeiXin_AppModel")]
    public class WeiXinAppInfo
    {
        [Key]
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
        public string wxId { get; set; }

        /// <summary>
        /// 易信原始id
        /// </summary>
        [Required, MaxLength(100)]
        [Column("yixinId")]
        public string yixinId { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        [Required, MaxLength(100)]
        [Column("weixinCode")]
        public string weixinCode { get; set; }

        /// <summary>
        /// 微信公众平台密码
        /// </summary>
        [Required, MaxLength(200)]
        [Column("wxPwd")]
        public string wxPwd { get; set; }

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
        public string apiurl { get; set; }

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
        public string wxToken { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [Required, MaxLength(200)]
        [Column("wxProvince")]
        public string wxProvince { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [Required, MaxLength(200)]
        [Column("wxCity")]
        public string wxCity { get; set; }

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
        public string Access_Token { get; set; }

        /// <summary>
        /// 关注用户openid字符串
        /// </summary>
        [Required, MaxLength(200)]
        [Column("openIdStr")]
        public string openIdStr { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column("createDate")]
        public DateTime createDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column("datetime")]
        public DateTime endDate { get; set; }

        /// <summary>
        /// 文本上限
        /// </summary>
        [Column("wenziMaxNum")]
        public int? wenziMaxNum { get; set; }

        /// <summary>
        /// 图文上限
        /// </summary>
        [Column("tuwenMaxNum")]
        public int? tuwenMaxNum { get; set; }

        /// <summary>
        /// 语音上限
        /// </summary>
        [Column("yuyinMaxNum")]
        public int? yuyinMaxNum { get; set; }

        /// <summary>
        /// 文本定义条数
        /// </summary>
        [Column("wenziDefineNum")]
        public int wenziDefineNum { get; set; }

        /// <summary>
        /// 图文定义条数
        /// </summary>
        [Column("tuwenDefineNum")]
        public int tuwenDefineNum { get; set; }

        /// <summary>
        /// 语音定义条数
        /// </summary>
        [Column("yuyinDefineNum")]
        public int yuyinDefineNum { get; set; }

        /// <summary>
        /// 总请求数
        /// </summary>
        [Column("requestTTNum")]
        public int requestTTNum { get; set; }

        /// <summary>
        /// 已经使用的请求数
        /// </summary>
        [Column("requestUsedNum")]
        public int requestUsedNum { get; set; }

        /// <summary>
        /// 短信总条数
        /// </summary>
        [Column("smsTTNum")]
        public int smsTTNum { get; set; }

        /// <summary>
        /// 已经使用的短信条数
        /// </summary>
        [Column("smsUsedNum")]
        public int smsUsedNum { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("isDelete")]
        public bool isDelete { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("deleteDate")]
        public DateTime deleteDate { get; set; }

        /// <summary>
        /// 微信公众帐号类型（1未认证的订阅号，2认证的订阅号，3未认证的服务号，4认证的服务号，5认证并且开通微支付的服务号）
        /// </summary>
        [Column("wxType")]
        public int wxType { get; set; }

        [MaxLength(900)]
        [Column("remark")]
        public string remark { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("seq")]
        public int seq { get; set; }

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
