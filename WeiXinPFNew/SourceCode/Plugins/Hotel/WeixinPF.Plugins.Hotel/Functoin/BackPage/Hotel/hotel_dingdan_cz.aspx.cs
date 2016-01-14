using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Hotel.Plugins.Helper;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Web.UI;
using HotelService = WeixinPF.Hotel.Plugins.Functoin.Service.HotelService;

namespace WeixinPF.Hotel.Plugins.Functoin.BackPage.Hotel
{
    public partial class hotel_dingdan_cz : ManagePage
    {
        public int OrderId = 0;
        protected GetHotelOrderResponse Order;
        public string ordername = "";
        public string openid = "";
        public string beizhu = "";
        public int hotelid = 0;
        public string arriveTime = string.Empty;
        public string leaveTime = string.Empty;
        public string Dingdanlist = "";
        public string Dingdanren = "";
        public bool isAdmin = false;//判断是景区还是商户
        public int orderStatus = -1;
        public StatusDict status;
        public TuidanDto tuidan = new TuidanDto();
        public string uName = string.Empty;
        public string roleName = string.Empty;
        public string ordermsg = string.Empty;
        private IList<QrCodeDto> _listCode;

        public hotel_dingdan_cz()
        {
            Order = new GetHotelOrderResponse();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var adminInfo = GetAdminInfo();
            if (adminInfo != null)
            {
                isAdmin = adminInfo.RoleId == 13;//景区管理员
            }
            OrderId = MyCommFun.RequestInt("id");
            hotelid = MyCommFun.RequestInt("hotelid");
            if (Request.Form["__EVENTTARGET"] == "btn_completed")
            {
                // Fire event
                btn_completed_OnClick(this, new EventArgs());
            }
           
            GetData(OrderId);

            if (!IsPostBack)
            {
                GetOrderStatusMsg(Order);
            }

        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="orderId"></param>
        private void GetData(int orderId)
        {
            var orderRequest = new GetHotelOrderByOrderIdRequest()
            {
                OrderId = orderId
            }; 
            var result = BusHelper.Send<GetHotelOrderResponse>(orderRequest);
            Order = result;
            GetIdentifyingCode(orderId);
            GetOrderList(orderId);
            GetUserMsg(Order);
            //            GetOrderStatusMsg(Order);
        }

        /// <summary>
        /// 获取验证码，只有已支付、退款中、退款完成由验证码
        /// </summary>
        /// <param name="orderId"></param>
        private void GetIdentifyingCode(int orderId)
        {
            var request = new GetIdentifyingCodeByOrderRequest()
            {
                OrderId = orderId
            };
            var result = BusHelper.Send<GetIdentifyingCodeByOrderResponse>(request);
            _listCode = result.Codes;
        }

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="order"></param>
        private void GetOrderStatusMsg(GetHotelOrderResponse order)
        {
            orderStatus = order.OrderStatus;
            //支付状态下默认退款金额为订单总额
            if (orderStatus == HotelStatusManager.OrderStatus.Payed.StatusId)
            {
                var result = GetPrice(order);

                txtAmount.Text = result.ToString();
                if (isAdmin)
                {
                    hidConfirmStr.Value = "确定执行【退款】操作吗？";

//                    WHEN 0 THEN ''未支付''
//                    WHEN 1 THEN ''已支付''
//                    WHEN 2 THEN ''已使用''
//                    WHEN 3 THEN ''申请退款''
//                    WHEN 4 THEN ''已退款''
                    if (_listCode != null && _listCode.Any(c => c.Status == 2))//订单中有验证码已使用
                    {
                        hidConfirmStr.Value = string.Format("{0}，{1}", "订单中有验证码已使用", hidConfirmStr.Value);
                    }
                }
            }
            else if (orderStatus == HotelStatusManager.OrderStatus.Refunding.StatusId
                     || orderStatus == HotelStatusManager.OrderStatus.Refunded.StatusId)
            {

              
                 
                tuidan = hotelService.GetModel(order.id, order.hotelid.Value);
                if (tuidan != null && tuidan.operateUser > 0)
                {
                    var manager = new BLL.manager().GetModel(tuidan.operateUser);
                    uName = manager.real_name;
                    roleName = new WeiXinPF.BLL.manager_role().GetTitle(manager.role_id);
                }
            }
        }

        /// <summary>
        /// 获取最大可退价格
        /// </summary>
        /// <param name="order"></param>
        private decimal GetPrice(GetHotelOrderResponse order)
        {
            decimal result = 0;

            var dateSpan = order.LeaveDate - order.ArriveDate;
            result = order.Price * order.OrderNum * dateSpan.Days;//单位分 

            return result;
        }
 
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="dingdanid"></param>
        private void GetOrderList(int orderId)
        {


            if (Order != null)
            {
                if (_listCode != null && _listCode.Any())
                {
                    decimal amount = 0;
                    arriveTime = string.Format("{0:yyyy/MM/dd HH:mm}", Order.ArriveDate);
                    leaveTime = string.Format("{0:yyyy/MM/dd HH:mm}", Order.LeaveDate);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<tr><th>商品名称</th><th class=\"cc\">单价</th><th class=\"cc\">验证码</th><th class=\"cc\">是否验证</th><th class=\"cc\">入住时间</th><th class=\"cc\">离店时间</th> </tr>");
                    foreach (var code in _listCode)
                    {
                        var isUserd = "未验证";
                        if (code.Status == 2)
                        {
                            isUserd = "已验证";
                        }
                        var codeCount = code.Code.Length - 4;
                        var icode = string.Format("****************{0}",
                            code.Code.Substring(codeCount, 4));
                        sb.Append(string.Format(" <tr><td>{0}</td>", Order.RoomType));
                        sb.Append(string.Format("<td class=\"cc\">￥{0}</td>", Order.Price));
                        sb.Append(string.Format("<td class=\"cc\">{0}</td>", icode));
                        sb.Append(string.Format("<td class=\"cc\">{0}</td>", isUserd));

                        sb.Append(string.Format("<td class=\"cc\">{0}</td>", arriveTime));
                        sb.Append(string.Format("<td class=\"cc\">{0}</td>", leaveTime));

                        sb.Append("</tr>");

                    }
                    //总花费
                    

                    amount += GetPrice(Order);

                    sb.AppendFormat("<tr><td></td><td ></td><td ></td><td ></td><td ></td><td class=\"rr\">总计：<span class='text-danger total-money'>￥{0}</span></td></tr>", amount);
                    Dingdanlist = sb.ToString();
                }

            }
        }

