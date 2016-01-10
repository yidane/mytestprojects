using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using WeixinPF.Application.Agent.Service;
using WeixinPF.Application.System.Service;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Infrastructure.Agent;
using WeixinPF.Infrastructure.System;
using WeixinPF.Infrastructure.Weixin;
using WeixinPF.Model.Agent;
using WeixinPF.Web.UI;

namespace WeixinPF.Web.Functoin.BackPage.Admin.manager
{
    public partial class role_edit : ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;
        private ManagerRoleService rBll;
        protected void Page_Load(object sender, EventArgs e)
        {
            rBll = new ManagerRoleService();
            string _action = MXRequest.GetQueryString("action");
            this.id = MXRequest.GetQueryInt("id");

            if (!string.IsNullOrEmpty(_action) && _action == MXEnums.ActionEnum.Edit.ToString())
            {
                this.action = MXEnums.ActionEnum.Edit.ToString();//修改类型
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                var adminEntity = GetAdminInfo();
                if (!rBll.Exists(this.id,adminEntity.Id))
                {
                    JscriptMsg("角色不存在或已被删除！", "back", "Error");
                    return;
                }

            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("manager_role", MXEnums.ActionEnum.View.ToString()); //检查权限
               // RoleTypeBind(); //绑定角色类型
                NavBind(); //绑定导航
                if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 角色类型=================================
        private void RoleTypeBind()
        {
            //Model.manager model = GetAdminInfo();
            //ddlRoleType.Items.Clear();
            //ddlRoleType.Items.Add(new ListItem("请选择类型...", ""));
            //if (model.role_type < 2)
            //{
            //    ddlRoleType.Items.Add(new ListItem("超级用户", "1"));
            //}
            //ddlRoleType.Items.Add(new ListItem("系统用户", "2"));
        }
        #endregion

        #region 导航菜单=================================
        private void NavBind()
        {
            var adminEntity = GetAdminInfo();
            var bll = new NavigationService(new NavigationRepository(siteConfig.sysdatabaseprefix));
            DataTable dt = new DataTable();
            bool isAgent = false;
            if (adminEntity.AgentLevel > 0)
            {
                isAgent = true;
            }
            dt = bll.GetList(0, MXEnums.NavigationEnum.System.ToString(), isAgent, new SystemConfigRepository());
            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            var bll = new ManagerRoleService();
            var model = bll.GetModel(_id);
            txtRoleName.Text = model.RoleName;
            //ddlRoleType.SelectedValue = model.role_type.ToString();
            //管理权限
            if (model.ManagerRoleValues != null)
            {
                for (int i = 0; i < rptList.Items.Count; i++)
                {
                    string navName = ((HiddenField)rptList.Items[i].FindControl("hidName")).Value;
                    CheckBoxList cblActionType = (CheckBoxList)rptList.Items[i].FindControl("cblActionType");
                    for (int n = 0; n < cblActionType.Items.Count; n++)
                    {
                        var modelt = model.ManagerRoleValues.Where(p => p.NavName == navName && p.ActionType == cblActionType.Items[n].Value);
                        if (modelt != null)
                        {
                            cblActionType.Items[n].Selected = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            var adminEntity=GetAdminInfo();
            bool result = false;
            var model = new ManagerRoleInfo();
            model.AgentId = adminEntity.Id;
            model.RoleName = txtRoleName.Text.Trim();
            //model.role_type = int.Parse(ddlRoleType.SelectedValue);
            model.RoleType = 2;

            //管理权限
            var ls = new List<ManagerRoleValueInfo>();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string navName = ((HiddenField)rptList.Items[i].FindControl("hidName")).Value;
                CheckBoxList cblActionType = (CheckBoxList)rptList.Items[i].FindControl("cblActionType");
                for (int n = 0; n < cblActionType.Items.Count; n++)
                {
                    if (cblActionType.Items[n].Selected == true)
                    {
                        ls.Add(new ManagerRoleValueInfo { NavName = navName, ActionType = cblActionType.Items[n].Value });
                    }
                }
            }
            model.ManagerRoleValues = ls;

            if (rBll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加角色:" + model.RoleName); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            var bll = new ManagerRoleService();
            var model = bll.GetModel(_id);

            model.RoleName = txtRoleName.Text.Trim();
            //  model.role_type = int.Parse(ddlRoleType.SelectedValue);

            //管理权限
            var ls = new List<ManagerRoleValueInfo>();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string navName = ((HiddenField)rptList.Items[i].FindControl("hidName")).Value;
                CheckBoxList cblActionType = (CheckBoxList)rptList.Items[i].FindControl("cblActionType");
                for (int n = 0; n < cblActionType.Items.Count; n++)
                {
                    if (cblActionType.Items[n].Selected == true)
                    {
                        ls.Add(new ManagerRoleValueInfo { RoleId = _id, NavName = navName, ActionType = cblActionType.Items[n].Value });
                    }
                }
            }
            model.ManagerRoleValues = ls;

            if (bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改角色:" + model.RoleName); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        //美化列表
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                //美化导航树结构
                Literal LitFirst = (Literal)e.Item.FindControl("LitFirst");
                HiddenField hidLayer = (HiddenField)e.Item.FindControl("hidLayer");
                string LitStyle = "<span style=\"display:inline-block;width:{0}px;\"></span>{1}{2}";
                string LitImg1 = "<span class=\"folder-open\"></span>";
                string LitImg2 = "<span class=\"folder-line\"></span>";

                int classLayer = Convert.ToInt32(hidLayer.Value);
                if (classLayer == 1)
                {
                    LitFirst.Text = LitImg1;
                }
                else
                {
                    LitFirst.Text = string.Format(LitStyle, (classLayer - 2) * 24, LitImg2, LitImg1);
                }
                //绑定导航权限资源
                string[] actionTypeArr = ((HiddenField)e.Item.FindControl("hidActionType")).Value.Split(',');
                CheckBoxList cblActionType = (CheckBoxList)e.Item.FindControl("cblActionType");
                cblActionType.Items.Clear();
                for (int i = 0; i < actionTypeArr.Length; i++)
                {
                    if (Utils.ActionType().ContainsKey(actionTypeArr[i]))
                    {
                        cblActionType.Items.Add(new ListItem(" " + Utils.ActionType()[actionTypeArr[i]] + " ", actionTypeArr[i]));
                    }
                }
            }
        }

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("manager_role", MXEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("修改角色成功！", "role_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("manager_role", MXEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("添加角色成功！", "role_list.aspx", "Success");
            }
        }
    }
}