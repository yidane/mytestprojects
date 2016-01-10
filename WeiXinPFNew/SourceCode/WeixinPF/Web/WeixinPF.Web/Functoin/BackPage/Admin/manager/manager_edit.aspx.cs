using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using WeixinPF.Application.Agent;
using WeixinPF.Application.Agent.Service;
using WeixinPF.Application.Common.Service;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Infrastructure.Agent;
using WeixinPF.Infrastructure.Common;
using WeixinPF.Model.Agent;
using WeixinPF.Model.Common;
using WeixinPF.Web.UI;

namespace WeixinPF.Web.Functoin.BackPage.Admin.manager
{
    /// <summary>
    /// 编辑用户
    /// </summary>
    public partial class manager_edit : ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;
        WXAgentService aBll = new WXAgentService(new WXAgentRepository());
        private bool isAgent = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = MXRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == MXEnums.ActionEnum.Edit.ToString())
            {
                this.action = MXEnums.ActionEnum.Edit.ToString();//修改类型
                if (!int.TryParse(Request.QueryString["id"] as string, out this.id))
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new ManagerInfoService().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back", "Error");
                    return;
                }
               
            }


            if (!Page.IsPostBack)
            {
                
                BindDdlProvince(ddlProvince);
                BindDdlCity(ddlCity);

                ChkAdminLevel("manager_list", MXEnums.ActionEnum.View.ToString()); //检查权限
                var model = GetAdminInfo(); //取得管理员信息
                var agent = aBll.GetAgentModel(model.Id);
                //代理商信息
                if (agent != null)
                {
                    lblremainMony.Text = agent.remainMony.Value.ToString();
                    lblagentPrice.Text = agent.agentPrice.Value.ToString();
                    lblMoney.Text = agent.agentPrice.Value.ToString();
                    isAgent = true;
                }

                RoleBind(ddlRoleId, model.Id);
                if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 角色类型=================================
        private void RoleBind(DropDownList ddl, int managerId)
        {
            var bll = new ManagerRoleService();
         //   DataTable dt = bll.GetList("agentId=" + managerId).Tables[0];
            DataTable dt = new DataTable();
            if (isAgent)
            {
                dt = bll.GetList("id!=1 and id!=3 ").Tables[0];
            }
            else
            {
                dt = bll.GetList("agentId=" + managerId).Tables[0];
            }

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("请选择角色...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                
            ddl.Items.Add(new ListItem(dr["role_name"].ToString(), dr["id"].ToString()));
                 
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            litpwdtip.Text = "不填则不修改密码";
            var bll = new ManagerInfoService();
         
            var model = bll.GetModel(_id);

            ddlRoleId.SelectedValue = model.RoleId.ToString();
            if (model.IsLock == 0)
            {
                cbIsLock.Checked = true;
            }
            else
            {
                cbIsLock.Checked = false;
            }
            txtUserName.Text = model.UserName;
            txtUserName.ReadOnly = true;
            txtUserName.Attributes.Remove("ajaxurl");
           
            txtRealName.Text = model.RealName;
            txtTelephone.Text = model.Telephone;
            txtEmail.Text = model.Email;
            ddlMaxNum.SelectedValue = model.WxNum.ToString();
            hidOldMaxNum.Value = model.WxNum.ToString();
            ddlProvince.SelectedValue = model.Province;
            ddlCity.SelectedValue = model.City;
            txtArea.Text = model.County;
            txtqq.Text = model.QQ;
            txtEmail.Text = model.Email;
            txtSortid.Text = MyCommFun.ObjToStr(model.SortId);  // model.sort_id;
            txtRemark.Text = model.Remark;
           

          }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
         
            var adminEntity = GetAdminInfo(); //取得管理员信息
            var agent = new AgentInfo();
            bool isAgent = false;
            if (adminEntity.AgentLevel < 0)
            {
                return false;
            }
            if (adminEntity.AgentLevel > 0)
            {
                agent = aBll.GetAgentModel(adminEntity.Id);
                isAgent = true;
                if (agent.remainMony < agent.agentPrice)
                {
                    JscriptMsg("余额不足，请联系管理员充值！", "", "Error");
                    return false;
                }
            }
            else
            { 
                
            }
            //int oldMaxNum = MyCommFun.Str2Int(hidOldMaxNum.Value);
            int newMaxNum = MyCommFun.Str2Int(ddlMaxNum.SelectedItem.Value);
            
            //地区
            string prov = ddlProvince.SelectedItem.Value;
            string city = ddlCity.SelectedItem.Value;
            string dist = txtArea.Text.Trim();

            var model = new ManagerInfo();
            var bll = new ManagerInfoService();
            model.RoleId = int.Parse(ddlRoleId.SelectedValue);
            model.RoleType = new ManagerRoleService().GetModel(model.RoleId).RoleType;
            if (cbIsLock.Checked == true)
            {
                model.IsLock = 0;
            }
            else
            {
                model.IsLock = 1;
            }
            //检测用户名是否重复
            if (bll.Exists(txtUserName.Text.Trim()))
            {
                return false;
            }
            model.UserName = txtUserName.Text.Trim();
            //获得6位的salt加密字符串
            model.Salt = Utils.GetCheckCode(6);
            //以随机生成的6位字符串做为密钥加密
            model.Password = DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.Salt);
            model.RealName = txtRealName.Text.Trim();
            model.Telephone = txtTelephone.Text.Trim();
            model.Email = txtEmail.Text.Trim();
            model.AddTime = DateTime.Now;
            model.WxNum = newMaxNum;
            model.AgentId = GetAdminInfo().Id;
            model.QQ = txtqq.Text;
            model.Email = txtEmail.Text;
            model.RegIp = MXRequest.GetIP();
            model.Province = prov;
            model.City = city;
            model.County = dist;
            model.SortId=MyCommFun.Obj2Int(txtSortid.Text);
            model.AgentLevel = -1;
            model.Remark = txtRemark.Text;
            model.AgentId = adminEntity.Id;
            int addId = bll.Add(model);
         
            if (addId>0 && isAgent)
            {
                int xfjine = newMaxNum * agent.agentPrice.Value;//消费金额

                //是代理商 :缴费，扣除金额，增加消费记录
                agent.remainMony -= xfjine;
                agent.userNum += 1;
                agent.wcodeNum += newMaxNum;
                bool updateRet= aBll.Update(agent);

                if (updateRet)
                {
                    var bBll = new WXManagerBillService(new WXManagerBillRepository());
                    var bill = new ManagerBillInfo
                    {
                        BillMoney = xfjine,
                        ManagerId = agent.managerId,
                        OperPersonId = agent.managerId,
                        OperDate = DateTime.Now,
                        BillUsed = "开通1个用户" + model.UserName + "的" + newMaxNum + "个微帐号",
                        MoneyType = "扣减"
                    };
                    int addBillId= bBll.Add(bill);
                   
                }
                else
                {
                    bll.Delete(addId);
                    addId = 0;
                }
            }
             
            if (addId> 0)
            {

                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加用户:" + model.UserName); //记录日志
                return true;
            }
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            int oldMaxNum = MyCommFun.Str2Int(hidOldMaxNum.Value);
            int newMaxNum = MyCommFun.Str2Int(ddlMaxNum.SelectedItem.Value);
            int addNewNum = newMaxNum - oldMaxNum; //新增的帐号
         

            var adminEntity = GetAdminInfo(); //取得管理员信息
            var agent = new AgentInfo();
            bool isAgent = false;
            if (adminEntity.AgentLevel < 0)
            {
                return false;
            }
            if (adminEntity.AgentLevel > 0)
            {
                agent = aBll.GetAgentModel(adminEntity.Id);
                isAgent = true;
                if (agent.remainMony < agent.agentPrice * addNewNum)
                {
                    JscriptMsg("余额不足，请联系管理员充值！", "", "Error");
                    return false;
                }
            }
            

            //地区
            string prov = ddlProvince.SelectedItem.Value;
            string city = ddlCity.SelectedItem.Value;
            string dist = txtArea.Text.Trim();


            bool result = false;
            var bll = new ManagerInfoService();
            var model = bll.GetModel(_id);

            model.RoleId = int.Parse(ddlRoleId.SelectedValue);
            model.RoleType = new ManagerRoleService().GetModel(model.RoleId).RoleType;
            if (cbIsLock.Checked == true)
            {
                model.IsLock = 0;
            }
            else
            {
                model.IsLock = 1;
            }
            //判断密码是否更改
            if (txtPassword.Text.Trim() != "")
            {
                //获取用户已生成的salt作为密钥加密
                model.Password = DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.Salt);
            }
            model.RealName = txtRealName.Text.Trim();
            model.Telephone = txtTelephone.Text.Trim();
            model.Email = txtEmail.Text.Trim();
            model.WxNum = int.Parse(ddlMaxNum.SelectedItem.Value);

            model.QQ = txtqq.Text;
            model.Email = txtEmail.Text;
          
            model.Province = prov;
            model.City = city;
            model.County = dist;
            model.SortId = MyCommFun.Str2Int(txtSortid.Text);
            model.Remark = txtRemark.Text;
          
            bool updateRet = bll.Update(model);

            if (updateRet && isAgent && addNewNum > 0)
            {
                int xfjine = addNewNum * agent.agentPrice.Value;//消费金额

                agent.remainMony -= xfjine;
                agent.wcodeNum += newMaxNum;

                bool updateRet2 = aBll.Update(agent);
                if (updateRet2)
                {
                    var bBll = new WXManagerBillService(new WXManagerBillRepository());
                    var bill = new ManagerBillInfo
                    {
                        BillMoney = xfjine,
                        ManagerId = agent.managerId,
                        OperPersonId = agent.managerId,
                        OperDate = DateTime.Now,
                        BillUsed = "原用户" + model.UserName + "新增了" + addNewNum + "个微帐号",
                        MoneyType = "扣减"
                    };
                    int addBillId = bBll.Add(bill);

                }
                else
                {
                    bll.Delete(_id);
                    updateRet=false;
                }
 
            }

            if (updateRet)
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改用户:" + model.UserName); //记录日志
                result = true;
            }

            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int oldMaxNum =MyCommFun.Str2Int( hidOldMaxNum.Value);
            int newMaxNum = MyCommFun.Str2Int(ddlMaxNum.SelectedItem.Value);

            if (newMaxNum < oldMaxNum)
            {
                JscriptMsg("微信帐号数量不能小于原来的微信帐号数量！", "", "Error");
                return;
            }
            if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("manager_list", MXEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("修改用户信息成功！", "manager_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("manager_list", MXEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("添加用户信息成功！", "manager_list.aspx", "Success");
            }
        }


        #region 地区绑定
        /// <summary>
        /// 绑定省份
        /// </summary>
        /// <param name="ddl"></param>
        public void BindDdlProvince(DropDownList ddl)
        {
            var disBll = new DistrictService(new DistrictRepository());
            IList<DistrictInfo> disList = disBll.GetModelList("level=1");
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
            var disBll = new DistrictService(new DistrictRepository());
            IList<DistrictInfo> disList = disBll.GetModelList("level=2");
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
            string provId = ddlProvince.SelectedItem.Value;
            var disBll = new DistrictService(new DistrictRepository());

            var disList = disBll.GetModelList("level=2 and upid=" + provId);
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