using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeixinPF.Common;
using WeixinPF.Common.Helper;
using WeixinPF.Hotel.Plugins.Controller;
using WeixinPF.Model.WeiXin.Pay;

namespace WeixinPF.Hotel.Plugins.Functoin.BackPage
{
    public partial class hotel_order_paycallback : System.Web.UI.Page
    {
        private string _orderId;
        private string openid;
        private string hotelid;
        private string roomid;
        protected string newUrl = string.Empty;
        private string wid;
        protected void Page_Load(object sender, EventArgs e)
        {
            var payData = Request.QueryString["payData"];
            var ticket = Request.QueryString["ticket"];
            var payStatus = Request.QueryString["payStatus"];
            if (!string.IsNullOrEmpty(payData) && !string.IsNullOrEmpty(ticket))
            {
                payData = EncryptionManager.AESDecrypt(payData, ticket);

                var payDataModel = JSONHelper.Deserialize<UnifiedOrderInfo>(payData);

                if (payDataModel != null)
                {

                    _orderId = payDataModel.Extra["orderId"];
                    openid = payDataModel.Extra["openid"];
                    hotelid = payDataModel.Extra["hotelid"];
                    roomid = payDataModel.Extra["roomid"];
                    wid = payDataModel.Extra["wid"];
                    JumpUrl(payStatus);
                }
                else
                {
                    _orderId = Request.QueryString["orderId"];
                    openid = Request.QueryString["openid"];
                    hotelid = Request.QueryString["hotelid"];
                    roomid = Request.QueryString["roomid"];
                    JumpUrl(payStatus);
                }
            }
            else
            {
                Response.Clear();
                Response.Write("不完整的参数");
            }
        }

        /// <summary>
        /// 根据状态跳转不同页面
        /// </summary>
        /// <param name="payStatus"></param>
        private void JumpUrl(string payStatus)
        {
            var url = string.Empty;
            url = string.Format("../StaticPage/hotel/index.html?openid={0}&wid={1}&hotelid={2}#/order", openid, wid, hotelid, _orderId, payStatus);
            newUrl = url;
            Response.Redirect(url);
        }


    }
}