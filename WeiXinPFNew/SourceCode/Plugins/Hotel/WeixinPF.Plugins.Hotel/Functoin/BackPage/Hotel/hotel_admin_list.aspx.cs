using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WeixinPF.Common;
using WeixinPF.Web.UI;

namespace WeixinPF.Hotel.Plugins.Functoin.BackPage.Hotel
{
    public partial class hotel_admin_list : ManagePage
    {

        protected int hotelid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid = MyCommFun.RequestInt("hotelid");
            if (!IsPostBack)
            {
                RptBind();
            }
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void RptBind()
        {
            BLL.wx_hotel_admin adminBll = new BLL.wx_hotel_admin();
            List<Model.wx_hotel_admin> adminList = adminBll.GetModelList("HotelId=" + hotelid);

            BLL.manager managerBll = new BLL.manager();
            DataSet dsData = new DataSet();
            if (adminList.Any())
            {
                string strWhere = string.Empty;
                for (int index = 0; index <= adminList.Count - 1; index++)
                {
                    strWhere += "," + adminList[index].ManagerId;
                }

                strWhere = "(" + strWhere.Substring(1) + ")";

                dsData = managerBll.GetList(0, "id in " + strWhere, string.Empty);
                rptList.DataSource = dsData;
            }
            else
            {
                rptList.DataSource = new List<Model.manager>();
            }

            rptList.DataBind();
        }
    }
}