using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Hotel.Plugins.Service.Models
{
    [Table("wx_Hotel_User")]
    public class HotelUserInfo
    {
        [Key]
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public int HotelId { get; set; }
        public int AdminId { get; set; }
    }
}
