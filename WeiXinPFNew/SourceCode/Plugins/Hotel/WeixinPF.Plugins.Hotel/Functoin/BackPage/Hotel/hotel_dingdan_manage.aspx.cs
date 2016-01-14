﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Common.Helper;
using WeixinPF.Hotel.Plugins.Functoin.Service;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Web.UI;

namespace WeixinPF.Hotel.Plugins.Functoin.BackPage.Hotel
{
    public partial class hotel_dingdan_manage : ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        //BLL.wx_hotel_dingdan gbll = new BLL.wx_hotel_dingdan();
        protected string keywords = string.Empty;
        public int hotelid = 0;

        protected string beginDate = string.Empty;
        protected string endDate = string.Empty;
        protected decimal payAmountMin = -1;
        protected decimal payAmountMax = -1;
        protected int orderStatus = -1;
        protected string orderNumber = string.Empty;
        protected string _strWhere = string.Empty;
        protected string _orderby = "orderTime desc,id desc";


        protected void Page_Load(object sender, EventArgs e)
        {
            //获取参数
            GetQueryString();
            hotelid = MyCommFun.RequestInt("hotelid", GetHotelId());

            if (!Page.IsPostBack)
            {


                this.pageSize = GetPageSize(10); //每页数量

                GetWhere();



                BindDropBox();
                BindControls();
                RptBind();
            }
        }

        private void GetWhere()
        {
            _strWhere = " hotelid=" + hotelid + " " + _strWhere;
            _strWhere += CombSqlTxt(keywords);
        }

