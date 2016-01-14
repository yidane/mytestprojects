using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WeixinPF.Common;
using WeixinPF.Web.UI;

namespace WeixinPF.Hotel.Plugins.Functoin.BackPage.Hotel
{
    public partial class hotel_user_list : ManagePage
    {
        protected int hotelid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid = MXRequest.GetQueryInt("hotelid", GetHotelId());
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
            BLL.wx_hotel_user dcBll = new BLL.wx_hotel_user();
            List<Model.wx_hotel_user> hotelAdmins = dcBll.GetModelList("hotelId=" + hotelid);

            BLL.manager managerBll = new BLL.manager();
            DataSet dsData = new DataSet();
            if (hotelAdmins.Any())
            {
                string strWhere = string.Empty;
                for (int index = 0; index <= hotelAdmins.Count - 1; index++)
                {
                    strWhere += "," + hotelAdmins[index].ManagerId;
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