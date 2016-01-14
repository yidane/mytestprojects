﻿using System;
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
        private string _openid;
        private string _hotelid;
        private string _roomid;
        protected string NewUrl = string.Empty;
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

                    _orderId = payDataModel.Extra["OrderId"];
                    _openid = payDataModel.Extra["_openid"];
                    _hotelid = payDataModel.Extra["_hotelid"];
                    _roomid = payDataModel.Extra["_roomid"];
                    wid = payDataModel.Extra["wid"];
                    JumpUrl(payStatus);
                }
                else
                {
                    _orderId = Request.QueryString["OrderId"];
                    _openid = Request.QueryString["_openid"];
                    _hotelid = Request.QueryString["_hotelid"];
                    _roomid = Request.QueryString["_roomid"];
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
            url = string.Format("../../../StaticPage/hotel/index.html?_openid={0}&wid={1}&_hotelid={2}#/order", _openid, wid, _hotelid, _orderId, payStatus);
            NewUrl = url;
            Response.Redirect(url);
        }


    }
}