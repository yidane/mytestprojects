using System;
namespace WeixinPF.Model.WeiXin.Message
{
    public class RequestRuleContent
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
        /// 规则主键Id
        /// </summary>
        public int? RId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string RContent { get; set; }
        /// <summary>
        /// 内容2
        /// </summary>
        public string RContent2 { get; set; }
        /// <summary>
        /// 详情链接地址
        /// </summary>
        public string DetailUrl { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 语音或视频地址
        /// </summary>
        public string MediaUrl { get; set; }
        /// <summary>
        /// 高清语音或者视频地址
        /// </summary>
        public string MeidaHdUrl { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int? Seq { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }
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
        public string Extstr3 { get; set; }
    }
}
