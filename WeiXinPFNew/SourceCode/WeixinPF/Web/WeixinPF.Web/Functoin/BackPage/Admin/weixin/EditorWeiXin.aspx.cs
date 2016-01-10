using System;
using System.Configuration;
using WeixinPF.Application.Weixin.Service;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Infrastructure.Article;
using WeixinPF.Infrastructure.Weixin;
using WeixinPF.Model.WeiXin;
using WeixinPF.Web.UI;

namespace WeixinPF.Web.Functoin.BackPage.Admin.weixin
{
    public partial class EditorWeiXin : ManagePage
    {
        /// <summary>
        /// 当前操作类型
        /// </summary>
        private MXEnums.ActionEnum CurrentAction
        {
            get
            {
                var actionString = MXRequest.GetQueryString("action");
                MXEnums.ActionEnum action;
                return Enum.TryParse(actionString, true, out action) ? action : MXEnums.ActionEnum.Add;
            }
        }

        private int Id = 0;
        private AppInfoService _appInfoService;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _appInfoService = new AppInfoService();

                if (CurrentAction == MXEnums.ActionEnum.Edit)
                {
                    if (!int.TryParse(Request.QueryString["id"] as string, out this.Id))
                    {
                        JscriptMsg("传输参数不正确！", "back", "Error");
                        return;
                    }
                    if (!_appInfoService.Exists(this.Id))
                    {
                        JscriptMsg("记录不存在或已被删除！", "back", "Error");
                        return;
                    }
                }
                else
                {
                    //添加，则需要判断可以添加的微信号数量
                    if (IsChaoGuoWxNum())
                    {
                        return;
                    }
                }

