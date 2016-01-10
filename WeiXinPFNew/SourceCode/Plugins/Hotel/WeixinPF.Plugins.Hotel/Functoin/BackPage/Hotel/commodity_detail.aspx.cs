using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Messages.Command;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Shared;
using WeixinPF.Web.UI;

namespace WeixinPF.Hotel.Plugins.Functoin.BackPage.Hotel
{
    public partial class commodity_detail : ManagePage
    {
        public string Dingdanlist = "";
        public string dingdanren = "";
        public string id = "";
        public string cid = "";
        public int ids = 0;
        public int hotelid = 0;
        public string openid = "";

        public int wid = 0;

        public const string ModuleName = "hotel";
        public const string HotelServiceName = "WeixinPF.Hotel.Plugins.Service";
        protected void Page_Load(object sender, EventArgs e)
        {
            ids = MyCommFun.RequestInt("id");
            id = MyCommFun.QueryString("id");
            cid = MyCommFun.QueryString("cid");
            hotelid = this.GetHotelId();
            wid = this.GetWeiXinCode().id;
            if (!IsPostBack)
            {
                if (ids != 0)
                {
                    List(ids);
                }
            }
        }

        protected void save_groupbase_Click(object sender, EventArgs e)
        {
            Guid identifyingCodeId;

            if (Guid.TryParse(this.cid, out identifyingCodeId))
            {
                GetIdentifyingCodeResponse identifyingCodeObject = null;

                var result = Global.Bus.Send<GetIdentifyingCodeResponse>(HotelServiceName,
                    new GetByIdnetifyingCodeIdRequest()
                    {
                        IdentifyingCodeId = identifyingCodeId,
                        ModuleName = ModuleName,
                        Wid = this.wid
                    });

                if (!result.IsSuccess)
                {
                    this.Response.Write("<script language='javascript' type='text/javascript'>alert('该订单不存在或未付款，请确认！')</script>");

                    return;
                }
                else
                {
                    identifyingCodeObject = result.Data;
                }               


                var orderRequest = new GetHotelOrderByOrderIdRequest()
                                    {
                                        OrderId = int.Parse(identifyingCodeObject.OrderId)
                                    };
                GetHotelOrderResponse order = null;

                result = Global.Bus.Send<GetHotelOrderResponse>(HotelServiceName, orderRequest);               

                if (!result.IsSuccess)
                {
                    this.Response.Write(
                        "<script language='javascript' type='text/javascript'>alert('该订单不存在或未付款，请确认！')</script>");
                    return;
                }
                else
                {
                    order = result.Data;
                }

                if (order != null)
                {
                    if (order.OrderStatus.Equals(HotelStatusManager.OrderStatus.Refunded.StatusId) ||
                        order.OrderStatus.Equals(HotelStatusManager.OrderStatus.Refunding.StatusId)
                        || order.OrderStatus.Equals(HotelStatusManager.OrderStatus.Completed))
                    {
                        this.Response.Write(
                            "<script language='javascript' type='text/javascript'>alert('该订单已完成或进行退单处理，不能进行验证！')</script>");
                        return;
                    }
                    else if (identifyingCodeObject.ShopId.Equals(this.hotelid.ToString(CultureInfo.InvariantCulture)))
                    {
                        var useIdentifyingCode = new MakeUseOfIdentifyingCode() {IdentifyingCodeId = identifyingCodeId};

                        Global.Bus.MyBus.Send(HotelServiceName, useIdentifyingCode)
                            .Register<int>(response =>
                            {
                                if (response == 1)
                                {
                                    AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改支付状态，主键为" + id);
                                        //记录日志                                                                                                   //Response.Redirect("dingdan_confirm.aspx?shopid=" + shopid + "");
                                    Response.Write(
                                        "<script language='javascript' type='text/javascript'>alert('核销成功！');location.href = 'dingdan_confirm.aspx?shopid=" +
                                        hotelid + "';</script>");
                                }
                                else
                                {
                                    AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改支付状态失败，主键为" + id);
                                        //记录日志                                                                                                   //Response.Redirect("dingdan_confirm.aspx?shopid=" + shopid + "");
                                    Response.Write(
                                        "<script language='javascript' type='text/javascript'>alert('核销失败');</script>");
                                }
                            });
                    }
                    else
                    {
                        this.Response.Write(
                            "<script language='javascript' type='text/javascript'>alert('核销失败。')</script>");
                    }
                }
                else
                {
                    this.Response.Write(
                        "<script language='javascript' type='text/javascript'>alert('该订单不存在或未付款，请确认！')</script>");
                }                                    
            }
        }