        /// <summary>
        /// 绑定
        /// </summary>
        private void BindControls()
        {
            txtbeginDate.Text = this.beginDate;
            txtendDate.Text = this.endDate;
            ddlOrderStatus.SelectedValue = this.orderStatus.ToString();
            if (this.payAmountMin > -1)
            {
                txtPayAmountMin.Text = this.payAmountMin.ToString();
            }
            this.payAmountMax = MXRequest.GetQueryDecimal("payAmountMax", -1);
            if (this.payAmountMax > -1)
            {
                txtPayAmountMax.Text = this.payAmountMax.ToString();
            }
            if (!string.IsNullOrEmpty(orderNumber))
            {
                txtOrderNumber.Text = this.orderNumber;
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        private void GetQueryString()
        {
            if (MyCommFun.IsRequestStr("beginDate", MyCommFun.RequestObjType.dateType))
            {
                this.beginDate = MXRequest.GetQueryString("beginDate");

            }
            if (MyCommFun.IsRequestStr("endDate", MyCommFun.RequestObjType.dateType))
            {
                this.endDate = MXRequest.GetQueryString("endDate");


            }
            if (MyCommFun.IsRequestStr("orderStatus", MyCommFun.RequestObjType.intType))
            {
                this.orderStatus = MXRequest.GetQueryInt("orderStatus");


            }
            this.payAmountMin = MXRequest.GetQueryDecimal("payAmountMin", -1);


            this.orderNumber = MXRequest.GetQueryString("orderNumber");


            this.keywords = MXRequest.GetQueryString("keywords");
        }

        //获取查询结果url
        private string GetNewUrl(string page)
        {
            var queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendFormat("hotel_dingdan_manage.aspx?hotelid={0}&page={1}&keywords={2}", this.hotelid, page, this.keywords);

            if (!string.IsNullOrEmpty(this.ddlOrderStatus.SelectedValue) && this.ddlOrderStatus.SelectedValue != "-1")
                queryStringBuilder.AppendFormat("&orderStatus={0}", this.ddlOrderStatus.SelectedValue.Trim());

            if (!string.IsNullOrEmpty(this.txtbeginDate.Text))
                queryStringBuilder.AppendFormat("&beginDate={0}", this.txtbeginDate.Text.Trim());
            if (!string.IsNullOrEmpty(this.txtendDate.Text))
                queryStringBuilder.AppendFormat("&endDate={0}", this.txtendDate.Text);
            if (!string.IsNullOrEmpty(this.txtPayAmountMax.Text))
                queryStringBuilder.AppendFormat("&payAmountMax={0}", this.txtPayAmountMax.Text);
            if (!string.IsNullOrEmpty(this.txtPayAmountMin.Text))
                queryStringBuilder.AppendFormat("&payAmountMin={0}", this.txtPayAmountMin.Text);
            if (!string.IsNullOrEmpty(this.txtOrderNumber.Text))
                queryStringBuilder.AppendFormat("&orderNumber={0}", this.txtOrderNumber.Text);


            return queryStringBuilder.ToString();
        }

        #region 数据绑定=================================
        private void RptBind()
        {


            this.page = MXRequest.GetQueryInt("page", 1);
            //            txtKeywords.Text = this.keywords;
            DataSet ds = GetQueryData();


            this.rptList.DataSource = ds;
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = this.GetNewUrl("__id__"); //Utils.CombUrlTxt("hotel_dingdan_manage.aspx", "keywords={0}&page={1}&hotelid={2}", this.keywords, "__id__", hotelid.ToString());
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);


        }

        /// <summary>
        /// 绑定订单状态下拉框
        /// </summary>
        private void BindDropBox()
        {
            BindDropOrderStatus();
        }

        private void BindDropOrderStatus()
        {
            var statusList = HotelStatusManager.OrderStatus.GetList();
            statusList.Insert(0, new StatusDict() { StatusId = -1, StatusName = "请选择" });

            ddlOrderStatus.DataSource = statusList;
            ddlOrderStatus.DataValueField = "StatusId";
            ddlOrderStatus.DataTextField = "StatusName";
            ddlOrderStatus.DataBind();
        }

        /// <summary>
        /// 获取查询结果
        /// </summary>
        /// <returns></returns>
        private DataSet GetQueryData()
        {
            //var hotelService = new HotelService();
            //var bllhotel = new BLL.wx_hotels_info();
            //var hotel = bllhotel.GetModel(hotelid);


            this.page = MXRequest.GetQueryInt("page", 1);

            var result = Global.Bus.Send<GetPagedOrderListResponse>(Constants.HotelServiceAddress,
                new GetPagedOrderListRequest()
                {
                    PageSize = pageSize,
                    Page = page,
                    Where = _strWhere,
                    Orderby = _orderby
                });

            if (!result.IsSuccess)
            {
                totalCount = 0;
                return null;
            }

            totalCount = result.Data.TotalCount;
            return result.Data.OrderList;
            //DataSet ds = gbll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);

            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ds.Tables[0].Columns.Add("isRefund", typeof(string));

            //    ds.Tables[0].Columns.Add("hotelName", typeof(string));
            //    ds.Tables[0].Columns.Add("totalPrice", typeof(decimal));
            //    ds.Tables[0].Columns.Add("statusName", typeof(string));
            //    ds.Tables[0].Columns.Add("strisRefund", typeof(string));

            //    DataRow dr;

            //    int count = ds.Tables[0].Rows.Count;
            //    for (int i = 0; i < count; i++)
            //    {
            //        dr = ds.Tables[0].Rows[i];

            //        int id = dr.Field<int>("id");
            //        int hotelId= dr.Field<int>("hotelId");
            //        var status = HotelStatusManager.OrderStatus.GetStatusDict(
            //            MyCommFun.Obj2Int(dr["orderStatus"]));
            //        dr["payStatusStr"] = "<em  style='width:70px;' class='status " + status.CssClass
            //            + "'>" + status.StatusName + "</em>";
            //        dr["statusName"] = status.StatusName;
            //        if (status.StatusId == HotelStatusManager.OrderStatus.Refunding.StatusId
            //            || status.StatusId == HotelStatusManager.OrderStatus.Refunded.StatusId)
            //        {
            //            dr["isRefund"] = "<em  style='width:70px;' class='status ok'>是</em>";
            //            dr["strisRefund"] = "是";
            //        }
            //        else if(status.StatusId == HotelStatusManager.OrderStatus.Completed.StatusId)
            //        {
            //            var tuidanDto = hotelService.GetModel(id, hotelId);
            //            if (tuidanDto != null)
            //            {
            //                dr["isRefund"] = "<em  style='width:70px;' class='status ok'>是</em>";
            //                dr["strisRefund"] = "是";
            //            }
            //            else
            //            {
            //                dr["isRefund"] = "<em  style='width:70px;' class='status no'>否</em>";
            //                dr["strisRefund"] = "否";
            //            }
            //        }
            //        else
            //        {
            //            dr["isRefund"] = "<em  style='width:70px;' class='status no'>否</em>";
            //            dr["strisRefund"] = "否";

            //        }
            //        dr["hotelName"] = hotel.hotelName;
            //        //总花费
            //        var dateSpan = dr.Field<DateTime>("leaveTime") - dr.Field<DateTime>("arriveTime");
            //        var totalPrice = MyCommFun.Str2Decimal(dr["price"].ToString()) * dr.Field<int>("orderNum") * dateSpan.Days;
            //        dr["totalPrice"] = totalPrice;
            //    }
            //    ds.AcceptChanges();
            //}
            //return ds;
        }

        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append("oderName like  '%" + _keywords + "%' ");
            }

