using System;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Model.Agent;
using WeixinPF.Web.UI;

namespace WeixinPF.Web
{
    public partial class index : ManagePage
    {
        public ManagerInfo admin_info;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                admin_info = GetAdminInfo();
                if (admin_info.AgentLevel > 0)
                {
                    //说明为代理商
                    mygzh.Style.Add("display", "none");
                  //  Response.Redirect("index.aspx");
                }
                else
                {
                    indexUrl.HRef = "wxIndex.aspx";
                  //  Response.Redirect("wxIndex.aspx");
                }
            }
        }

        //安全退出
        protected void lbtnExit_Click(object sender, EventArgs e)
        {
            Session[SystemKeys.SESSION_ADMIN_INFO] = null;
            Utils.WriteCookie("AdminName", "WeiXinPF", -14400);
            Utils.WriteCookie("AdminPwd", "WeiXinPF", -14400);

            Session["uweixinId"] = null;
            Utils.WriteCookie("uweixinId", "WeiXinPF", -14400);

            Session[SystemKeys.WEIXIN_DIANCAI_SHOPID] = null;
            Utils.WriteCookie(SystemKeys.WEIXIN_DIANCAI_SHOPID, "WeiXinPF", -14400);

            Response.Redirect("login.aspx");
        }

    }
}