using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Model.Weixin.Message
{
    /// <summary>
    /// 模块的回复，将内容实例化传递
    /// </summary>
    [Table("WeiXin_ResponseContentEntity")]
    public class ResponseContentEntity
    {
        public ResponseContentEntity() { }
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 微帐号主键，也为apiid
        /// </summary>
        public int wid { get; set; }

        /// <summary>
        /// 回复内容的类型：text:纯文本；txtpic:多图文；music:语音；video：视频
        /// </summary>
        public ReponseContentType rcType { get; set; }

        /// <summary>
        /// 回复内容，若为纯文本，则问文本内容；若为多图文、语音，则为标题
        /// </summary>
        public string rContent { get; set; }

        /// <summary>
        /// 回复内容2,多图文、语音：描述；
        /// </summary>
        public string rContent2 { get; set; }

        /// <summary>
        /// 多图文：图片链接
        /// </summary>
        public string picUrl { get; set; }

        /// <summary>
        /// 多图文：详情链接
        /// </summary>
        public string detailUrl { get; set; }

        /// <summary>
        /// 语音：链接
        /// </summary>
        public string musicUrl { get; set; }

        /// <summary>
        /// 语音：高清链接
        /// </summary>
        public string hQMusicUrl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }


    }


    /// <summary>
    /// 枚举：text:纯文本；txtpic:多图文；music:语音；video：视频
    /// </summary>
    public enum ReponseContentType
    {
        /// <summary>
        /// 纯文本
        /// </summary>
        Text,
        /// <summary>
        /// 多图文
        /// </summary>
        TextWithPicture,
        /// <summary>
        /// 语音
        /// </summary>
        Voice,
        /// <summary>
        /// 视频
        /// </summary>
        Video
    }
}
