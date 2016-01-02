using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WeixinPF.Application.Agent;
using WeixinPF.Application.Common.Service;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Infrastructure.Agent;
using WeixinPF.Infrastructure.Common;
using WeixinPF.Model.Common;
using WeixinPF.Web.UI;

namespace WeixinPF.Web.Functoin.BackPage.Admin.manager
{
    public partial class editormyinfo : ManagePage
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                BindDdlProvince(ddlProvince);
                BindDdlCity(ddlCity);

                var model = GetAdminInfo();
                ShowInfo(model.id);
            }
        }



        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            lblid.Text = _id.ToString();

            var managerService = new ManagerService(new ManagerRepository(siteConfig.sysdatabaseprefix));
            var model = managerService.GetModel(_id);

            lblUserName.Text = model.user_name;


            txtRealName.Text = model.real_name;
            txtTelephone.Text = model.telephone;
            txtEmail.Text = model.email;
            txtqq.Text = model.qq;
            ddlProvince.SelectedValue = model.province;
            ddlCity.SelectedValue = model.city;
            txtArea.Text = model.county;
           

        }
        #endregion
        #region 修改操作=================================
        private bool DoEdit()
        {
            int _id = MyCommFun.Str2Int(lblid.Text);
            //地区
            string prov = ddlProvince.SelectedItem.Value;
            string city = ddlCity.SelectedItem.Value;
            string dist = txtArea.Text.Trim();


            bool result = false;
            var managerService = new ManagerService(new ManagerRepository(siteConfig.sysdatabaseprefix));
            var model = managerService.GetModel(_id);


            model.real_name = txtRealName.Text.Trim();
            model.telephone = txtTelephone.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.qq = txtqq.Text;
            model.email = txtEmail.Text;

            model.province = prov;
            model.city = city;
            model.county = dist;


            if (managerService.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改个人资料:" + model.user_name); //记录日志
                result = true;
            }

            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!DoEdit())
            {
                JscriptMsg("保存过程中发生错误！", "", "Error");
                return;
            }
            JscriptMsg("修改个人资料成功！", "editormyinfo.aspx", "Success");

        }


        #region 地区绑定
        /// <summary>
        /// 绑定省份
        /// </summary>
        /// <param name="ddl"></param>
        public void BindDdlProvince(DropDownList ddl)
        {
            var districtService = new DistrictService(new DistrictRepository());
            IList<DistrictInfo> disList = districtService.GetModelList("level=1");
            if (disList != null)
            {
                ddl.DataTextField = "name";
                ddl.DataValueField = "id";
                ddl.DataSource = disList;
                ddl.DataBind();
            }

        }

        /// <summary>
        /// 绑定省份
        /// </summary>
        /// <param name="ddl"></param>
        public void BindDdlCity(DropDownList ddl)
        {
            var districtService = new DistrictService(new DistrictRepository());
            IList<DistrictInfo> disList = districtService.GetModelList("level=2");
            if (disList != null)
            {
                ddl.DataTextField = "name";
                ddl.DataValueField = "id";
                ddl.DataSource = disList;
                ddl.DataBind();
            }

        }

        /// <summary>
        /// 省级选择完以后，联动带出城市
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            var provId = ddlProvince.SelectedItem.Value;
            var districtService = new DistrictService(new DistrictRepository());

            IList<DistrictInfo> disList = districtService.GetModelList("level=2 and upid=" + provId);
            if (disList != null)
            {
                ddlCity.DataTextField = "name";
                ddlCity.DataValueField = "id";
                ddlCity.DataSource = disList;
                ddlCity.DataBind();
            }
            MessageBox.ResponseScript(this, "$(\"#txtUserName\").focus();");

        }

        #endregion
    }
}