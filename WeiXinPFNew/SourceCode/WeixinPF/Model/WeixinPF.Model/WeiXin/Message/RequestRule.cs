using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Model.WeiXin.Message
{
    [Table("WeiXin_RequestRule")]
    public class RequestRule
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户表主键Id
        /// </summary>
        public int? UId { get; set; }

        /// <summary>
        /// 微信公众帐号信息表主键Id
        /// </summary>
        public int? WId { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string RuleName { get; set; }
        /// <summary>
        /// 请求的关键词（多个中间使用英文逗号隔开）
        /// </summary>
        public string ReqKeywords { get; set; }
        /// <summary>
        /// 请求类型 （文字1，图片2，语音3，链接4，地理位置5，6关注，7取消关注，8扫描带参数二维码事件，上报地理位置事件9，自定义菜单事件10）
        /// </summary>
        public int? ReqestType { get; set; }
        /// <summary>
        /// 回复类型（文本1，图文2，语音3，视频4,第三方接口5）
        /// </summary>
        public int? ResponseType { get; set; }
        /// <summary>
        /// 是默认回复
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 功能模版名称
        /// </summary>
        public string ModelFunctionName { get; set; }
        /// <summary>
        /// 功能模块Id
        /// </summary>
        public int? ModelFunctionId { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int? Seq { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 第三方接口的url
        /// </summary>
        public string AgentUrl { get; set; }
        /// <summary>
        /// 第三方token值
        /// </summary>
        public string AgentToken { get; set; }
        /// <summary>
        /// 是模糊查询
        /// </summary>
        public bool IsLikeSearch { get; set; }
        /// <summary>
        /// 扩展int
        /// </summary>
        public int? ExtInt { get; set; }
        /// <summary>
        /// 扩展int
        /// </summary>
        public int? ExtInt2 { get; set; }
        /// <summary>
        /// 扩展str
        /// </summary>
        public string ExtStr { get; set; }
        /// <summary>
        /// 扩展str2
        /// </summary>
        public string ExtStr2 { get; set; }
        /// <summary>
        /// 扩展str3
        /// </summary>
        public string ExtStr3 { get; set; }
        /// <summary>
        /// 扩展str4
        /// </summary>
        public string ExtStr4 { get; set; }
    }
}
