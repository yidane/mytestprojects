using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Model.WeiXin
{
    /// <summary>
    /// 微信属性值存储值 支持多用户平台
    /// </summary>
    [Serializable]
    public partial class PropertyInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 微帐号id
        /// </summary>
        public int Wid { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        public int? TypeId { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string typeName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string iName { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string iContent { get; set; }
        /// <summary>
        /// 有效期（秒）
        /// </summary>
        public int? expires_in { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? createDate { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int? count { get; set; }
        /// <summary>
        /// 类分类d
        /// </summary>
        public int? categoryId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string categoryName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
