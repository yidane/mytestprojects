using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using WeixinPF.Application.Agent;
using WeixinPF.Application.Weixin.Service;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Infrastructure.Agent;
using WeixinPF.Infrastructure.Article;
using WeixinPF.Infrastructure.Weixin;
using WeixinPF.Model.Weixin;
using WeixinPF.Web.UI;

namespace WeiXinPF.Web.admin.manager
{
    public partial class weixin_add : ManagePage
    {
        protected int uid = 0;
        WXUserService bll = new WXUserService(new WXUserRepository());
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
                var mBll = new ManagerService(new ManagerRepository(siteConfig.sysdatabaseprefix));
                var user = mBll.GetModel(uid);
                lblUserName.Text = user.user_name + " " + user.real_name;

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
            int uId = manager.id;
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


            var model = new WX_UserWeixinInfo
            {
                uId = this.uid,
                wxName = wxName,
                wxId = wxId,
                yixinId = "",
                weixinCode = weixinCode,
                wxPwd = wxPwd,
                headerpic = headerpic,
                apiurl = apiurl,
                wxToken = wxToken,
                wxProvince = wxProvince,
                wxCity = wxCity,
                AppId = AppId,
                AppSecret = AppSecret,
                Access_Token = "",
                openIdStr = "",
                createDate = createDate,
                endDate = endDate,
                wenziMaxNum = -1,
                tuwenMaxNum = -1,
                yuyinMaxNum = -1,
                wenziDefineNum = 0,
                tuwenDefineNum = 0,
                yuyinDefineNum = 0,
                requestTTNum = 0,
                requestUsedNum = 0,
                smsTTNum = 0,
                smsUsedNum = 0,
                isDelete = false,
                wStatus = 1,
                remark = "",
                seq = 99
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
                    var mBll = new ManagerService(new ManagerRepository(siteConfig.sysdatabaseprefix));
                    var idBll = new WXIndustryDefaultModuleService(new WXIndustryDefaultModuleRepository());
                    var user = mBll.GetModel(uid);
                    int roleid = user.role_id;
                    idBll.addMouduleByRoleid(roleid, wId, new ArticleCategoryRepository(siteConfig.sysdatabaseprefix));
                }

                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加微信号，主键为:" + model.id + ",微信号为：" + model.weixinCode); //记录日志
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
            var mBll = new ManagerService(new ManagerRepository(siteConfig.sysdatabaseprefix));
            var manager = mBll.GetModel(this.uid);

            int hasNum = bll.GetUserWxNumCount(this.uid);
            if (hasNum >= manager.wxNum)
            {
                JscriptMsg("该用户只能添加" + manager.wxNum + "个微信公众帐号,若想添加多个，请联系管理员！", "weixin_list.aspx?id=" + uid, "Error");
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}