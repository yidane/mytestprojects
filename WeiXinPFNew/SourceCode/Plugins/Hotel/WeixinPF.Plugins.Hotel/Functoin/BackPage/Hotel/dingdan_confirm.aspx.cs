using System;
using System.Threading;
using NServiceBus;
using WeixinPF.Application.Agent;
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
            var bll = new ManagerService(new ManagerRepository(siteConfig.sysdatabaseprefix));
            var model = bll.GetModel("ht", "123456", true);
            if (model == null)
            {
                //msgtip.InnerHtml = "用户名或密码有误，请重试！";
                return;
            }
            Session[MXKeys.SESSION_ADMIN_INFO] = model;
            Session.Timeout = 45;
#endif

            hotelid = this.GetHotelId();
            wid = this.GetWeiXinCode().id;
            confirmnumber.CausesValidation = true;
        }


        #region 验证订单
        protected void confirm_dingdan_Click(object sender, EventArgs e)
        {
            var number = this.confirmnumber.Text.Trim();
            GetIdentifyingCodeResponse identifyingCode = null;
            GetHotelOrderResponse hotelOrder = null;

            


            IAsyncResult resIdentifyingCode = BusEntry.dictBus["hotel"].Send("WeixinPF.Hotel.Plugins", new GetIdentifyingCodeRequest()
            {
                ShopId = this.hotelid,
                Number = number,
                ModuleName = ModuleName,
                Wid = wid
            }).Register(response =>
            {
                CompletionResult localResult = (CompletionResult)response.AsyncState;
                identifyingCode = localResult.Messages[0] as GetIdentifyingCodeResponse;
            }, this);            

            WaitHandle asyncWaitHandle = resIdentifyingCode.AsyncWaitHandle;
            asyncWaitHandle.WaitOne(10000);

            if (!resIdentifyingCode.IsCompleted || identifyingCode == null)
            {
                return;
            }

            IAsyncResult resOrder = BusEntry.dictBus["hotel"].Send("WeixinPF.Hotel.Plugins", new GetHotelOrderByOrderIdRequest()
            {
                OrderId = int.Parse(identifyingCode.OrderId)
            }).Register(response =>
            {
                CompletionResult localResult = (CompletionResult)response.AsyncState;
                hotelOrder = localResult.Messages[0] as GetHotelOrderResponse;
            }, this);

            WaitHandle asyncOrderWaitHandle = resOrder.AsyncWaitHandle;
            asyncOrderWaitHandle.WaitOne(10000);


            if (!resOrder.IsCompleted || hotelOrder == null)
            {
                return;
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
                    //this.Response.Write("<script language='javascript' type='text/javascript'>alert('请确认！')</script>");

                    this.Response.Redirect("commodity_detail.aspx?cid=" + identifyingCode.IdentifyingCodeId + "&shopid=" + identifyingCode.ShopId + "&id=" + identifyingCode.OrderId);
                }
            }



            //var identifyingCode = IdentifyingCodeService.GetConfirmIdentifyingCodeInfo(this.hotelid, number, ModuleName, wid);

            //if (identifyingCode != null)
            //{
            //    var order = new BLL.wx_hotel_dingdan().GetModel(int.Parse(identifyingCode.OrderId));

            //    if (order != null)
            //    {
            //        if (order.orderStatus.Value.Equals(HotelStatusManager.OrderStatus.Refunded.StatusId)
            //            || order.orderStatus.Value.Equals(HotelStatusManager.OrderStatus.Refunding.StatusId)
            //            || order.orderStatus.Value.Equals(HotelStatusManager.OrderStatus.Completed))
            //        {
            //            this.Response.Write(
            //                "<script language='javascript' type='text/javascript'>alert('该订单已完成或进行退单处理，不能进行验证！')</script>");
            //        }
            //        else
            //        {
            //            if (identifyingCode.Status != 1)
            //            {
            //                if (identifyingCode.Status == 0)
            //                {
            //                    this.Response.Write("<script language='javascript' type='text/javascript'>alert('该商品未付款！')</script>");
            //                }
            //                else
            //                {
            //                    this.Response.Write("<script language='javascript' type='text/javascript'>alert('该商品已消费或者退单，请确认！')</script>");

            //                }
            //            }
            //            else
            //            {
            //                this.Response.Redirect("commodity_detail.aspx?cid=" + identifyingCode.IdentifyingCodeId + "&shopid=" + identifyingCode.ShopId + "&id=" + identifyingCode.OrderId);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        this.Response.Write("<script language='javascript' type='text/javascript'>alert('该订单不存在或未付款，请确认！')</script>");
            //    }
            //}
            //else
            //{
            //    this.Response.Write("<script language='javascript' type='text/javascript'>alert('该订单不存在或未付款，请确认！')</script>");
            //}            
        }

        protected void confirmnumber_Validating()
        {

        }
        #endregion
    }
}