        private void GetUserMsg(GetHotelOrderResponse order)
        {

            //订单信息
            if (order != null)
            {
                var request = new GetHotelRequest()
                {
                    HotelId = order.HotelId
                };
                var hotel = BusHelper.Send<GetHotelResponse>(request);
                var createTime = string.Format("{0:yyyy/MM/dd HH:mm}", Order.CreateDate);
                
                Dingdanren += "<tr> <td>酒店商户或门店：" + hotel.Name + "</td></tr>";
                Dingdanren += "<tr> <td>商户或门店编号：" + hotel.Code + "</td></tr>";
                Dingdanren += "<tr><td width=\"70\">订单编号： " + order.OrderNum + "</td></tr>";
                Dingdanren += "<tr> <td>交易日期：" + createTime + "</td></tr>";
                Dingdanren += "<tr><td>预定人：" + order.OrderPersonName + "</td></tr>";
                Dingdanren += "<tr><td>电话：" + order.Tel + "</td></tr>";
                //                dingdanren += "<tr><td>地址：" + manage.address + "</td></tr>";
                //                dingdanren += "<tr><td>备注 ：" + manage.oderRemark + "</td></tr>";

                status = HotelStatusManager.OrderStatus.GetStatusDict(order.OrderStatus);
                Dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='" + status.CssClass
                    + "'>" + status.StatusName + "</em></td></tr>";


            }
            else
            {
                Dingdanren += "<tr> <td>酒店商户或门店：</td></tr>";
                Dingdanren += "<tr> <td>商户或门店编号：</td></tr>";
                Dingdanren += "<tr><td width=\"70\">订单编号：</td></tr>";
                Dingdanren += "<tr> <td>交易日期：</td></tr>";
                Dingdanren += "<tr><td>预定人：</td></tr>";
                Dingdanren += "<tr><td>电话：</td></tr>";

                Dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='no'>未处理</em></td></tr>";
            }
        }

        protected void save_groupbase_Click(object sender, EventArgs e)
        {
            dingdanid = MyCommFun.RequestInt("id");
            string status = StatusType.SelectedItem.Value;
            _hotelOrderService.Update(dingdanid, status);

            AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改酒店状态为" + status + "，主键为" + dingdanid); //记录日志
            JscriptMsg("状态修改成功！", "hotel_dingdan_manage.aspx?hotelid=" + hotelid + "", "Success");
        }

