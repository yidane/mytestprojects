
/**************************************
 *
 * A_uthor:
 * createDate:2015-8-1
 * update:2015-8-1
 * 
 ***********************************/

using System;
using WeixinPF.Application.Weixin.Service;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Infrastructure.Weixin;
using WeixinPF.Model.Weixin;
using WeixinPF.Web.UI;

namespace WeixinPF.Web.Functoin.BackPage.Admin.weixin
{
    /// <summary>
    /// 微支付的相关配置
    /// li pu   
    /// </summary>
    public partial class baseSetting : ManagePage
    {

        private WXUserService bll;
        protected void Page_Load(object sender, EventArgs e)
        {
            bll = new WXUserService(new WXUserRepository());
            if (!Page.IsPostBack)
            {
                //txtapiurl.Text = MyCommFun.getWebSite() + "/api/weixin/api.aspx";
                ChkAdminLevel("wx_paysetting", MXEnums.ActionEnum.View.ToString()); //检查权限
                ShowInfo();

            }
        }

        #region 赋值操作=================================
        private void ShowInfo()
        {
            var weixin = GetWeiXinCode();
            lblWeixinName.Text = weixin.wxName;
            lblAppid.Text = weixin.AppId;

            var wxpayBll = new WXPaymentService(new WXPaymentRepository());
            var model = wxpayBll.GetModelByWid(weixin.id);
            if (model == null || model.id == 0)
            {
                //新增记录
            }
            else
            { 
                //修改
                hidId.Value = model.id.ToString();
                this.txtmch_id.Text = model.mch_id;
                this.txtpaykey.Text = model.paykey;
                this.txtcertInfoPath.Text = model.certInfoPath;
                this.txtcerInfoPwd.Text = model.cerInfoPwd;

            }
            var propertyBll = new WXPropertyService(new WXPropertyRepository());
            var propertyEntity = propertyBll.GetModelByIName(weixin.id, MXEnums.WXPropertyKeyName.OpenOauth.ToString());
            if (propertyEntity != null)
            {
                radOpenOAuth.SelectedValue = propertyEntity.iContent;
                hidOpenOauthId.ID = propertyEntity.id.ToString();
            }            
        }

        #endregion
 

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var wxpayBll = new WXPaymentService(new WXPaymentRepository());
            int id =MyCommFun.Obj2Int( hidId.Value,0);
            var wxpayModel = new WX_PaymentInfo();
            var weixin = GetWeiXinCode();
            if (id == 0)
            {
                //新增
                
                  wxpayModel.wid = weixin.id;
                  wxpayModel.createDate = DateTime.Now;
            }
            else
            { 
                //修改
                wxpayModel = wxpayBll.GetModel(id);
            }

            wxpayModel.mch_id = txtmch_id.Text.Trim();
            wxpayModel.paykey = txtpaykey.Text.Trim();
            wxpayModel.certInfoPath = txtcertInfoPath.Text.Trim();
            wxpayModel.cerInfoPwd = txtcerInfoPwd.Text.Trim();
           
            bool ret = false;
            if (id == 0)
            {
                wxpayModel.createDate = DateTime.Now;
                int retNum=wxpayBll.Add(wxpayModel);
                if (retNum > 0)
                {
                    ret = true;
                }
            }
            else
            {
               ret= wxpayBll.Update(wxpayModel);
            }

            //OpenOAuth开启
            var propertyBll = new WXPropertyService(new WXPropertyRepository());
            string pValue=radOpenOAuth.SelectedItem.Value;
            propertyBll.AddProperty(weixin.id, MXEnums.WXPropertyKeyName.OpenOauth.ToString(), pValue);

            if (ret)
            {
                JscriptMsg("修改信息成功！", "baseSetting.aspx", "Success");
            }
            else
            {
                JscriptMsg("修改信息成功！", "", "Error");
                return;
            }
            
        }

    }
}