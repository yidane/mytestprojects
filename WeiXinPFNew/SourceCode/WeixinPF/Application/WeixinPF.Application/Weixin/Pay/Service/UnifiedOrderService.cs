using System;
using System.Web;
using OneGulp.WeChat.MP;
using OneGulp.WeChat.MP.TenPayLibV3;
using OneGulp.WeChat.MP.TenPayLibV3.TenPayV3;
using OneGulp.WeChat.MP.TenPayLibV3.TenPayV3.Model;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Common;
using WeixinPF.Common.Helper;
using WeixinPF.Model.WeiXin.Pay;
using IPaymentInfoRepository = WeixinPF.Application.Weixin.Pay.Repository.IPaymentInfoRepository;

namespace WeixinPF.Application.Weixin.Pay.Service
{
    public class UnifiedOrderService
    {
        /// <summary>
        ///     统一下单
        /// </summary>
        /// <param name="unifiedOrderInfo"></param>
        /// <param name="errMessage"></param>
        /// <returns></returns>
        public JsAPIParameter UnifiedOrder(UnifiedOrderInfo unifiedOrderInfo, out string errMessage)
        {
            if (unifiedOrderInfo == null)
            {
                errMessage = "下单数据为空";
                return null;
            }

            if (!unifiedOrderInfo.CheckRequired(out errMessage))
            {
                return null;
            }

            var paymentInfoRepository = DependencyManager.Resolve<IPaymentInfoRepository>();

            //判断当前订单号是否存在
            var orderInfo = paymentInfoRepository.GetPaymentInfoBySysOrderNo(unifiedOrderInfo.PayModuleName,
                unifiedOrderInfo.OutTradeNo);
            if (orderInfo != null && orderInfo.Status != 0 && orderInfo.OpenId.Equals(unifiedOrderInfo.Openid))
            //0表示尚未支付
            {
                errMessage = "系统异常，请重新下单操作";
                return null;
            }

            //调用微信统一下单接口
            var appInfo = DependencyManager.Resolve<IAppInfoRepository>().GetAppInfo(unifiedOrderInfo.AppId);
            var payment =
                DependencyManager.Resolve<Weixin.Repository.IPaymentInfoRepository>()
                    .GetModelByAppId(unifiedOrderInfo.AppId);
            if (string.IsNullOrEmpty(appInfo.AppId) || string.IsNullOrEmpty(payment.MchId) ||
                string.IsNullOrEmpty(payment.Paykey))
            {
                errMessage = "当前公众号支付配置不完整";
                return null;
            }


            var packageReqHandler = new RequestHandler(default(HttpContext));
            //初始化
            packageReqHandler.Init();

            var nonceStr = TenPayV3Util.GetNoncestr();

            //设置package订单参数
            packageReqHandler.SetParameter("appid", appInfo.AppId); //公众账号ID
            packageReqHandler.SetParameter("mch_id", payment.MchId); //商户号
            packageReqHandler.SetParameter("nonce_str", nonceStr); //随机字符串
            packageReqHandler.SetParameter("body", unifiedOrderInfo.Body); //商品描述
            packageReqHandler.SetParameter("attach", unifiedOrderInfo.PayModuleName); //向微信传递系统支付模块ID
            packageReqHandler.SetParameter("out_trade_no", unifiedOrderInfo.OutTradeNo); //商家订单号

            //debug模式下，单位为
            packageReqHandler.SetParameter("total_fee", PayHelper.IsDebug
                ? (unifiedOrderInfo.TotalFee / 100).ToString()
                : (unifiedOrderInfo.TotalFee * 100).ToString());

            packageReqHandler.SetParameter("spbill_create_ip", "1.1.1.1"); //用户的公网ip，不是商户服务器IP

            packageReqHandler.SetParameter("notify_url", PayHelper.GetPayNotifyUrl()); //接收财付通通知的URL
            packageReqHandler.SetParameter("trade_type", TenPayV3Type.JSAPI.ToString()); //交易类型
            packageReqHandler.SetParameter("openid", unifiedOrderInfo.Openid); //用户的openId

            var sign = packageReqHandler.CreateMd5Sign("key", payment.Paykey);
            packageReqHandler.SetParameter("sign", sign); //签名

            var data = packageReqHandler.ParseXML();

            //同意下单，获取到预付订单号
            var unifiedOrderResult = TenPayV3Helper.Unifiedorder(data);
            var rtnUnifiedOrderResult = new UnifiedOrderResponse(unifiedOrderResult);

            //下单成功，保存下单对象
            if (rtnUnifiedOrderResult.IsSuccess)
            {
                var paymentInfo = new PaymentInfo
                {
                    PaymentId = Guid.NewGuid(),
                    AppId = unifiedOrderInfo.AppId,
                    CreateTime = DateTime.Now,
                    Description = "无",
                    Body = unifiedOrderInfo.Body,
                    ModuleName = unifiedOrderInfo.PayModuleName,
                    OrderCode = unifiedOrderInfo.OutTradeNo,
                    OrderId = unifiedOrderInfo.OrderId,
                    OpenId = unifiedOrderInfo.Openid,
                    PayAmount = unifiedOrderInfo.TotalFee,
                    WxOrderCode = rtnUnifiedOrderResult.prepay_id,
                    ModifyTime = DateTime.Now,
                    Status = 0
                };

                paymentInfoRepository.Add(paymentInfo);

                var jsApiParameters = rtnUnifiedOrderResult.GetJsApiParameters(payment.Paykey);

                return jsApiParameters;
            }

            errMessage = "下单失败";
            return null;
        }

        /// <summary>
        ///     生成支付链接
        /// </summary>
        /// <param name="unifiedOrderInfo"></param>
        /// <returns></returns>
        public string GeneratePayUrl(UnifiedOrderInfo unifiedOrderInfo)
        {
            var ticket = EncryptionManager.CreateIV();
            var payData = EncryptionManager.AESEncrypt(JSONHelper.Serialize(unifiedOrderInfo, "yyyy-MM-dd"), ticket);

            return PayHelper.GetPayUrl(payData, ticket);
        }
    }
}