        public void List(int ids)
        {
            //订单
            Dingdanlist = "";
            dingdanren = "";

            var searchRequest = new GetIdentifyingCodeDetailRequest()
            {
                IdentifyingCodeId = Guid.Parse(cid),
                ModuleName = "hotel"
            };

            GetIdentifyingCodeDetailResponse searchResponse = null;

            var result = Global.Bus.Send<GetIdentifyingCodeDetailResponse>(HotelServiceName, searchRequest);

            if (!result.IsSuccess)
            {
                this.Response.Write("<script language='javascript' type='text/javascript'>alert('该订单不存在或未付款，请确认！');location.href = 'dingdan_confirm.aspx?shopid=" +
                        hotelid + "';</script>");
                this.Response.End();

            }
            else
            {
                searchResponse = result.Data;
            }

            decimal amount = 0;

            if (searchResponse.Details.FirstOrDefault().Status == 2)
            {
                save_groupbase.Text = "已验证";
                save_groupbase.Enabled = false;
                save_groupbase.Style.Value = "";
            }
            Dingdanlist += "<tr><th>商品名称</th><th class=\"cc\">购买数量</th><th class=\"cc\">单价</th><th class=\"cc\">入住时间</th><th class=\"cc\">离店时间</th></tr>";
            foreach (var item in searchResponse.Details)
            {
                Dingdanlist += " <tr><td class=\"cc\">" + item.ProductName + "</td>";
                Dingdanlist += "<td class=\"cc\">" + item.Number + "</td>";
                Dingdanlist += "<td class=\"cc\">" + item.Price + "</td>";
                Dingdanlist += "<td class=\"cc\">" + item.ArriveTime + "</td>";
                Dingdanlist += "<td class=\"cc\">" + item.LeaveTime + "</td></tr>";
                amount += Convert.ToDecimal(item.TotelPrice);
            }
            Dingdanlist += "<tr><td></td><td ></td><td ></td><td ></td><td class=\"rr\" style=\"color: red; font-weight:bold;\">支付总计：￥" + amount + "</td></tr>";
            

            GetHotelOrderResponse orderResponse = null;

            result = Global.Bus.Send<GetHotelOrderResponse>(HotelServiceName,
                new GetHotelOrderByOrderIdRequest() {OrderId = int.Parse(id)});

            if (!result.IsSuccess)
            {
                this.Response.Write("<script language='javascript' type='text/javascript'>alert('该订单不存在或未付款，请确认！');location.href = 'dingdan_confirm.aspx?shopid=" +
                                        hotelid + "';</script>");
                this.Response.End();
            }
            else
            {
                orderResponse = result.Data;
            }

            //订单信息
            if (orderResponse != null)
            {
                dingdanren += "<tr><td width=\"70\">订单编号： " + orderResponse.OrderNumber + "</td></tr>";
                dingdanren += "<tr> <td>交易日期：" + orderResponse.OrderDate + "</td></tr>";
                dingdanren += "<tr><td>预定人：" + orderResponse.OrderPersonName + "</td></tr>";
                dingdanren += "<tr><td>电话：" + orderResponse.Tel + "</td></tr>";
            }
            else
            {
                dingdanren += "<tr><td width=\"70\">订单编号：</td></tr>";
                dingdanren += "<tr> <td>交易日期：</td></tr>";
                dingdanren += "<tr><td>预定人：</td></tr>";
                dingdanren += "<tr><td>电话：</td></tr>";
            }
        }
    }
}