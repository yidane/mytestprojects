using System;
using WeixinPF.Application.Agent.Service;
using WeixinPF.Common.Enum;
using WeixinPF.Infrastructure.Agent;
using WeixinPF.Plugins.Hotel.Service.Application;
using WeixinPF.Plugins.Hotel.Service.Application.Service;
using WeixinPF.Plugins.Hotel.Service.Infrastructure;
using WeixinPF.Web.UI;

namespace WeixinPF.Web
{
    public partial class center : ManagePage
    {
        public string _userType = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
//            if (!Page.IsPostBack)
//            {
                var admin_info = GetAdminInfo(); //管理员信息
                //登录信息
                if (admin_info != null)
                {
                    var managerLogService = new ManagerLogService(new ManagerLogRepository(siteConfig.sysdatabaseprefix));
                    //BLL.manager_log bll = new BLL.manager_log();
                    var log = managerLogService.GetModel(admin_info.user_name, 1, MXEnums.ActionEnum.Login.ToString());
                    if (log != null)
                    {
                        //本次登录
                        litIP.Text = log.user_ip;
                    }
                    var log2 = managerLogService.GetModel(admin_info.user_name, 2, MXEnums.ActionEnum.Login.ToString());
                    if (log2 != null)
                    {
                        //上一次登录
                        litBackIP.Text = log2.user_ip;
                        litBackTime.Text = log2.add_time.ToString();
                    }

                    if (IsWeiXinCode())
                    {
                        _userType = "ScenicAdmin";//景区管理员
                    }
                    //else if (IsShopAdmin(admin_info.id))
                    //{
                    //    _userType = "ShopAdmin";//餐饮管理员
                    //}
                    else if (IsHotelAdmin(admin_info.id))
                    {
                        _userType = "HotelAdmin";//酒店管理员
                    }
                }

                //LitUpgrade.Text = Utils.GetDomainStr(MXKeys.CACHE_OFFICIAL_UPGRADE, DESEncrypt.Decrypt(MXKeys.FILE_URL_UPGRADE_CODE));
                //LitNotice.Text = Utils.GetDomainStr(MXKeys.CACHE_OFFICIAL_NOTICE, DESEncrypt.Decrypt(MXKeys.FILE_URL_NOTICE_CODE));
                //Utils.GetDomainStr("dt_cache_domain_info", "http://www.WeiXinPF.net/upgrade.ashx?u=" + Request.Url.DnsSafeHost + "&i=" + Request.ServerVariables["LOCAL_ADDR"]);

              
//            }
        }

        //private bool IsShopAdmin(int id)
        //{
        //    BLL.wx_diancai_admin dBll = new BLL.wx_diancai_admin();
        //    Model.wx_diancai_admin shopAdmin = dBll.GetModel(id);

        //    //餐饮 商铺管理员
        //    if (shopAdmin != null)
        //    {
        //        //Session[MXKeys.WEIXIN_DIANCAI_SHOPID] = shopAdmin.ShopId;
        //        //Utils.WriteCookie(MXKeys.WEIXIN_DIANCAI_SHOPID, "WeiXinPF", shopAdmin.ShopId.ToString());

        //        return true;
        //    }

        //    BLL.wx_diancai_shop_user suBll = new BLL.wx_diancai_shop_user();
        //    Model.wx_diancai_shop_user shopUser = suBll.GetModel(id);

        //    if (shopUser != null)
        //    {
        //        //Session[MXKeys.WEIXIN_DIANCAI_SHOPID] = shopUser.ShopId;
        //        //Utils.WriteCookie(MXKeys.WEIXIN_DIANCAI_SHOPID, "WeiXinPF", shopUser.ShopId.ToString());

        //        return true;
        //    }

        //    return false;
        //}

        private bool IsHotelAdmin(int id)
        {
            using (var context = new HotelDbContext()) //todo: CodeReview的时候注意context放在这是否合适？
            {
                var hotelAdminService = new HotelAdminService(new HotelAdminRepository(context));
                var adminInfo = hotelAdminService.GetModel(id);

                //酒店管理员
                if (adminInfo != null)
                {
                    return true;
                }

                var hotelUserService = new HotelUserService(new HotelUserRepository(context));
                var hotelUserInfo = hotelUserService.GetModel(id);

                if (hotelUserInfo != null)
                {
                    return true;
                }

                return false;
            }            
        }
    }
}