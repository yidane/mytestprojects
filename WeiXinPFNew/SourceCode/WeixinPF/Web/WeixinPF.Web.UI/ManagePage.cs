using System;
using System.Web;
using WeixinPF.Application.Agent;
using WeixinPF.Application.Agent.Service;
using WeixinPF.Application.System;
using WeixinPF.Application.Weixin.Service;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Infrastructure.Agent;
using WeixinPF.Infrastructure.System;
using WeixinPF.Infrastructure.Weixin;
using WeixinPF.Model.Agent;
using WeixinPF.Model.System;
using WeixinPF.Model.Weixin;
using WeixinPF.Plugins.Hotel.Service.Application;
using WeixinPF.Plugins.Hotel.Service.Application.Service;
using WeixinPF.Plugins.Hotel.Service.Infrastructure;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Web.UI
{
    public class ManagePage : System.Web.UI.Page
    {
        protected internal siteconfig siteConfig;

        public ManagePage()
        {
            this.Load += new EventHandler(ManagePage_Load);
            siteConfig = new SiteConfig(new SiteConfigRepository()).loadConfig();
        }

        private void ManagePage_Load(object sender, EventArgs e)
        {
            //判断管理员是否登录
            //if (!IsAdminLogin())
            //{
            //    Response.Write("<script>parent.location.href='" + siteConfig.webpath + siteConfig.webmanagepath + "/login.aspx'</script>");
            //    Response.End();
            //}
        }

        #region 管理员============================================
        /// <summary>
        /// 判断管理员是否已经登录(解决Session超时问题)
        /// </summary>
        public bool IsAdminLogin()
        {
            //如果Session为Null
            if (Session[MXKeys.SESSION_ADMIN_INFO] != null)
            {
                return true;
            }
            else
            {
                //检查Cookies
                string adminname = Utils.GetCookie("AdminName", "WeiXinPF");
                string adminpwd = Utils.GetCookie("AdminPwd", "WeiXinPF");
                if (adminname != "" && adminpwd != "")
                {
                    var service = new ManagerService(new ManagerRepository(siteConfig.sysdatabaseprefix));
                    var model = service.GetModel(adminname, adminpwd);
                    if (model != null)
                    {
                        Session[MXKeys.SESSION_ADMIN_INFO] = model;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得管理员信息
        /// </summary>
        public ManagerInfo GetAdminInfo()
        {
            if (IsAdminLogin())
            {
                var model = Session[MXKeys.SESSION_ADMIN_INFO] as ManagerInfo;
                if (model != null)
                {
                    return model;
                }
            }
            return null;
        }



        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="nav_name">菜单名称</param>
        /// <param name="action_type">操作类型</param>
        public void ChkAdminLevel(string nav_name, string action_type)
        {
            var model = GetAdminInfo();
            var service = new ManagerRoleService(new ManagerRoleRepository(siteConfig.sysdatabaseprefix));
            bool result = service.Exists(model.role_id, nav_name, action_type);

            if (!result)
            {
                string msgbox = "parent.jsdialog(\"错误提示\", \"您没有管理该页面的权限，请勿非法进入！\", \"back\", \"Error\")";
                Response.Write("<script type=\"text/javascript\">" + msgbox + "</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 写入管理日志
        /// </summary>
        /// <param name="action_type"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool AddAdminLog(string action_type, string remark)
        {
            if (siteConfig.logstatus > 0)
            {
                var model = GetAdminInfo();
                int newId = new ManagerLogService(new ManagerLogRepository(siteConfig.sysdatabaseprefix)).Add(model.id, model.user_name, action_type, remark);
                if (newId > 0)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region JS提示============================================
        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        /// <summary>
        /// 带回传函数的添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        /// <param name="callback">JS回调函数</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss, string callback)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", " + callback + ")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }

        /// <summary>
        /// 带确认按钮的提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        /// <param name="callback">JS回调函数</param>
        protected void JscriptConfirmMsg(string msgtitle, string url, string msgcss, string callback)
        {
            string msbox = "parent.jsconfirm(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", " + callback + ")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsConfirm", msbox, true);
        }


        #endregion
        public bool IsWeiXinCode()
        {

            //如果Session为Null
            if (Session["nowweixin"] != null)
            {
                return true;
            }
            else
            {
                //检查Cookies
                string uweixinId = Utils.GetCookie("nowweixinId", "WeiXinPF");
                if (uweixinId != "")
                {
                    var service = new WXUserService(new WXUserRepository());
                    var model = service.GetModel(int.Parse(uweixinId));
                    if (model != null)
                    {
                        Session["nowweixin"] = model;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得当前微信帐号信息
        /// </summary>
        public WX_UserWeixinInfo GetWeiXinCode()
        {
            if (IsWeiXinCode())
            {
                var model = Session["nowweixin"] as WX_UserWeixinInfo;
                if (model != null)
                {
                    return model;
                }
            }
            else
            {
                //int shopid = GetShopId();
                //if (shopid != 0)
                //{
                //    BLL.wx_diancai_shopinfo shopBll = new BLL.wx_diancai_shopinfo();
                //    Model.wx_diancai_shopinfo shop = shopBll.GetModel(shopid);

                //    return new BLL.wx_userweixin().GetModel(shop.wid.Value);
                //}

                int hotelid = GetHotelId();
                if (hotelid != 0)
                {
                    HotelInfo hotel = null;

                    hotel = new HotelService(new HotelRepository()).GetModel(hotelid);

                    if (hotel !=null)
                    {
                        return new WXUserService(new WXUserRepository()).GetModel(hotel.wid.Value);
                    }
                    
                }
                Response.Write("<script>parent.location.href='http://" + HttpContext.Current.Request.Url.Authority + "/admin/weixin/myweixinlist.aspx'</script>");
                Response.End();
            }
            return null;
        }

        //public int GetShopId()
        //{
        //    if (IsAdminLogin())
        //    {
        //        var admin = GetAdminInfo();
        //        BLL.wx_diancai_admin shopAdminBll = new BLL.wx_diancai_admin();
        //        Model.wx_diancai_admin shopAdmin = shopAdminBll.GetModel(admin.id);
        //        if (shopAdmin != null)
        //        {
        //            return shopAdmin.ShopId;
        //        }

        //        BLL.wx_diancai_shop_user suBll = new BLL.wx_diancai_shop_user();
        //        Model.wx_diancai_shop_user shopUser = suBll.GetModel(admin.id);

        //        if (shopUser != null)
        //        {
        //            return shopUser.ShopId;
        //        }
        //        return 0;
        //    }
        //    return 0;
        //}

        public int GetHotelId()
        {
            if (IsAdminLogin())
            {
                var admin = GetAdminInfo();

                using (var dbContext = new HotelDbContext())
                {
                    var adminService = new HotelAdminService(new HotelAdminRepository(dbContext));

                    var hotelAdmin = adminService.GetModel(admin.id);
                    if (hotelAdmin != null)
                    {
                        return hotelAdmin.HotelId;
                    }

                    var userService = new HotelUserService(new HotelUserRepository(dbContext));
                    
                    var hotelUser = userService.GetModel(admin.id);

                    if (hotelUser != null)
                    {
                        return hotelUser.HotelId;
                    }
                }
              
                return 0;
            }
            return 0;
        }
    }
}
