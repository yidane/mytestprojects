using System;
using OneGulp.WeChat.MP.TenPayLibV3.TenPayV3.Model;
using WeiXinPF.Plugins.WeiXinPay.Service.Application.Repository;
using WeiXinPF.Plugins.WeiXinPay.Service.Models;

namespace WeiXinPF.Plugins.WeiXinPay.Service.Common
{
    public class PayNotifyManager
    {
        public static bool PayNotify(string payModuleName, PaymentNotify paymentNotify, out string message)
        {
            message = string.Empty;
            IPayNotifyRepository payNotify = null;

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
                NotifyId = Guid.NewGuid(),
                Appid = paymentNotify.appid,
                Attach = paymentNotify.attach,
                BankType = paymentNotify.bank_type,
                CashFee = paymentNotify.cash_fee,
                CashFeeType = paymentNotify.cash_fee_type,
                CouponCount = paymentNotify.coupon_count,
                CouponFee = paymentNotify.coupon_fee,
                CreateTime = DateTime.Now,
                DeviceInfo = paymentNotify.device_info,
                ErrCode = paymentNotify.err_code,
                ErrCodeDes = paymentNotify.err_code_des,
                FeeType = paymentNotify.fee_type,
                IsSubscribe = paymentNotify.is_subscribe,
                MchId = paymentNotify.mch_id,
                NonceStr = paymentNotify.nonce_str,
                Openid = paymentNotify.openid,
                OutTradeNo = paymentNotify.out_trade_no,
                ResultCode = paymentNotify.result_code,
                ReturnCode = paymentNotify.return_code,
                ReturnMsg = paymentNotify.return_msg,
                Sign = paymentNotify.sign,
                TimeEnd = paymentNotify.time_end,
                TotalFee = paymentNotify.total_fee,
                TradeType = paymentNotify.trade_type,
                TransactionId = paymentNotify.transaction_id
            };

            payNotifyInfo.Add();
        }
    }
}