using System;
using System.Threading;
using NServiceBus;
using WeixinPF.Application.Agent;
using WeixinPF.Application.Agent.Service;
using WeixinPF.Common.Enum;
using WeixinPF.Infrastructure.Agent;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Shared;
using WeixinPF.Web.UI;

//using WeixinPF.Plugins.Hotel.Functoin.BackPage.BasePage;

//using WeixinPF.Plugins.Hotel.Functoin.BackPage.BasePage;

namespace WeixinPF.Hotel.Plugins.Functoin.BackPage.Hotel
{
    public partial class dingdan_confirm : ManagePage
    {
        public int hotelid = 0;
        public string Dingdanlist = "";
        public string dingdanren = "";

        public int wid = 0;

        public const string ModuleName = "hotel";

        protected void Page_Load(object sender, EventArgs e)
        {
#if DEBUG
            var bll = new ManagerInfoService();
            var model = bll.GetModel("ht", "123456", true);
            if (model == null)
            {
                //msgtip.InnerHtml = "用户名或密码有误，请重试！";
                return;
            }
            Session[SystemKeys.SESSION_ADMIN_INFO] = model;
            Session.Timeout = 45;
#endif

            hotelid = this.GetHotelId();
            wid = 1;//this.GetWeiXinCode().id;
            confirmnumber.CausesValidation = true;
        }


        #region 验证订单
        protected void confirm_dingdan_Click(object sender, EventArgs e)
        {
            var number = this.confirmnumber.Text.Trim();
            GetIdentifyingCodeResponse identifyingCode = null;
            GetHotelOrderResponse hotelOrder = null;

            var result = Global.Bus
                .Send<GetIdentifyingCodeResponse>("WeixinPF.Hotel.Plugins.Service",
                    new GetIdentifyingCodeRequest()
                    {
                        ShopId = this.hotelid,
                        Number = number,
                        ModuleName = ModuleName,
                        Wid = wid
                    });

            if (!result.IsSuccess)
            {
                return;
            }
            else
            {
                identifyingCode = result.Data;
            }

           var orderResult = Global.Bus
                .Send<GetHotelOrderResponse>("WeixinPF.Hotel.Plugins.Service",
                    new GetHotelOrderByOrderIdRequest()
                    {
                        OrderId = int.Parse(identifyingCode.OrderId)
                    });

            if (!orderResult.IsSuccess)
            {
                return;
            }
            else
            {
                hotelOrder = orderResult.Data;
            }

            if (hotelOrder.OrderStatus.Equals(HotelStatusManager.OrderStatus.Refunded.StatusId)
                        || hotelOrder.OrderStatus.Equals(HotelStatusManager.OrderStatus.Refunding.StatusId)
                        || hotelOrder.OrderStatus.Equals(HotelStatusManager.OrderStatus.Completed))
            {
                this.Response.Write(
                    "<script language='javascript' type='text/javascript'>alert('该订单已完成或进行退单处理，不能进行验证！')</script>");
            }
            else
            {
                if (identifyingCode.Status != 1)
                {
                    if (identifyingCode.Status == 0)
                    {
                        this.Response.Write("<script language='javascript' type='text/javascript'>alert('该商品未付款！')</script>");
                    }
                    else
                    {
                        this.Response.Write("<script language='javascript' type='text/javascript'>alert('该商品已消费或者退单，请确认！')</script>");

                    }
                }
                else
                {
                    this.Response.Redirect("commodity_detail.aspx?cid=" + identifyingCode.IdentifyingCodeId + "&shopid=" + identifyingCode.ShopId + "&id=" + identifyingCode.OrderId);
                }
            }           
        }

        protected void confirmnumber_Validating()
        {

        }
        #endregion
    }
}