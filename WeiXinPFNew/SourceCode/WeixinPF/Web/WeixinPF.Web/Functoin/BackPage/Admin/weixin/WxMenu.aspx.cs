using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OneGulp.WeChat.MP.Entities.Menu;
using WeixinPF.Common;
using WeixinPF.Web.UI;

namespace WeixinPF.Web.Functoin.BackPage.Admin.weixin
{
    public partial class WxMenu : ManagePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        /// <summary>
        /// 更新菜单
        /// </summary>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Model.wx_userweixin weixin = GetWeiXinCode();

            //    string error = "";
            //    string accessToken = WeiXinCRMComm.getAccessToken(weixin.id, out error);

            //    if (error != "")
            //    {
            //        JscriptMsg(error, "", "Error");
            //        return;
            //    }


            //    //重新整理按钮信息
            //    ButtonGroup bg = new ButtonGroup();
            //    TextBox txtName = new TextBox();
            //    TextBox txtKey = new TextBox();
            //    TextBox txtUrl = new TextBox();
            //    IList<BaseButton> topList = new List<BaseButton>();
            //    IList<SingleButton> subList = new List<SingleButton>();
            //    #region 菜单设置
            //    for (int i = 0; i < 3; i++)
            //    {
            //        txtName = this.FindControl("txtTop" + (i + 1)) as TextBox;
            //        txtKey = this.FindControl("txtTop" + (i + 1) + "Key") as TextBox;
            //        txtUrl = this.FindControl("txtTop" + (i + 1) + "Url") as TextBox;
            //        if (txtName.Text.Trim() == "")
            //        {
            //            // 如果名称为空，则忽略该菜单，以及下面的子菜单
            //            continue;
            //        }


            //        subList = new List<SingleButton>();
            //        TextBox txtSubName = new TextBox();
            //        TextBox txtSubKey = new TextBox();
            //        TextBox txtSubUrl = new TextBox();
            //        for (int j = 0; j < 5; j++)
            //        {
            //            #region 子菜单的设置
            //            txtSubName = this.FindControl("txtMenu" + (i + 1) + (j + 1)) as TextBox;
            //            txtSubKey = this.FindControl("txtMenu" + (i + 1) + (j + 1) + "Key") as TextBox;
            //            txtSubUrl = this.FindControl("txtMenu" + (i + 1) + (j + 1) + "Url") as TextBox;
            //            if (txtSubName.Text.Trim() == "")
            //            {
            //                continue;
            //            }

            //            if (txtSubUrl.Text.Trim() != "")
            //            {
            //                SingleViewButton sub = new SingleViewButton();
            //                sub.name = txtSubName.Text.Trim();
            //                sub.url = txtSubUrl.Text.Trim();
            //                subList.Add(sub);

            //            }
            //            else if (txtSubKey.Text.Trim() != "")
            //            {
            //                SingleClickButton sub = new SingleClickButton();
            //                sub.name = txtSubName.Text.Trim();
            //                sub.key = txtSubKey.Text.Trim();
            //                subList.Add(sub);
            //            }
            //            else
            //            {
            //                //报错 :子菜单必须有key和name
            //                JscriptMsg("二级菜单的名称和key或者url必填！", "", "Error");
            //                return;
            //            }
            //            #endregion

            //        } //子菜单循环结束


            //        if (subList != null && subList.Count > 0)
            //        {
            //            //有子菜单, 该一级菜单是SubButton
            //            if (subList.Count < 1)
            //            {
            //                JscriptMsg("子菜单个数为2~5个！", "", "Error");
            //                return;
            //            }
            //            SubButton topButton = new SubButton(Utils.CutString(txtName.Text.Trim(), 16));
            //            topButton.sub_button.AddRange(subList);
            //            topList.Add(topButton);
            //        }
            //        else
            //        {
            //            // 无子菜单
            //            if (txtKey.Text.Trim() == "" && txtUrl.Text.Trim() == "")
            //            {
            //                JscriptMsg("若无子菜单，必须填写key或者url值！", "", "Error");
            //                return;
            //            }

            //            if (txtUrl.Text.Trim() != "")
            //            {  //view 页面跳转
            //                SingleViewButton topSingleButton = new SingleViewButton();
            //                topSingleButton.name = txtName.Text.Trim();
            //                topSingleButton.url = txtUrl.Text.Trim();
            //                topList.Add(topSingleButton);
            //            }
            //            else if (txtKey.Text.Trim() != "")
            //            {
            //                SingleClickButton topSingleButton = new SingleClickButton();
            //                topSingleButton.name = txtName.Text.Trim();
            //                topSingleButton.key = txtKey.Text.Trim();
            //                topList.Add(topSingleButton);
            //            }
            //        }
            //    }
            //    #endregion

            //    bg.button.AddRange(topList);

            //    var result = mMrg.CreateMenu(accessToken, bg);
            //    JscriptMsg("菜单提交成功！", "wxMenu.aspx", "Success");
            //}
            //catch (Exception ex)
            //{
            //    JscriptMsg("报错：" + ex.Message, "", "Error");
            //}

        }

        /// <summary>
        /// 删当前菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelMenu_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Model.wx_userweixin weixin = GetWeiXinCode();

            //    string error = "";
            //    string token = WeiXinCRMComm.getAccessToken(weixin.id, out error);
            //    if (error != "")
            //    {
            //        JscriptMsg(error, "", "Error");
            //        return;
            //    }
            //    var result = OneGulp.WeChat.MP.CommonAPIs.CommonApi.DeleteMenu(token);
            //    //重新获得最新的菜单
            //    GetMenu();

            //}
            //catch (Exception ex)
            //{
            //    JscriptMsg("执行过程出现错误：" + ex.Message, "back", "Error");
            //    return;
            //}

        }
	}
}