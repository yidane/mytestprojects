using System;
using System.Web.UI;
using OneGulp.WeChat.MP.Helpers;
using OneGulp.WeChat.MP.TenPayLibV3.TenPayV3.Model;
using WeixinPF.Application.Weixin.Pay.Service;
using WeixinPF.Application.Weixin.Token.Service;
using WeixinPF.Common;
using WeixinPF.Common.Helper;
using WeixinPF.Model.WeiXin.Pay;
using WeixinPF.Model.WeiXin.Token;

namespace WeixinPF.Web.Functoin.BackPage.WeiXin.WeChatPay
{
    public partial class Pay : Page
    {
        public int AppId;
        //支付参数
        public JsAPIParameter JsApiParameter = new JsAPIParameter();
        public string PayComplete;
        public int InitPayComplete = 0;

        //JsAPI初始化参数
        public JsApiTicket JsApiTicket = new JsApiTicket();
        public string JsApiNonceStr;
        public string JsApiTimeStamp;
        public string JsApiSingature;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //读取支付参数
                var payData = Request.QueryString["payData"];
                var ticket = Request.QueryString["ticket"];
                if (string.IsNullOrEmpty(payData) || string.IsNullOrEmpty(ticket))
                {
                    Response.Clear();
                    Response.Write("不完整的支付参数");
                    return;
                }

                //解析支付参数
                payData = EncryptionManager.AESDecrypt(payData, ticket);
                var payDataModel = JSONHelper.Deserialize<UnifiedOrderInfo>(payData);
                if (payDataModel == null)
                {
                    Response.Clear();
                    Response.Write("解析支付参数为空");
                    return;
                }

                //判断是否存在相同订单号
                //TODO:判断下单逻辑

                //统一下单
                string payErrorMessage;
                JsApiParameter = new UnifiedOrderService().UnifiedOrder(payDataModel, out payErrorMessage);
                if (JsApiParameter == null)
                {
                    Response.Clear();
                    Response.Write(payErrorMessage);
                    return;
                }

                //注册支付脚本
                var jsApiTicketService = new JsApiTicketService();
                string jsApiTicketMessage;
                var jsApiTicket = jsApiTicketService.GetJsApiTicket(payDataModel.AppId, out jsApiTicketMessage);
                if (jsApiTicket == null)
                {
                    Response.Clear();
                    Response.Write(jsApiTicketMessage);
                    return;
                }

                JsApiNonceStr = JSSDKHelper.GetNoncestr();
                JsApiTimeStamp = JSSDKHelper.GetTimestamp();
                JsApiSingature = new JSSDKHelper().GetSignature(jsApiTicket.Ticket, JsApiNonceStr, JsApiTimeStamp, Request.Url.ToString());

                //支付初始化完成
                InitPayComplete = 1;
            }
            catch (Exception exception)
            {
                Response.Clear();
                Response.Write(exception.Message);
            }
        }
    }
}