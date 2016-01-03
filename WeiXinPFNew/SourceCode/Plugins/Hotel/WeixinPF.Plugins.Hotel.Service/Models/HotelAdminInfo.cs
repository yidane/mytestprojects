using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeixinPF.Hotel.Plugins.Service.Models
{
    [Table("wx_hotel_admin")]
    public class HotelAdminInfo
    {        
        [Key]
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public int HotelId { get; set; }
    }
}
