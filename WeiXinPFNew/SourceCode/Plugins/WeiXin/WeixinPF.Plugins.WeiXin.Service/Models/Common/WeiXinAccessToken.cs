﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Plugins.WeiXin.Service.Models.Common
{
    [Table("WeiXin_AppModel")]
    public class WeiXinAccessToken
    {
        [Key]
        public int AppId { get; set; }

        [Key]
        public Guid Id { get; set; }

        public string Ticket { get; set; }

        public int Expires { get; set; }

        public DateTime SysDateTime { get; set; }

        public string TypeDescript { get { return "client_credential"; } }

        public bool Expired { get; set; }

        public DateTime ExpireDateTime { get; set; }

        /// <summary>
        /// 判断是否过期
        /// <returns></returns>
        /// </summary>
        public bool IsExpired()
        {
            //为了系统稳定性，只会取过期时间的95%的过期时间值
            var realExpires = (int)(Expires * 0.95);
            return SysDateTime.AddSeconds(realExpires) >= DateTime.Now;
        }
    }
}