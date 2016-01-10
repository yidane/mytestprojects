using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Model.WeiXin.Message
{
    public class ResponseMessageLog
    {
        //[ForeignKey()]
        public int AppId { get; set; }
        public string Openid { get; set; }
        [Key]
        public Guid Id { get; set; }
        public string RequestType { get; set; }
        public string RequestContent { get; set; }
        public string ResponseType { get; set; }
        public string ResponseContent { get; set; }
        public string ToUserName { get; set; }
    }
}
