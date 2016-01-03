using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Plugins.WeiXin.Service.Models.Group
{
    [Table("WeiXin_GroupModel")]
    public class WeiXinGroupInfo
    {
        /// <summary>
        /// 所对应公众号ID
        /// </summary>
        [Key]
        public int AppId { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [Required, MaxLength(100)]
        public string name { get; set; }

        /// <summary>
        /// 组内用户总数
        /// </summary>
        public int count { get; set; }
    }
}
