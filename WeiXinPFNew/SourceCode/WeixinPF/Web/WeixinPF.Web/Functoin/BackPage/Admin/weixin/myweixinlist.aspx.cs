using System;
using System.Text;
using System.Web.UI.WebControls;
using WeixinPF.Application.Weixin.Service;
using WeixinPF.Common;
using WeixinPF.Infrastructure.Weixin;
using WeixinPF.Model.WeiXin;
using WeixinPF.Web.UI;

namespace WeixinPF.Web.Functoin.BackPage.Admin.weixin
{
    public partial class myweixinlist : ManagePage
    {
        protected int totalCount;
        protected int page = 1;
        protected int pageSize = 20;
        private AppInfoService bll;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            bll = new AppInfoService();
            this.keywords = MXRequest.GetQueryString("keywords");
            if (!Page.IsPostBack)
            {
                //RptBind(CombSqlTxt(keywords), "createDate desc,id desc");
                RptBind(keywords, "createDate desc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            var model = GetAdminInfo(); //取得当前管理员信息
            _strWhere = "uId=" + model.Id + " and isDelete=0 " + _strWhere + " order by " + _orderby;

            txtKeywords.Text = this.keywords;
            var wxList = bll.GetModelList(model.Id, _strWhere);

            if (wxList != null)
            {
                lblHasNum.Text = wxList.Count.ToString();
                if (wxList.Count > 0)
                {
                    foreach (AppInfo appInfo in wxList)
                    {
                        appInfo.extStr = "<span class=\"span_zhengchang\">正常</span>";
                        if (appInfo.wStatus)
                        {
                            appInfo.extStr = "<span class=\"span_jinyong\">禁用</span>";
                        }

                        if (appInfo.EndDate != null)
                        {
                            if (appInfo.EndDate < DateTime.Now)
                            {
                                appInfo.extStr = "<span class=\"span_guoqi\">过期</span>";
                            }
                            else if (appInfo.EndDate < DateTime.Now.AddDays(15))
                            {
                                appInfo.extStr = "<span class=\"span_kguoqi\">快到期</span>";
                            }
                        }
                    }
                }
            }

            lblTotNum.Text = model.WxNum.ToString();

            this.rptList.DataSource = wxList;
            this.rptList.DataBind();
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (wxName like  '%" + _keywords + "%' or weixinCode like '%" + _keywords + "%')");
            }

            return strTemp.ToString();
        }
        #endregion



        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("myweixinlist.aspx", "keywords={0}", txtKeywords.Text));
        }



        /// <summary>
        /// 选择某一个微信公众帐号，并且将其保存到session里
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "toIndex":
                    {
                        int wid = int.Parse(e.CommandArgument.ToString());
                        var weixin = bll.GetAppInfo(wid);
                        if (weixin.wStatus)
                        {
                            MessageBox.Show(this, "账号已被禁用，无法进入");
                            return;
                        }

                        if (weixin.EndDate != null)
                        {
                            if (weixin.EndDate < DateTime.Now)
                            {
                                MessageBox.Show(this, "账号已过期，无法进入");
                                return;
                            }
                        }

                        Session["nowweixin"] = weixin;
                        Utils.WriteCookie("nowweixinId", "WeiXinPF", e.CommandArgument.ToString());
                        Response.Write("<script>parent.location.href='../../../../index.aspx'</script>");
                    }
                    break;
            }
        }


    }
}