using System;
using WeixinPF.Application.Agent;
using WeixinPF.Application.Agent.Service;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Infrastructure.Agent;
using WeixinPF.Web.UI;

namespace WeixinPF.Web.Functoin.BackPage.Admin.manager
{
    public partial class manager_pwd : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var model = GetAdminInfo();
                ShowInfo(model.Id);
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            var bll = new ManagerInfoService();
            var model = bll.GetModel(_id);
           lblUserName.Text = model.UserName;
            
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var bll = new ManagerInfoService();
            var model = GetAdminInfo();

            if (DESEncrypt.Encrypt(txtOldPassword.Text.Trim(), model.Salt) != model.Password)
            {
                JscriptMsg("旧密码不正确！", "", "Warning");
                return;
            }
            if (txtPassword.Text.Trim() != txtPassword1.Text.Trim())
            {
                JscriptMsg("两次密码不一致！", "", "Warning");
                return;
            }
            model.Password = DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.Salt);
            if (!bll.Update(model))
            {
                JscriptMsg("保存过程中发生错误！", "", "Error");
                return;
            }
            Session[SystemKeys.SESSION_ADMIN_INFO] = null;
            JscriptMsg("密码修改成功！", "manager_pwd.aspx", "Success");
        }
    }
}