using System;
using WeixinPF.Application.Agent;
using WeixinPF.Application.Agent.Service;
using WeixinPF.Application.System;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Hotel.Plugins.Service.Application;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Infrastructure;
using WeixinPF.Infrastructure.Agent;
using WeixinPF.Infrastructure.System;
using WeixinPF.Model.System;

namespace WeixinPF.Web
{
    public partial class login : System.Web.UI.Page
    {
        protected internal siteconfig siteConfig;
        public login()
        {
            siteConfig = new SiteConfig(new SiteConfigRepository()).loadConfig();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Session[MXKeys.SESSION_ADMIN_INFO] = null;
                Utils.WriteCookie("AdminName", "WeiXinPF", -14400);
                Utils.WriteCookie("AdminPwd", "WeiXinPF", -14400);

                Session["uweixinId"] = null;
                Utils.WriteCookie("uweixinId", "WeiXinPF", -14400);

                //餐饮——商铺ID
                Session[MXKeys.WEIXIN_DIANCAI_SHOPID] = null;
                Utils.WriteCookie(MXKeys.WEIXIN_DIANCAI_SHOPID, "WeiXinPF", -14400);

                msgtip.InnerHtml = "请输入用户名或密码";
                txtUserName.Value = Utils.GetCookie("DTRememberName");
                //                txtPassword.Value = Utils.GetCookie("DTRememberPwd");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Value.Trim();
            string userPwd = txtPassword.Value.Trim();

            if (userName.Equals("") || userPwd.Equals(""))
            {
                msgtip.InnerHtml = "请输入用户名或密码";
                return;
            }
            if (Session["AdminLoginSun"] == null)
            {
                Session["AdminLoginSun"] = 1;
            }
            else
            {
                Session["AdminLoginSun"] = Convert.ToInt32(Session["AdminLoginSun"]) + 1;
            }
            //判断登录错误次数
            if (Session["AdminLoginSun"] != null && Convert.ToInt32(Session["AdminLoginSun"]) > 5)
            {
                msgtip.InnerHtml = "错误超过5次，关闭浏览器重新登录！";
                return;
            }

            var managerService = new ManagerService(new ManagerRepository(this.siteConfig.sysdatabaseprefix));
            var model = managerService.GetModel(userName, userPwd, true);

            //BLL.manager bll = new BLL.manager();
            //Model.manager model = bll.GetModel(userName, userPwd, true);

            if (model == null)
            {
                msgtip.InnerHtml = "用户名或密码有误，请重试！";
                return;
            }
            Session[MXKeys.SESSION_ADMIN_INFO] = model;
            Session.Timeout = 45;
            //写入登录日志
            //Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
            if (this.siteConfig.logstatus > 0)
            {
                new ManagerLogService(new ManagerLogRepository(siteConfig.sysdatabaseprefix))
                    .Add(model.id, model.user_name, MXEnums.ActionEnum.Login.ToString(), "用户登录");                
            }
            //写入Cookies
            Utils.WriteCookie("DTRememberName", model.user_name, 14400);
            //            if (chkRemember.Checked)
            //            {
            //                Utils.WriteCookie("DTRememberPwd", model.password, 14400);
            //            }

            Utils.WriteCookie("AdminName", "WeiXinPF", model.user_name);
            Utils.WriteCookie("AdminPwd", "WeiXinPF", model.password);
            if (model.agentLevel > 0)
            {
                //说明为代理商
                Response.Redirect("index.aspx");
            }
            else
            {
                //餐饮||酒店管理员
                if (IsShopAdmin(model.id) || IsHotelAdmin(model.id))
                {
                    Response.Redirect("index.aspx");
                }
                Response.Redirect("wxIndex.aspx");
            }
        }
        private bool IsShopAdmin(int id)
        {
            //BLL.wx_diancai_admin dBll = new BLL.wx_diancai_admin();
            //Model.wx_diancai_admin shopAdmin = dBll.GetModel(id);

            ////餐饮 商铺管理员
            //if (shopAdmin != null)
            //{
            //    //Session[MXKeys.WEIXIN_DIANCAI_SHOPID] = shopAdmin.ShopId;
            //    //Utils.WriteCookie(MXKeys.WEIXIN_DIANCAI_SHOPID, "WeiXinPF", shopAdmin.ShopId.ToString());

            //    return true;
            //}

            //BLL.wx_diancai_shop_user suBll = new BLL.wx_diancai_shop_user();
            //Model.wx_diancai_shop_user shopUser = suBll.GetModel(id);

            //if (shopUser != null)
            //{
            //    //Session[MXKeys.WEIXIN_DIANCAI_SHOPID] = shopUser.ShopId;
            //    //Utils.WriteCookie(MXKeys.WEIXIN_DIANCAI_SHOPID, "WeiXinPF", shopUser.ShopId.ToString());

            //    return true;
            //}

            return false;
        }
        private bool IsHotelAdmin(int id)
        {
            using (var context = new HotelDbContext())
            {
                var hotelAdminService = new HotelAdminService(new HotelAdminRepository(context));
                var hotelAdminInfo = hotelAdminService.GetModel(id);

                if (hotelAdminInfo != null)
                {
                    return true;
                }

                var hotelUserService = new HotelUserService(new HotelUserRepository(context));
                var hotelUserInfo = hotelUserService.GetModel(id);

                if (hotelUserInfo != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}