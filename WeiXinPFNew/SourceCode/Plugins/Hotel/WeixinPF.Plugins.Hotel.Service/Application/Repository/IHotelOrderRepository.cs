using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WeixinPF.Application.BaseRepository;
using WeixinPF.Common;
using WeixinPF.DBUtility;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Repository
{
    public interface IHotelOrderRepository //:IRepository<HotelOrderInfo>
    {

        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int id);


        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(HotelOrderInfo model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(HotelOrderInfo model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int id);

        /// <summary>
        /// 批量删除数据
        /// </summary>
        bool DeleteList(string idlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        HotelOrderInfo GetModel(int id);

        HotelOrderInfo DataRowToModel(DataRow row);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        List<HotelOrderInfo> GetList(string strWhere);

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);

        /// <summary>
        /// 获取记录总数
        /// </summary>
        int GetRecordCount(string strWhere);

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount);

        DataSet GetList(int hotelid);

        bool Updatehotel(HotelOrderInfo model);
        bool Update(int id, string status);

        bool Update(int id);

        DataSet GetList(string openid, int hotelid);

        DataSet GetListWithSql(string openid, int hotelid, string orderstatus, string orderby);

        DataSet GetUserOrderList(string openid, int wid, string sqlWhere, string orderby);

        HotelOrderInfo GetLastUserModel(string openid);

        HotelOrderInfo GetModel(string outTradeNo);

        DataSet GetWeChatRefundParams(int wid, int hotelid, int dingdanId, string refundCode, string modelName);
    }
}