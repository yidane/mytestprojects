using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Plugins.WeiXin.Service.Models.User
{
    [Table("WeiXin_UserModel")]
    public class WeiXinUserInfo
    {
        [Key]
        public int AppId { get; set; }
        [Key]
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public Int64 subscribe_time { get; set; }
        public string remark { get; set; }
        public int groupid { get; set; }
        public string subscribe { get; set; }
    }
}
