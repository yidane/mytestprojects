using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using WeixinPF.Application.Agent;
using WeixinPF.Application.Agent.Service;
using WeixinPF.Application.Weixin.Service;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Infrastructure.Agent;
using WeixinPF.Infrastructure.Article;
using WeixinPF.Infrastructure.Weixin;
using WeixinPF.Model.WeiXin;
using WeixinPF.Web.UI;

namespace WeiXinPF.Web.admin.manager
{
    public partial class weixin_add : ManagePage
    {
        protected int uid = 0;
        AppInfoService bll = new AppInfoService();
        protected void Page_Load(object sender, EventArgs e)
        {
            uid = MyCommFun.RequestInt("uid", 0);

            //添加，则需要判断可以添加的微信号数量
            if (uid == 0)
            {
                JscriptMsg("参数不正确！", "back", "Error");
                return;
            }
            if (IsChaoGuoWxNum())
            {
                JscriptMsg("该用户微账号的数量已满，无法添加！", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("wcodemgr", MXEnums.ActionEnum.View.ToString()); //检查权限
                txtEndData.Text = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
                var mBll = new ManagerInfoService();
                var user = mBll.GetModel(uid);
                lblUserName.Text = user.UserName + " " + user.RealName;

            }
        }

        #region 增加操作=================================
        private bool DoAdd()
        {
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
            if (this.txtEndData.Text.Trim().Length == 0)
            {
                strErr += "到期时间不能为空！";
            }

            if (strErr != "")
            {
                JscriptMsg(strErr, "back", "Error");

                return false;
            }

            var manager = GetAdminInfo();
            int uId = manager.Id;
            string wxName = this.txtwxName.Text;
            string wxId = this.txtwxId.Text;

            string weixinCode = this.txtweixinCode.Text;
            string wxPwd = "";
            string headerpic = this.txtImgUrl.Text;
            string apiurl = "";
            string wxToken = this.txtwxToken.Text;
            string wxProvince = "";
            string wxCity = "";
            string AppId = this.txtAppId.Text;
            string AppSecret = this.txtAppSecret.Text;
            DateTime createDate = DateTime.Now;
            DateTime endDate = MyCommFun.Obj2DateTime(txtEndData.Text);


            var model = new AppInfo
            {
                UId = this.uid,
                WxName = wxName,
                WxId = wxId,
                YixinId = "",
                WxCode = weixinCode,
                WxPwd = wxPwd,
                Headerpic = headerpic,
                Apiurl = apiurl,
                WxToken = wxToken,
                WxProvince = wxProvince,
                WxCity = wxCity,
                AppId = AppId,
                AppSecret = AppSecret,
                AccessToken = "",
                OpenIdStr = "",
                CreateDate = createDate,
                EndDate = endDate,
                WenziMaxNum = -1,
                TuwenMaxNum = -1,
                YuyinMaxNum = -1,
                WenziDefineNum = 0,
                TuwenDefineNum = 0,
                YuyinDefineNum = 0,
                RequestTtNum = 0,
                RequestUsedNum = 0,
                SmsTtNum = 0,
                SmsUsedNum = 0,
                IsDelete = false,
                WStatus = true,
                Remark = "",
                Seq = 99
            };

            //-1为无限制

            if (IsChaoGuoWxNum())
            {
                return false;
            }
            int wId = bll.Add(model);
            if (wId > 0)
            {
                Object obj = ConfigurationManager.AppSettings["industry_defaultAdd"];
                if (obj != null && obj.ToString() == "true")
                {
                    //为微账户添加行业默认模块
                    var mBll = new ManagerInfoService();
                    var idBll = new WXIndustryDefaultModuleService(new IndustryDefaultModuleRepository());
                    var user = mBll.GetModel(uid);
                    int roleid = user.RoleId;
                    idBll.addMouduleByRoleid(roleid, wId, new ArticleCategoryRepository(siteConfig.sysdatabaseprefix));
                }

                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加微信号，主键为:" + model.Id + ",微信号为：" + model.WxCode); //记录日志
                return true;
            }
            return false;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("wcodemgr", MXEnums.ActionEnum.Add.ToString()); //检查权限
            if (!DoAdd())
            {
                JscriptMsg("保存过程中发生错误！", "", "Error");
                return;
            }

            JscriptMsg("添加微信公众帐号信息成功！", "weixin_list.aspx?id=" + this.uid, "Success");

        }

        /// <summary>
        ///  判断已经有的微信个数，若超过，则不能添加
        /// </summary>
        /// <returns>超过为true,未超过为false</returns>
        private bool IsChaoGuoWxNum()
        {
            var mBll = new ManagerInfoService();
            var manager = mBll.GetModel(this.uid);

            int hasNum = bll.GetUserWxNumCount(this.uid);
            if (hasNum >= manager.WxNum)
            {
                JscriptMsg("该用户只能添加" + manager.WxNum + "个微信公众帐号,若想添加多个，请联系管理员！", "weixin_list.aspx?id=" + uid, "Error");
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}