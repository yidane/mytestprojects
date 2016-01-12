
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
using WeixinPF.Model.WeiXin;
using WeixinPF.Web.UI;

namespace WeixinPF.Web.Functoin.BackPage.Admin.weixin
{
    /// <summary>
    /// 微支付的相关配置
    /// li pu   
    /// </summary>
    public partial class baseSetting : ManagePage
    {

        private AppInfoService bll;
        protected void Page_Load(object sender, EventArgs e)
        {
            bll = new AppInfoService();
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
            lblWeixinName.Text = weixin.WxName;
            lblAppid.Text = weixin.AppId;

            var wxpayBll = new WXPaymentService(new PaymentRepository());
            var model = wxpayBll.GetModelByWid(weixin.Id);
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
            var propertyBll = new WXPropertyService(new PropertyRepository());
            var propertyEntity = propertyBll.GetModelByIName(weixin.Id, MXEnums.WXPropertyKeyName.OpenOauth.ToString());
            if (propertyEntity != null)
            {
                radOpenOAuth.SelectedValue = propertyEntity.iContent;
                hidOpenOauthId.ID = propertyEntity.Id.ToString();
            }            
        }

        #endregion
 

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var wxpayBll = new WXPaymentService(new PaymentRepository());
            int id =MyCommFun.Obj2Int( hidId.Value,0);
            var wxpayModel = new PaymentInfo();
            var weixin = GetWeiXinCode();
            if (id == 0)
            {
                //新增
                
                  wxpayModel.wid = weixin.Id;
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
            var propertyBll = new WXPropertyService(new PropertyRepository());
            string pValue=radOpenOAuth.SelectedItem.Value;
            propertyBll.AddProperty(weixin.Id, MXEnums.WXPropertyKeyName.OpenOauth.ToString(), pValue);

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