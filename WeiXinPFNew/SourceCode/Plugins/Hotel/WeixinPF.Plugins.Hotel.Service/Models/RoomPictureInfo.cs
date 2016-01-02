using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Plugins.Hotel.Service.Models
{
    [Table("wx_hotel_roompic")]
    public class RoomPictureInfo
    {

        /// <summary>
        /// 编号
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 房间号
        /// </summary>
        public int? roomid { set; get; }

        /// <summary>
        /// 商家号
        /// </summary>
        public int? hotelid { set; get; }

        /// <summary>
        /// 文字描述
        /// </summary>
        public string title { set; get; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int? sortpicid { set; get; }

        /// <summary>
        /// 房间图片
        /// </summary>
        public string roomPic { set; get; }

        /// <summary>
        /// 图片跳转地址
        /// </summary>
        public string roomPictz { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? createDate { set; get; }

    }
}
