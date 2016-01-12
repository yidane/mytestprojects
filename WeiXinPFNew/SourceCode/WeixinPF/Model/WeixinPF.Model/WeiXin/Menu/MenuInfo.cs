using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Model.WeiXin.Menu
{
    [Table("WeiXin_Menu")]
    public class MenuInfo
    {
        [Key]
        [Column(Order = 1)]
        public int AppId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 2)]
        public int Id { get; set; }
        public int ParentId { get; set; }

        [MaxLength(100)]
        public string MenuName { get; set; }

        [MaxLength(100)]
        public string MenuType { get; set; }

        [MaxLength(900)]
        public string MenuUrl { get; set; }
    }
}
