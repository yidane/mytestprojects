using System;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Model.Agent;
using WeixinPF.Web.UI;

namespace WeixinPF.Web
{
    public partial class wxIndex : ManagePage
    {
        protected ManagerInfo admin_info;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                admin_info = GetAdminInfo();
            }
        }



        //安全退出
        protected void lbtnExit_Click(object sender, EventArgs e)
        {
            Session[MXKeys.SESSION_ADMIN_INFO] = null;
            Utils.WriteCookie("AdminName", "WeiXinPF", -14400);
            Utils.WriteCookie("AdminPwd", "WeiXinPF", -14400);
            Response.Redirect("login.aspx");
        }
    }
}