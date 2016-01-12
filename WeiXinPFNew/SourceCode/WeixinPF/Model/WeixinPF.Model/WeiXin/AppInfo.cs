using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Model.WeiXin
{
    [Table("WeiXin_AppInfo")]
    public class AppInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 用户表主键id
        /// </summary>
        public int UId { get; set; }

        /// <summary>
        /// 公众帐号名称
        /// </summary>
        [Required, MaxLength(200)]
        public string WxName { get; set; }

        /// <summary>
        /// 公众号原始id
        /// </summary>
        [Required, MaxLength(100)]
        public string WxId { get; set; }

        /// <summary>
        /// 易信原始id
        /// </summary>
        [MaxLength(100)]
        public string YixinId { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        [Required, MaxLength(100)]
        public string WxCode { get; set; }

        /// <summary>
        /// 微信公众平台密码
        /// </summary>
        [MaxLength(200)]
        public string WxPwd { get; set; }

        /// <summary>
        /// 头像地址（url）
        /// </summary>
        [MaxLength(1000)]
        public string Headerpic { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        [MaxLength(1000)]
        public string Apiurl { get; set; }

        /// <summary>
        /// EncodingAESKey值
        /// </summary>
        public string EncodingAesKey { get; set; }

        /// <summary>
        /// TOKEN值
        /// </summary>
        [MaxLength(300)]
        public string WxToken { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [MaxLength(200)]
        public string WxProvince { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [MaxLength(200)]
        public string WxCity { get; set; }

        /// <summary>
        /// 服务号的AppId
        /// </summary>
        [Required, MaxLength(200)]
        public string AppId { get; set; }

        /// <summary>
        /// 服务号的AppSecret
        /// </summary>
        [Required, MaxLength(200)]
        public string AppSecret { get; set; }

        /// <summary>
        /// Access_Token值
        /// </summary>
        [MaxLength(200)]
        public string AccessToken { get; set; }

        /// <summary>
        /// 关注用户openid字符串
        /// </summary>
        [MaxLength(200)]
        public string OpenIdStr { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 文本上限
        /// </summary>
        public int? WenziMaxNum { get; set; }

        /// <summary>
        /// 图文上限
        /// </summary>
        public int? TuwenMaxNum { get; set; }

        /// <summary>
        /// 语音上限
        /// </summary>
        public int? YuyinMaxNum { get; set; }

        /// <summary>
        /// 文本定义条数
        /// </summary>
        public int WenziDefineNum { get; set; }

        /// <summary>
        /// 图文定义条数
        /// </summary>
        public int TuwenDefineNum { get; set; }

        /// <summary>
        /// 语音定义条数
        /// </summary>
        public int YuyinDefineNum { get; set; }

        /// <summary>
        /// 总请求数
        /// </summary>
        public int RequestTtNum { get; set; }

        /// <summary>
        /// 已经使用的请求数
        /// </summary>
        public int RequestUsedNum { get; set; }

        /// <summary>
        /// 短信总条数
        /// </summary>
        public int SmsTtNum { get; set; }

        /// <summary>
        /// 已经使用的短信条数
        /// </summary>
        public int SmsUsedNum { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteDate { get; set; }

        /// <summary>
        /// 微信公众帐号类型（1未认证的订阅号，2认证的订阅号，3未认证的服务号，4认证的服务号，5认证并且开通微支付的服务号）
        /// </summary>
        public int? WxType { get; set; }

        [MaxLength(900)]
        public string Remark { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 扩展字段1
        /// </summary>
        [MaxLength(900)]
        public string ExtStr { get; set; }

        /// <summary>
        /// 扩展字段2
        /// </summary>
        [MaxLength(900)]
        public string ExtStr2 { get; set; }

        /// <summary>
        /// 扩展字段3
        /// </summary>
        [MaxLength(900)]
        public string ExtStr3 { get; set; }

        /// <summary>
        /// 扩展字段1
        /// </summary>
        public int ExtInt { get; set; }

        /// <summary>
        /// 扩展字段1
        /// </summary>
        public int ExtInt2 { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool WStatus { get; set; }

        /// <summary>
        /// 是否关闭自动恢复
        /// </summary>
        public bool CloseKw { get; set; }
    }
}