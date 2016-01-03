using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeixinPF.Plugins.WeiXin.WeChatPay
{
    public partial class Pay : System.Web.UI.Page
    {
        //注册微信支付脚本
        public int IsRegister = 0;
        public string appId = string.Empty;
        public string timestamp = string.Empty;
        public string nonceStr = string.Empty;
        public string signature = string.Empty;

        //支付参数
        protected readonly int wid = 0;
        protected readonly string body = string.Empty;
        protected readonly string payModuleID = string.Empty;
        protected readonly string out_trade_no = string.Empty;
        protected int total_fee = 0;
        protected readonly string openid = string.Empty;
        protected readonly string OrderID = string.Empty;

        //事件
        protected readonly string BeforePay = string.Empty;
        protected readonly string PaySuccess = string.Empty;
        protected readonly string PayFail = string.Empty;
        protected readonly string PayCancel = string.Empty;
        protected readonly string PayComplete = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            var payData = Request.QueryString["payData"];
            var ticket = Request.QueryString["ticket"];

            if (!string.IsNullOrEmpty(payData) && !string.IsNullOrEmpty(ticket))
            {
                //payData = EncryptionManager.AESDecrypt(payData, ticket);

                //var payDataModel = JSONHelper.Deserialize<UnifiedOrderEntity>(payData);

                //if (payDataModel != null)
                //{
                //    //赋值支付参数
                //    wid = payDataModel.wid;
                //    body = payDataModel.body;
                //    payModuleID = payDataModel.PayModuleID.ToString();
                //    out_trade_no = payDataModel.out_trade_no;
                //    total_fee = payDataModel.total_fee;
                //    openid = payDataModel.openid;
                //    OrderID = payDataModel.OrderId;

                //    PayComplete = string.Format("{0}{1}{2}&payStatus=",
                //                                                        payDataModel.PayComplete,
                //                                                        payDataModel.PayComplete.IndexOf("?", System.StringComparison.Ordinal) > 0 ? "&" : "?",
                //                                                        Request.Url.Query.Substring(1));
                //}
            }
            else
            {
                Response.Clear();
                Response.Write("不完整的参数");
            }
        }

        /// <summary>
        /// 注册支付脚本
        /// <param name="weixinId"></param>
        /// <param name="errorMessage"></param>
        /// </summary>
        public bool RegisterJWeiXin(int weixinId, out string errorMessage)
        {
            //try
            //{
            //    errorMessage = string.Empty;
            //    var ticket = WeiXinCRMComm.getJsApiTicket(weixinId, out errorMessage);
            //    if (!string.IsNullOrEmpty(errorMessage))
            //        return false;

            //    var wxModel = new BLL.wx_userweixin().GetModel(weixinId);
            //    if (wxModel == null)
            //    {
            //        errorMessage = "不合法的参数wid";
            //        return false;
            //    }

            //    appId = wxModel.AppId;
            //    nonceStr = JSSDKHelper.GetNoncestr();
            //    timestamp = JSSDKHelper.GetTimestamp();
            //    signature = new JSSDKHelper().GetSignature(ticket, nonceStr, timestamp, Request.Url.ToString());

            //    IsRegister = 1;
            //    return true;
            //}
            //catch (Exception exception)
            //{
            //    errorMessage = exception.Message;
            //    return false;
            //}

            errorMessage = string.Empty;
            return false;
        }
    }
}