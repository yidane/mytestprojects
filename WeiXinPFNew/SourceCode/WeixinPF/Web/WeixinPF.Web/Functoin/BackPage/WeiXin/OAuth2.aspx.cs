using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OneGulp.WeChat.MP;
using OneGulp.WeChat.MP.AdvancedAPIs;
using WeixinPF.Application.Weixin.Repository;
using WeixinPF.Application.Weixin.Service;
using WeixinPF.Common;

namespace WeixinPF.Web.Functoin.BackPage.WeiXin
{
    public partial class OAuth2 : System.Web.UI.Page
    {
        /// <summary>
        /// OAuth2之后跳转的链接
        /// </summary>
        private string ReturnUrl
        {
            get { return Request.QueryString["ReturnUrl"]; }
        }

        /// <summary>
        /// OAuth2第一从微信获取到的参数
        /// </summary>
        private string Code
        {
            get { return Request.QueryString["Code"]; }
        }

        /// <summary>
        /// 平台公众号配置的公众号ID
        /// </summary>
        private int Wid
        {
            get
            {
                var wid = 0;
                return int.TryParse(Request.QueryString["wid"], out wid) ? wid : 0;
            }
        }

        /// <summary>
        /// 用户OpenId
        /// </summary>
        private string OpenId
        {
            get { return Request.QueryString["OpenId"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //如果存在OpenId，就不再处理
                if (!string.IsNullOrEmpty(OpenId))
                    return;

                var appInfo = new AppInfoService().GetAppInfo(Wid);
                if (appInfo == null)
                    throw new Exception(string.Format("不合法的参数wid {0}", Wid));

                //判断是否存在Code
                if (string.IsNullOrEmpty(Code))
                {
                    //存在Code，则向微信申请Code
                    var newUrl = OAuthApi.GetAuthorizeUrl(appInfo.AppId, "http://www.cloudorg.com.cn/admin/login.aspx", "OAuth2", OAuthScope.snsapi_base);
                    Response.Redirect(newUrl);
                }
                else
                {
                    var result = OAuthApi.GetAccessToken(appInfo.AppId, appInfo.AppSecret, Code);
                    Response.Redirect(string.Format("{0}&OpenId={1}", ReturnUrl, result.openid));
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}