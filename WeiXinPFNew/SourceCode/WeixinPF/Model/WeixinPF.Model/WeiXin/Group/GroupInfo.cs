using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Model.WeiXin.Group
{
    [Table("WeiXin_GroupModel")]
    public class GroupInfo
    {
        /// <summary>
        /// 所对应公众号ID
        /// </summary>
        [Key]
        [Column(Order = 1)]
        public int AppId { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        [Key]
        [Column(Order = 2)]
        public int Id { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 组内用户总数
        /// </summary>
        public int Count { get; set; }
    }
}
