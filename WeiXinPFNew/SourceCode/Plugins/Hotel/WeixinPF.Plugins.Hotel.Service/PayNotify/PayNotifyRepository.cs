using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Hotel.Plugins.Service.Application.Service;

namespace WeixinPF.Hotel.Plugins.Service.PayNotify
{
    public class PayNotifyRepository
    {
        public bool PayNotify(PaymentNotify paymentNotify, out string message)
        {

            message = string.Empty; 
            //支付完成后修改订单状态为已支付
            try
            {
                var hotelOrderService = new HotelOrderService();
                hotelOrderService.PaySuccess(paymentNotify.out_trade_no); 
                return true;
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return false;
            } 
        }
    }

    public class PaymentNotify
    {
        public string out_trade_no { get; set; }
    }
}
