using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinPF.Plugins.WeiXinPay.Service.Models
{
    [Table("WeiXin_Payment_UnifiedOrderInfo")]
    public class WeiXinUnifiedOrderInfo
    {
        /// <summary>
        /// 微信号ID
        /// </summary>
        public int wid { get; set; }

        /// <summary>
        /// 支付模块ID
        /// </summary>
        public int PayModuleID { get; set; }

        /// <summary>
        /// 业务系统订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 支付费用，单位：分
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 支付的OpenID
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 支付前
        /// </summary>
        public string BeforePay { get; set; }

        /// <summary>
        /// 支付成功
        /// </summary>
        public string PaySuccess { get; set; }

        /// <summary>
        /// 支付失败
        /// </summary>
        public string PayFail { get; set; }

        /// <summary>
        /// 支付取消
        /// </summary>
        public string PayCancel { get; set; }

        /// <summary>
        /// 支付完成
        /// </summary>
        public string PayComplete { get; set; }

        /// <summary>
        /// 额外参数
        /// </summary>
        public Dictionary<string, string> Extra = new Dictionary<string, string>();

        /// <summary>
        /// 检查必须参数
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CheckRequired(out string message)
        {
            var stringBuilder = new StringBuilder();
            const string msg = "{0}必须赋值{1}";
            if (wid <= 0)
                stringBuilder.AppendFormat(msg, "wid", Environment.NewLine);

            if (PayModuleID < 0)
                stringBuilder.AppendFormat(msg, "PayModuleID", Environment.NewLine);

            if (string.IsNullOrEmpty(OrderId))
                stringBuilder.AppendFormat(msg, "OrderId", Environment.NewLine);

            if (string.IsNullOrEmpty(body))
                stringBuilder.AppendFormat(msg, "body", Environment.NewLine);

            if (string.IsNullOrEmpty(out_trade_no))
                stringBuilder.AppendFormat(msg, "out_trade_no", Environment.NewLine);

            if (total_fee <= 0)
                stringBuilder.AppendFormat(msg, "total_fee", Environment.NewLine);

            if (string.IsNullOrEmpty(openid))
                stringBuilder.AppendFormat(msg, "openid", Environment.NewLine);

            if (stringBuilder.Length > 0)
            {
                message = stringBuilder.ToString();
                return false;
            }

            message = string.Empty;
            return true;
        }
    }
}