        protected void btnSaveRefund_OnClick(object sender, EventArgs e)
        {
            var wxUserweixin = GetAdminInfo();
            if (wxUserweixin == null)
            {
                throw new Exception("用户不能为空！");
            }
            if (chkIsRefund.Checked)
            {
                double money = MyCommFun.Str2Float(txtAmount.Text);
                var hotelService = new HotelService();
                Order = _hotelOrderService.GetModel(dingdanid);

                var hotel = new BLL.wx_hotels_info().GetModel(Order.hotelid.Value);

                using (var scope = new TransactionScope())
                {
                    var dto = new TuidanDto()
                    {

                        dingdanid = Order.id,
                        hotelid = Order.hotelid.Value,
                        roomid = Order.roomid.Value,
                        openid = Order.openid,
                        wid = hotel.wid.Value,
                        operateUser = wxUserweixin.id,
                        refundAmount = money,
                        refundTime = DateTime.Now,
                        remarks = this.remarks.InnerText,
                        refundCode = "HT" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + Utils.Number(5)

                    };
                    hotelService.AddTuidan(dto);

                    string return_msg = String.Empty;

                    if (WeChatRefund(Order, dto, hotel.wid.Value, out return_msg))//
                    {
                        new BLL.wx_hotel_dingdan().RefundComplete(Order.OrderNumber);

                        AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改酒店状态为" +
                    HotelStatusManager.OrderStatus.Refunding.StatusName
                    + HotelStatusManager.OrderStatus.Refunding.StatusId + "，主键为" + dingdanid); //记录日志
                        JscriptMsg("退款成功！", "hotel_dingdan_manage.aspx?hotelid=" + hotelid + "", "Success");
                    }
                    else
                    {
                        Response.Write(return_msg);
                        GetData(dingdanid);
                    }
                    //                    _hotelOrderService.Update(Order.id, HotelStatusManager.OrderStatus.Refunding.StatusId.ToString());



                    scope.Complete();
                }










            }
        }

        /// <summary>
        /// 微信退单
        /// </summary>
        /// <param name="dingdan"></param>
        /// <param name="dto"></param>
        /// <param name="returnMsg"></param>
        /// <returns></returns>
        private bool WeChatRefund(wx_hotel_dingdan dingdan, TuidanDto dto, int wid, out string returnMsg)
        {
            bool result = false;
            returnMsg = null;

            var refundResult = _hotelOrderService.GetWeChatRefundParams(wid, dingdan.hotelid.Value, dingdan.id, dto.refundCode);

            //使用系统订单号退单
            if (refundResult != null && refundResult.Tables.Count > 0 && refundResult.Tables[0].Rows.Count > 0)
            {
                var orderNumber = refundResult.Tables[0].Rows[0]["orderNumber"].ToString();
                var transaction_id = refundResult.Tables[0].Rows[0]["transaction_id"].ToString();
                var refundAmount = Convert.ToInt32(refundResult.Tables[0].Rows[0]["refundAmount"]);
                var payAmount = Convert.ToInt32(refundResult.Tables[0].Rows[0]["payAmount"]);


                var wxModel = new BLL.wx_userweixin().GetModel(wid);
                var payInfo = new BLL.wx_payment_wxpay().GetModelByWid(wid);

                var requestHandler = new RequestHandler(null);
                requestHandler.SetParameter("out_trade_no", orderNumber);
                //requestHandler.SetParameter("transaction_id", transaction_id);
                requestHandler.SetParameter("out_refund_no", dto.refundCode);
                requestHandler.SetParameter("appid", wxModel.AppId);
                requestHandler.SetParameter("mch_id", payInfo.mch_id);//商户号
                requestHandler.SetParameter("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));

                //退款金额
                if (PayHelper.IsDebug)
                {
                    requestHandler.SetParameter("total_fee", (payAmount).ToString());
                    requestHandler.SetParameter("refund_fee", (refundAmount).ToString());
                }
                else
                {
                    requestHandler.SetParameter("total_fee", (payAmount * 100).ToString());
                    requestHandler.SetParameter("refund_fee", (refundAmount * 100).ToString());
                }

                requestHandler.SetParameter("op_user_id", wxModel.AppId);
                requestHandler.SetParameter("sign", requestHandler.CreateMd5Sign("key", payInfo.paykey));

                var refundInfo = TenPayV3Helper.Refund(requestHandler.ParseXML(), string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, payInfo.certInfoPath), payInfo.cerInfoPwd);
                var refundOrderResponse = new RefundOrderResponse(refundInfo);

                result = refundOrderResponse.IsSuccess;
                returnMsg = refundOrderResponse.return_msg;
            }

            return result;
        }


        protected void btn_completed_OnClick(object sender, EventArgs e)
        {

            Order = _hotelOrderService.GetModel(dingdanid);
            _hotelOrderService.Update(Order.id, HotelStatusManager.OrderStatus.Completed.StatusId.ToString());

            AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改酒店状态为" +
                   HotelStatusManager.OrderStatus.Completed.StatusName
                   + HotelStatusManager.OrderStatus.Completed.StatusId + "，主键为" + dingdanid); //记录日志
            JscriptMsg("修改成功！", "hotel_dingdan_manage.aspx?hotelid=" + hotelid + "", "Success");
        }



        protected void btn_return_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("hotel_dingdan_manage.aspx?hotelid=" + hotelid);
        }
    }
}