using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeixinPF.Plugins.Hotel.Service.Models
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