            //             protected string beginDate = string.Empty;
            //        protected string endDate = string.Empty;
            //        protected decimal payAmountMin = -1;
            //        protected decimal payAmountMax = -1;
            //        protected int orderStatus = -1;

            if (!string.IsNullOrEmpty(beginDate))
            {
                strTemp.Append(" AND orderTime >='" + beginDate + "' ");
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                strTemp.Append(" AND  orderTime <='" + endDate + "' ");
            }

            if (payAmountMin >= 0)
            {
                strTemp.Append(" AND  price *  orderNum * (DATEDIFF ( DAY,arriveTime,leaveTime)) >=" + payAmountMin + " ");
            }

            if (payAmountMax >= 0)
            {
                strTemp.Append("  AND  price *  orderNum * (DATEDIFF ( DAY,arriveTime,leaveTime)) <=" + payAmountMax + "  ");
            }

            if (orderStatus >= 0)
            {
                strTemp.Append("  AND  orderStatus =" + orderStatus + "  ");
            }

            if (!string.IsNullOrEmpty(orderNumber))
            {
                strTemp.Append(" AND  orderNumber like  '%" + orderNumber + "%' ");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("hotel_list_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetNewUrl("1"));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("hotel_list_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("hotel_list.aspx", "keywords={0}", this.keywords));
        }

        //批量删除 
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // ChkAdminLevel("manager_list", MXEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    //if (gbll.Delete(id))
                    //{
                    //    sucCount += 1;
                    //}
                    //else
                    //{
                    //    errorCount += 1;
                    //}
                }
            }
            AddAdminLog(MXEnums.ActionEnum.Delete.ToString(), "信息" + sucCount + "条，失败" + errorCount + "条"); //记录日志

            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("hotel_list.aspx", "keywords={0}", this.keywords), "Success");
        }


        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            try
            {
                GetWhere();
                var result = GetQueryData();
                List<string> headerList = new List<string>() { "序号", "订单编号", "订单状态", "是否退单",
                    "商户或门店名称","	预定人","交易日期","商品名称","购买数量","商品价格","入住时间"
                    ,"离店时间","支付金额" };
                List<int> columnIndexList = new List<int>() { 1, 5, 22, 23, 20, 4, 18, 11, 13, 14, 9, 10, 21 };
                CSVHelper.DownloadAsSCV(Response, "酒店订单管理", result.Tables[0], columnIndexList, headerList);
            }
            catch (Exception)
            {
                // ignored
            }
        }



    }
}