using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Model.WeiXin.User
{
    [Table("WeiXin_User")]
    public class UserInfo
    {
        [Key]
        [Column(Order = 1)]
        public int AppId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string Openid { get; set; }
        public string Nickname { get; set; }
        public int Sex { get; set; }
        public string Language { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Headimgurl { get; set; }
        public Int64 SubscribeTime { get; set; }
        public string Remark { get; set; }
        public int Groupid { get; set; }
        public string Subscribe { get; set; }
    }
}