                if (!IsPostBack)
                {
                    txtapiurl.Text = MyCommFun.getWebSite() + "/api/weixin/api.aspx";
                    //  ChkAdminLevel("manager_list", MXEnums.ActionEnum.View.ToString()); //检查权限
                    //1e2124dd04e11d01b9df2865f85944be
                    var model = GetAdminInfo(); //取得管理员信息

                    if (this.CurrentAction == MXEnums.ActionEnum.Edit) //修改
                    {
                        ShowInfo(this.Id);
                    }
                    else
                    {
                        txtEncodingAESKey.Text = Utils.GetLetterOrNumberRandom(43);
                        txtwxToken.Text = Utils.GetLetterOrNumberRandom(10);
                    }
                }
            }
            catch (Exception exception)
            {
                JscriptMsg(exception.Message, "back", "Error");
            }
        }



        #region 赋值操作=================================
        private void ShowInfo(int id)
        {

            var model = _appInfoService.GetAppInfo(id);
            this.Id = model.id;

            this.txtwxName.Text = model.wxName;
            this.txtwxId.Text = model.WxId;

            this.txtweixinCode.Text = model.WxCode;

            this.txtImgUrl.Text = model.headerpic;
            //this.txtapiurl.Text = model.apiurl;
            this.txtwxToken.Text = model.WxToken;
            this.txtAppId.Text = model.AppId;
            this.txtAppSecret.Text = model.AppSecret;
            txtapiurl.Text = MyCommFun.getWebSite() + "/api/weixin/api.aspx?apiid=" + model.id; //todo: 改地址
            this.txtEncodingAESKey.Text = model.extStr;
            rblweixintype.SelectedValue = model.WxType == null ? "3" : model.WxType.Value.ToString();
        }

        #endregion

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
            string EncodingAESKey = txtEncodingAESKey.Text;
            DateTime createDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddYears(1);


            var model = new AppInfo
            {
                uId = uId,
                wxName = wxName,
                WxId = wxId,
                YixinId = "",
                WxCode = weixinCode,
                WxPwd = wxPwd,
                headerpic = headerpic,
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
                extStr = EncodingAESKey,
                WxType = MyCommFun.Obj2Int(rblweixintype.SelectedItem.Value),
                Remark = "",
                Seq = 99
            };

            //-1为无限制
            //暂存入extStr字段里

            if (IsChaoGuoWxNum())
            {
                return false;
            }

            int wId = _appInfoService.Add(model);
            this.Id = wId;
            if (wId > 0)
            {
                Object obj = ConfigurationManager.AppSettings["industry_defaultAdd"];
                if (obj != null && obj.ToString() == "true")
                {
                    //根据登录者所在行业为微帐号添加相应默认模块
                    var mModel = GetAdminInfo(); //取得管理员信息
                    var idBll = new WXIndustryDefaultModuleService(new IndustryDefaultModuleRepository());
                    idBll.addMouduleByRoleid(mModel.role_id, wId, new ArticleCategoryRepository(siteConfig.sysdatabaseprefix));
                }

                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加微信号，主键为:" + model.id + ",微信号为：" + model.WxCode); //记录日志
                return true;
            }
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
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

            if (strErr != "")
            {
                JscriptMsg(strErr, "back", "Error");

                return false;
            }

            string wxName = this.txtwxName.Text;
            string wxId = this.txtwxId.Text;
            string weixinCode = this.txtweixinCode.Text;
            string headerpic = this.txtImgUrl.Text;
            string apiurl = MyCommFun.getWebSite() + "/api/weixin/api.aspx?apiid=" + _id;
            string wxToken = this.txtwxToken.Text;
            string AppId = this.txtAppId.Text;
            string AppSecret = this.txtAppSecret.Text;
            bool noChangeAppProp = true;
            string EncodingAESKey = txtEncodingAESKey.Text;
            var model = _appInfoService.GetAppInfo(_id);

            model.wxName = wxName;
            model.WxId = wxId;
            model.WxCode = weixinCode;
            model.headerpic = headerpic;
            model.Apiurl = apiurl;
            model.WxToken = wxToken;
            model.extStr = EncodingAESKey;//暂存入extStr字段里
            model.WxType = MyCommFun.Obj2Int(rblweixintype.SelectedItem.Value);

            if (model.AppId != AppId || model.AppSecret != AppSecret)
            {
                //改变了
                noChangeAppProp = false;
            }
            model.AppId = AppId;
            model.AppSecret = AppSecret;

            bool ret = _appInfoService.Update(model);

            if (ret)
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改微信号，主键为:" + model.id + ",微信号为：" + model.WxCode); //记录日志
                if (!noChangeAppProp)
                {  //更新access_token值
                    UpdateAccess_Token(_id, AppId, AppSecret);
                }
                return true;
            }
            return false;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CurrentAction == MXEnums.ActionEnum.Edit) //修改
            {
                // ChkAdminLevel("manager_list", MXEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.Id))
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("修改微信公众帐号信息成功！", "myweixinlist.aspx", "Success");
            }
            else //添加
            {
                // ChkAdminLevel("manager_list", MXEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }

                JscriptMsg("添加微信公众帐号信息成功！", "editorWeiXin.aspx?action=" + MXEnums.ActionEnum.Edit + "&id=" + this.Id, "Success");
            }
        }

        /// <summary>
        ///  判断已经有的微信个数，若超过，则不能添加
        ///  1e2124dd04e11d01b9df2865f85944be
        /// </summary>
        /// <returns>超过为true,未超过为false</returns>
        private bool IsChaoGuoWxNum()
        {
            var manager = GetAdminInfo();
            int hasNum = _appInfoService.GetUserWxNumCount(manager.id);
            if (hasNum >= manager.wxNum)
            {
                JscriptMsg("你只能添加" + manager.wxNum + "个微信公众帐号,若想添加多个，请联系管理员！", "myweixinlist.aspx", "Error");
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  //更新access_token值
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="AppId"></param>
        /// <param name="AppSecret"></param>
        private void UpdateAccess_Token(int _id, string AppId, string AppSecret)
        {
            try
            {
                var pBll = new WXPropertyService(new PropertyRepository());

                if (!pBll.ExistsWid(_id))
                {
                    return;
                }
                string newToken = "";

                try
                {
                    var result = OneGulp.WeChat.MP.CommonAPIs.CommonApi.GetToken(AppId, AppSecret);
                    newToken = result.access_token;

                }
                catch (Exception ex)
                {
                    JscriptMsg("AppId或者AppSecret填写错误！", "", "Error");

                }
                finally
                {
                    //更新到数据库里
                    var wxProperty = pBll.GetModelList("iName='access_token' and wid=" + _id)[0];
                    wxProperty.iContent = newToken;
                    wxProperty.createDate = DateTime.Now;
                    pBll.Update(wxProperty);
                }
            }
            catch (Exception ex)
            {

            }


        }

    }
}