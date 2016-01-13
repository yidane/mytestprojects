using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneGulp.WeChat.MP.TenPayLibV3.TenPayV3.Model;
using WeixinPF.Application.Weixin.Pay.Repository;
using WeixinPF.Hotel.Plugins.Service.Application.Service;

namespace WeixinPF.Hotel.Plugins.Service.PayNotify
{

    /// <summary>
    /// 订单支付完成
    /// </summary>
    public class OrderPayNotifyRepository: IPayNotifyRepository
    {
        public bool PayNotify(PaymentNotify paymentNotify,out string message)
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

    
}
