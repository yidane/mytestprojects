using System;
using System.Text;
using System.Web.UI.WebControls;
using WeixinPF.Application.Agent;
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
    public partial class manager_list : ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = MXRequest.GetQueryString("keywords");

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("manager_list", MXEnums.ActionEnum.View.ToString()); //检查权限
                var model = GetAdminInfo(); //取得当前管理员信息
                if (model.agentLevel > 0)
                {
                    RptBind("agentId=" + model.id + CombSqlTxt(keywords), "add_time desc,id desc");
                }
                else
                {
                    RptBind("agentId=" + model.id + " and agentLevel<0 " + CombSqlTxt(keywords), "add_time desc,id desc");
                }
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = MXRequest.GetQueryInt("page", 1);
            txtKeywords.Text = this.keywords;
            var bll = new ManagerService(new ManagerRepository(siteConfig.sysdatabaseprefix));
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("manager_list.aspx", "keywords={0}&page={1}", this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name like  '%" + _keywords + "%' or real_name like '%" + _keywords + "%' or email like '%" + _keywords + "%')");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("manager_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("manager_list.aspx", "keywords={0}", txtKeywords.Text));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("manager_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("manager_list.aspx", "keywords={0}", this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("manager_list", MXEnums.ActionEnum.Delete.ToString()); //检查权限

            bool isAgent = false;
            var aBll = new WXAgentService(new WXAgentRepository());
            var adminEntity = GetAdminInfo(); //取得管理员信息
            var agent = new WX_AgentInfo();
            if (adminEntity.agentLevel > 0)
            {
                isAgent = true;
                agent = aBll.GetAgentModel(adminEntity.id);
            }

            var wBll = new AppInfoService(); 
            int sucCount = 0;
            int errorCount = 0;
            
            var bll = new ManagerService(new ManagerRepository(siteConfig.sysdatabaseprefix));
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    int hasNum = wBll.GetUserWxNumCount(id);
                    if (hasNum > 0)
                    {
                        JscriptMsg("该用户已经添加微信号，无法删除！", "back", "Error");
                        return;
                    }
                }
            }

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    int hasNum = wBll.GetUserWxNumCount(id);
                   

                        if (bll.Delete(id))
                        {
                            sucCount += 1;
                        }
                        else
                        {
                            errorCount += 1;
                        }
                    
                }
            }

            if (isAgent && agent!=null)
            {
                //如果为代理商，则将起用户数量减掉
                agent.userNum -= sucCount;
                aBll.Update(agent);
            }
            AddAdminLog(MXEnums.ActionEnum.Delete.ToString(), "删除用户" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("manager_list.aspx", "keywords={0}", this.keywords), "Success");
        }
    }
}