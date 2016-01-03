using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneGulp.WeChat.MP.TenPayLibV3.TenPayV3.Model;
using WeiXinPF.Plugins.WeiXinPay.Service.Application.Repository;
using WeiXinPF.Plugins.WeiXinPay.Service.Models;

namespace WeiXinPF.Plugins.WeiXinPay.Service.Application
{
    public class PayNotifyManager
    {
        public static bool PayNotify(string payModuleName, PaymentNotify paymentNotify, out string message)
        {
            message = string.Empty;
            IPayNotify payNotify = null;

            //记录通知信息
            SavePayNotifyInfo(payModuleName, paymentNotify);

            //此处应该用到IOC
            //switch (payModule)
            //{
            //    case PayModuleEnum.Restaurant:
            //        payNotify = new RestaurantPayNotify();
            //        break;
            //    case PayModuleEnum.Hotel:
            //        payNotify = new HotelPayNotify();
            //        break;
            //    default:
            //        message = "异常参数支付模块ID";
            //        return false;
            //}

            return payNotify.PayNotify(paymentNotify, out message);
        }

        private static void SavePayNotifyInfo(string payModuleName, PaymentNotify paymentNotify)
        {
            var payNotifyInfo = new WeiXinPayNotifyInfo
            {
                ModuleName = payModuleName,
                NotifyID = Guid.NewGuid(),
                appid = paymentNotify.appid,
                attach = paymentNotify.attach,
                bank_type = paymentNotify.bank_type,
                cash_fee = paymentNotify.cash_fee,
                cash_fee_type = paymentNotify.cash_fee_type,
                coupon_count = paymentNotify.coupon_count,
                coupon_fee = paymentNotify.coupon_fee,
                CreateTime = DateTime.Now,
                device_info = paymentNotify.device_info,
                err_code = paymentNotify.err_code,
                err_code_des = paymentNotify.err_code_des,
                fee_type = paymentNotify.fee_type,
                is_subscribe = paymentNotify.is_subscribe,
                mch_id = paymentNotify.mch_id,
                nonce_str = paymentNotify.nonce_str,
                openid = paymentNotify.openid,
                out_trade_no = paymentNotify.out_trade_no,
                result_code = paymentNotify.result_code,
                return_code = paymentNotify.return_code,
                return_msg = paymentNotify.return_msg,
                sign = paymentNotify.sign,
                time_end = paymentNotify.time_end,
                total_fee = paymentNotify.total_fee,
                trade_type = paymentNotify.trade_type,
                transaction_id = paymentNotify.transaction_id
            };

            payNotifyInfo.Add();
        }
    }
}
