using System;
using WeixinPF.Application.Agent.Service;
using WeixinPF.Application.Weixin.Service;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Infrastructure.Agent;
using WeixinPF.Infrastructure.Weixin;
using WeixinPF.Model.Agent;
using WeixinPF.Web.UI;

namespace WeixinPF.Web.Functoin.BackPage.Admin.manager
{
    public partial class wxcode_edit : ManagePage
    {
        private WXUserService bll;
        private WXAgentService aBll;
        private ManagerInfo adminEntity;
        private WX_AgentInfo agent;
        protected string returnPage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            bll = new WXUserService(new WXUserRepository());
            aBll = new WXAgentService(new WXAgentRepository());
            adminEntity = GetAdminInfo(); //取得管理员信息
             agent = aBll.GetAgentModel(adminEntity.id);
            if (!Page.IsPostBack)
            {
                 int id = 0;

                if (!int.TryParse(Request.QueryString["id"] as string, out id))
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!bll.Exists(id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back", "Error");
                    return;
                }
                returnPage = "wxcodemgr.aspx";
                //如果是从微用户管理里来的，还得判断下
                if (MyCommFun.QueryString("fpage").Trim().Length > 0 && MyCommFun.RequestInt("uid") > 0)
                {
                    returnPage = "weixin_list.aspx?id=" + MyCommFun.RequestInt("uid");
                }
                ShowInfo(id);
            }
        }



        #region 赋值操作=================================
        private void ShowInfo(int id)
        {
            var model = bll.GetModel(id);
            lblId.Text = model.id.ToString();
            this.txtwxName.Text = model.wxName;
            this.txtwxId.Text = model.wxId;
            this.txtweixinCode.Text = model.weixinCode;
            this.txtImgUrl.Text = model.headerpic;
            this.txtapiurl.Text =MyCommFun.getWebSite()+"/api/weixin/api.aspx?apiid="+ model.id;
            this.txtwxToken.Text = model.wxToken;
            if(model.wStatus!=null && model.wStatus==0)
            {
                this.rblwStatus.SelectedValue = "0";
            }
            

            this.txtAppId.Text = model.AppId;
            this.txtAppSecret.Text = model.AppSecret;
           // txtEndTime.Text = model.endDate.Value.ToString("yyyy-MM-dd");
            lblEndDate.Text = model.endDate.Value.ToString("yyyy-MM-dd");
            lblAddDate.Text = model.createDate.Value.ToString("yyyy-MM-dd");
            lblEndDate.Font.Bold = true;
            if (model.endDate < DateTime.Now)
            {
                //过期
                lblEndDate.ForeColor = System.Drawing.Color.Red;
                lblEndDate.Text += "[已过期]";

            }
            else if (model.endDate <= DateTime.Now.AddDays(20))
            {
                //快到期
                TimeSpan ts = model.endDate.Value - DateTime.Now;
                int sub = ts.Days;  
                lblEndDate.ForeColor = System.Drawing.Color.Red;
                lblEndDate.Text += " [还有" + sub + "天到期]";
              
            }
            else
            { 
                
            }

            //代理商信息
            if (agent != null)
            {
                lblremainMony.Text = agent.remainMony.Value.ToString();
                lblagentPrice.Text = agent.agentPrice.Value.ToString();
            }

        }

        #endregion



        #region 修改操作=================================
        private bool DoEdit()
        {
            int _id =MyCommFun.Str2Int(lblId.Text);
            string strErr = "";
            if (this.txtwxName.Text.Trim().Length == 0)
            {
                strErr += "公众帐号名称不能为空！";
            }
            if (this.txtwxId.Text.Trim().Length == 0)
            {
                strErr += "公众号原始id不能为空！";
            }

            if (this.txtweixinCode.Text.Trim().Length == 0)
            {
                strErr += "微信号不能为空！";
            }
            if (this.txtwxToken.Text.Trim().Length == 0)
            {
                strErr += "TOKEN值不能为空！";
            }

            if (strErr != "")
            {
                JscriptMsg(strErr, "back", "Error");
                return false;
            }

            string wxName = this.txtwxName.Text;
            string wxId = this.txtwxId.Text;
            string weixinCode = this.txtweixinCode.Text;
            string headerpic = this.txtImgUrl.Text;
            string apiurl = this.txtapiurl.Text;
            string wxToken = this.txtwxToken.Text;
            string AppId = this.txtAppId.Text;
            string AppSecret = this.txtAppSecret.Text;

            var model = bll.GetModel(_id);

            model.wxName = wxName;
            model.wxId = wxId;
            model.weixinCode = weixinCode;
            model.headerpic = headerpic;
            model.apiurl = apiurl;
            model.wxToken = wxToken;
            model.AppId = AppId;
            model.AppSecret = AppSecret;
            model.wStatus =MyCommFun.Str2Int( rblwStatus.SelectedItem.Value);

            int addYear =MyCommFun.Str2Int( ddlMaxNum.SelectedItem.Value);
            if (addYear > 0)
            {
                if (model.endDate.Value >= DateTime.Now)
                {
                    //直接加
                    model.endDate = model.endDate.Value.AddYears(addYear);
                }
                else
                { 
                    //已过期的，直接在当天开始加年份
                    model.endDate = DateTime.Now.AddYears(addYear);
                 }

                bool isAgent = false;
                if (adminEntity.agentLevel < 0)
                {
                    return false;
                }
                if (adminEntity.agentLevel > 0)
                {
                    agent = aBll.GetAgentModel(adminEntity.id);
                    isAgent = true;
                    if (agent.remainMony < agent.agentPrice)
                    {
                        JscriptMsg("余额不足，请联系管理员充值！", "", "Error");
                        return false;
                    }
                    else
                    {
                        int xfjine = addYear * agent.agentPrice.Value;//消费金额

                        //是代理商 :缴费，扣除金额，增加消费记录
                        agent.remainMony -= xfjine;
                        bool updateRet = aBll.Update(agent);
                        if (updateRet)
                        {
                            var bBll = new WXManagerBillService(new WXManagerBillRepository());
                            var bill = new WX_ManagerBillInfo
                            {
                                billMoney = xfjine,
                                managerId = agent.managerId,
                                operPersonId = agent.managerId,
                                operDate = DateTime.Now,
                                billUsed = "微帐号" + model.wxName + "增加时间" + addYear + "年",
                                moneyType = "扣减"
                            };

                            bBll.Add(bill);

                        }
                        else
                        {
                            JscriptMsg("数据执行错误，请重新操作！", "", "Error");
                            return false;
                        }
                    }
                }               
            }

            bool ret = bll.Update(model);

            if (ret)
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "【管理】修改微信号，主键为:" + model.id + ",微信号为：" + model.weixinCode); //记录日志
                return true;
            }
            return false;
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
            //如果是从微用户管理里来的，还得判断下
            if (MyCommFun.QueryString("fpage").Trim().Length > 0 && MyCommFun.RequestInt("uid") > 0)
            {
                returnPage = "weixin_list.aspx?id=" + MyCommFun.RequestInt("uid");
                JscriptMsg("修改微信公众帐号信息成功！", returnPage, "Success");
            }
            else
            {
                JscriptMsg("修改微信公众帐号信息成功！", "wxcodemgr.aspx", "Success");
            }
        }


    }
}