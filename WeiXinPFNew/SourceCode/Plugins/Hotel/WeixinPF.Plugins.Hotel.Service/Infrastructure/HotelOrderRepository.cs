﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using WeixinPF.Common;
using WeixinPF.DBUtility;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Infrastructure.BaseRepository;

namespace WeixinPF.Hotel.Plugins.Service.Infrastructure
{
    public class HotelOrderRepository:IHotelOrderRepository//:EFRepository<HotelOrderInfo>,IHotelOrderRepository
    {
        //public HotelOrderRepository()//(DbContext dbContext) : base(dbContext)
        //{

        //}

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "wx_hotel_dingdan");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_hotel_dingdan");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(HotelOrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_hotel_dingdan(");
            strSql.Append("hotelid,openid,oderName,orderNumber,wxOrderNumber,identityNumber,tel,arriveTime,leaveTime,roomType,orderTime,orderNum,price,orderStatus,isDelete,createDate,roomid,yuanjia,remark)");
            strSql.Append(" values (");
            strSql.Append("@hotelid,@openid,@oderName,@orderNumber,@wxOrderNumber,@identityNumber,@tel,@arriveTime,@leaveTime,@roomType,@orderTime,@orderNum,@price,@orderStatus,@isDelete,@createDate,@roomid,@yuanjia,@remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@hotelid", SqlDbType.Int,4),
                    new SqlParameter("@openid", SqlDbType.VarChar,200),
                    new SqlParameter("@oderName", SqlDbType.VarChar,100),
                    new SqlParameter("@tel", SqlDbType.VarChar,100),
                    new SqlParameter("@arriveTime", SqlDbType.DateTime),
                    new SqlParameter("@leaveTime", SqlDbType.DateTime),
                    new SqlParameter("@roomType", SqlDbType.VarChar,200),
                    new SqlParameter("@orderTime", SqlDbType.DateTime),
                    new SqlParameter("@orderNum", SqlDbType.Int,4),
                    new SqlParameter("@price", SqlDbType.Float,8),
                    new SqlParameter("@orderStatus", SqlDbType.Int,4),
                    new SqlParameter("@isDelete", SqlDbType.Int,4),
                    new SqlParameter("@createDate", SqlDbType.DateTime),
                    new SqlParameter("@roomid", SqlDbType.Int,4),
                    new SqlParameter("@yuanjia", SqlDbType.Float,8),
                    new SqlParameter("@remark", SqlDbType.NVarChar,100),
                    new SqlParameter("@orderNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@wxOrderNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@identityNumber", SqlDbType.VarChar,50)};
            parameters[0].Value = model.hotelid;
            parameters[1].Value = model.openid;
            parameters[2].Value = model.oderName;
            parameters[3].Value = model.tel;
            parameters[4].Value = model.arriveTime;
            parameters[5].Value = model.leaveTime;
            parameters[6].Value = model.roomType;
            parameters[7].Value = model.orderTime;
            parameters[8].Value = model.orderNum;
            parameters[9].Value = model.price;
            parameters[10].Value = model.orderStatus;
            parameters[11].Value = model.isDelete;
            parameters[12].Value = model.createDate;
            parameters[13].Value = model.roomid;
            parameters[14].Value = model.yuanjia;
            parameters[15].Value = model.remark;
            parameters[16].Value = model.orderNumber;
            parameters[17].Value = model.wxOrderNumber;
            parameters[18].Value = model.identityNumber;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(HotelOrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_hotel_dingdan set ");
            strSql.Append("hotelid=@hotelid,");
            strSql.Append("openid=@openid,");
            strSql.Append("oderName=@oderName,");
            strSql.Append("orderNumber=@orderNumber,");
            strSql.Append("wxOrderNumber=@wxOrderNumber,");
            strSql.Append("identityNumber=@identityNumber,");
            strSql.Append("tel=@tel,");
            strSql.Append("arriveTime=@arriveTime,");
            strSql.Append("leaveTime=@leaveTime,");
            strSql.Append("roomType=@roomType,");
            strSql.Append("orderTime=@orderTime,");
            strSql.Append("orderNum=@orderNum,");
            strSql.Append("price=@price,");
            strSql.Append("orderStatus=@orderStatus,");
            strSql.Append("isDelete=@isDelete,");
            strSql.Append("createDate=@createDate,");
            strSql.Append("roomid=@roomid,");
            strSql.Append("yuanjia=@yuanjia,");
            strSql.Append("remark=@remark");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@hotelid", SqlDbType.Int,4),
                    new SqlParameter("@openid", SqlDbType.VarChar,200),
                    new SqlParameter("@oderName", SqlDbType.VarChar,100),
                    new SqlParameter("@tel", SqlDbType.VarChar,100),
                    new SqlParameter("@arriveTime", SqlDbType.DateTime),
                    new SqlParameter("@leaveTime", SqlDbType.DateTime),
                    new SqlParameter("@roomType", SqlDbType.VarChar,200),
                    new SqlParameter("@orderTime", SqlDbType.DateTime),
                    new SqlParameter("@orderNum", SqlDbType.Int,4),
                    new SqlParameter("@price", SqlDbType.Float,8),
                    new SqlParameter("@orderStatus", SqlDbType.Int,4),
                    new SqlParameter("@isDelete", SqlDbType.Int,4),
                    new SqlParameter("@createDate", SqlDbType.DateTime),
                    new SqlParameter("@roomid", SqlDbType.Int,4),
                    new SqlParameter("@yuanjia", SqlDbType.Float,8),
                    new SqlParameter("@remark", SqlDbType.NVarChar,100),
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@orderNumber", SqlDbType.NVarChar,50),
                    new SqlParameter("@wxOrderNumber", SqlDbType.NVarChar,50),
                    new SqlParameter("@identityNumber", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.hotelid;
            parameters[1].Value = model.openid;
            parameters[2].Value = model.oderName;
            parameters[3].Value = model.tel;
            parameters[4].Value = model.arriveTime;
            parameters[5].Value = model.leaveTime;
            parameters[6].Value = model.roomType;
            parameters[7].Value = model.orderTime;
            parameters[8].Value = model.orderNum;
            parameters[9].Value = model.price;
            parameters[10].Value = model.orderStatus;
            parameters[11].Value = model.isDelete;
            parameters[12].Value = model.createDate;
            parameters[13].Value = model.roomid;
            parameters[14].Value = model.yuanjia;
            parameters[15].Value = model.remark;
            parameters[16].Value = model.id;
            parameters[17].Value = model.orderNumber;
            parameters[18].Value = model.wxOrderNumber;
            parameters[19].Value = model.identityNumber;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_hotel_dingdan ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_hotel_dingdan ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HotelOrderInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,hotelid,openid,oderName,orderNumber,wxOrderNumber,identityNumber,tel,arriveTime,leaveTime,roomType,orderTime,orderNum,price,orderStatus,isDelete,createDate,roomid,yuanjia,remark from wx_hotel_dingdan ");
            strSql.Append(" where id=@id and isDelete='0' ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            HotelOrderInfo model = new HotelOrderInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HotelOrderInfo DataRowToModel(DataRow row)
        {
            HotelOrderInfo model = new HotelOrderInfo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["hotelid"] != null && row["hotelid"].ToString() != "")
                {
                    model.hotelid = int.Parse(row["hotelid"].ToString());
                }
                if (row["openid"] != null)
                {
                    model.openid = row["openid"].ToString();
                }
                if (row["oderName"] != null)
                {
                    model.oderName = row["oderName"].ToString();
                }
                if (row["orderNumber"] != null)
                {
                    model.orderNumber = row["orderNumber"].ToString();
                }
                if (row["wxOrderNumber"] != null)
                {
                    model.wxOrderNumber = row["wxOrderNumber"].ToString();
                }
                if (row["identityNumber"] != null)
                {
                    model.identityNumber = row["identityNumber"].ToString();
                }
                if (row["tel"] != null)
                {
                    model.tel = row["tel"].ToString();
                }
                if (row["arriveTime"] != null && row["arriveTime"].ToString() != "")
                {
                    model.arriveTime = DateTime.Parse(row["arriveTime"].ToString());
                }
                if (row["leaveTime"] != null && row["leaveTime"].ToString() != "")
                {
                    model.leaveTime = DateTime.Parse(row["leaveTime"].ToString());
                }
                if (row["roomType"] != null)
                {
                    model.roomType = row["roomType"].ToString();
                }
                if (row["orderTime"] != null && row["orderTime"].ToString() != "")
                {
                    model.orderTime = DateTime.Parse(row["orderTime"].ToString());
                }
                if (row["orderNum"] != null && row["orderNum"].ToString() != "")
                {
                    model.orderNum = int.Parse(row["orderNum"].ToString());
                }
                if (row["price"] != null && row["price"].ToString() != "")
                {
                    model.price = double.Parse(row["price"].ToString());
                }
                if (row["orderStatus"] != null && row["orderStatus"].ToString() != "")
                {
                    model.orderStatus = int.Parse(row["orderStatus"].ToString());
                }
                if (row["isDelete"] != null && row["isDelete"].ToString() != "")
                {
                    model.isDelete = int.Parse(row["isDelete"].ToString());
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
                }
                if (row["roomid"] != null && row["roomid"].ToString() != "")
                {
                    model.roomid = int.Parse(row["roomid"].ToString());
                }
                if (row["yuanjia"] != null && row["yuanjia"].ToString() != "")
                {
                    model.yuanjia = double.Parse(row["yuanjia"].ToString());
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<HotelOrderInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select aa.*,bb.hotelName as hotelName,(Select Top 1 c.roomPic From dbo.wx_hotel_roompic c Where c.roomid=aa.roomid) As RoomPicture FROM wx_hotel_dingdan  as aa right join wx_hotels_info as bb on aa.hotelid=bb.id and aa.isDelete=0 ");

            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" Where " + strWhere);
            }
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
               return  db.Query<HotelOrderInfo>(strSql.ToString()).ToList();
            }

            //return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,hotelid,openid,oderName,orderNumber,wxOrderNumber,identityNumber,tel,arriveTime,leaveTime,roomType,orderTime,orderNum,price,orderStatus,isDelete,createDate,roomid,yuanjia,remark ");
            strSql.Append(" FROM wx_hotel_dingdan ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM wx_hotel_dingdan ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from wx_hotel_dingdan T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,ROW_NUMBER() OVER(ORDER BY hotelid,orderTime desc) AS orderno,hotelid,openid,oderName,orderNumber,wxOrderNumber,identityNumber,tel,arriveTime,leaveTime,roomType,orderTime,orderNum,price,orderStatus,isDelete,'' as payStatusStr,createDate ");
            strSql.Append(" FROM wx_hotel_dingdan ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }


        public DataSet GetList(int hotelid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select aa.id,aa.hotelid,aa.openid,bb.hotelName as hotelName,aa.oderName,aa.orderNumber,aa.wxOrderNumber,aa.identityNumber,aa.tel,");
            strSql.Append(" aa.arriveTime,aa.leaveTime,aa.roomType,aa.orderTime,aa.orderNum,aa.price,aa.orderStatus,aa.isDelete,aa.createDate,aa.roomid,aa.yuanjia,aa.remark  ");
            strSql.Append("from wx_hotel_dingdan  as aa left join (select * from wx_hotels_info where id='" + hotelid + "' ) as bb on bb.id=aa.hotelid");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool Updatehotel(HotelOrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_hotel_dingdan set ");
            strSql.Append("oderName=@oderName,");
            //            strSql.Append("orderNumber=@orderNumber,");
            strSql.Append("wxOrderNumber=@wxOrderNumber,");
            strSql.Append("identityNumber=@identityNumber,");
            strSql.Append("tel=@tel,");
            strSql.Append("arriveTime=@arriveTime,");
            strSql.Append("leaveTime=@leaveTime,");
            strSql.Append("orderNum=@orderNum,");
            strSql.Append("price=@price,");
            strSql.Append("yuanjia=@yuanjia,");
            strSql.Append("remark=@remark");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@oderName", SqlDbType.VarChar,100),
                    new SqlParameter("@tel", SqlDbType.VarChar,100),
                    new SqlParameter("@arriveTime", SqlDbType.DateTime),
                    new SqlParameter("@leaveTime", SqlDbType.DateTime),
                    new SqlParameter("@orderNum", SqlDbType.Int,4),
                    new SqlParameter("@price", SqlDbType.Float,8),
                    new SqlParameter("@yuanjia", SqlDbType.Float,8),
                    new SqlParameter("@remark", SqlDbType.NVarChar,100),
                    new SqlParameter("@id", SqlDbType.Int,4),
//                    new SqlParameter("@orderNumber", SqlDbType.NVarChar,50),
                    new SqlParameter("@wxOrderNumber", SqlDbType.NVarChar,50),
                    new SqlParameter("@identityNumber", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.oderName;
            parameters[1].Value = model.tel;
            parameters[2].Value = model.arriveTime;
            parameters[3].Value = model.orderNum;
            parameters[4].Value = model.price;
            parameters[5].Value = model.yuanjia;
            parameters[6].Value = model.remark;
            parameters[7].Value = model.id;
            //            parameters[8].Value = model.OrderNumber;
            parameters[8].Value = model.wxOrderNumber;
            parameters[9].Value = model.identityNumber;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int id, string status)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  wx_hotel_dingdan  set orderStatus='" + status + "'  , orderTime=GETDATE()  where  id='" + id + "'  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool Update(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  wx_hotel_dingdan  set isDelete='1'  where  id='" + id + "'  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public DataSet GetList(string openid, int hotelid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select aa.*,bb.hotelName as hotelName FROM wx_hotel_dingdan  as aa inner join wx_hotels_info as bb on aa.hotelid=bb.id ");

            strSql.Append(" and aa.openid='" + openid + "' and aa.isDelete='0'  and aa.hotelid='" + hotelid + "' order by orderTime  ");

            return DbHelperSQL.Query(strSql.ToString());
        }


        public DataSet GetListWithSql(string openid, int hotelid, string orderstatus, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select aa.*,bb.hotelName as hotelName FROM wx_hotel_dingdan  as aa inner join wx_hotels_info as bb on aa.hotelid=bb.id ");

            strSql.Append(" and aa.openid='" + openid + "' and aa.isDelete='0'  and aa.hotelid='" + hotelid + "'");

            if (!string.IsNullOrEmpty(orderstatus))
            {
                strSql.Append(orderstatus);
            }

            if (!string.IsNullOrEmpty(orderby))
            {
                strSql.Append(orderby);
            }

            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetUserOrderList(string openid, int wid, string sqlWhere, string orderby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select aa.*,bb.hotelName as hotelName FROM wx_hotel_dingdan  as aa inner join wx_hotels_info as bb on aa.hotelid=bb.id ");

            strSql.Append(" and aa.openid='" + openid + "' and aa.isDelete='0'  and bb.wid='" + wid + "'");

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                strSql.Append(" and " + sqlWhere);
            }

            if (!string.IsNullOrEmpty(orderby))
            {
                strSql.Append(orderby);
            }

            return DbHelperSQL.Query(strSql.ToString());
        }

        public HotelOrderInfo GetLastUserModel(string openid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from wx_hotel_dingdan ");
            strSql.Append(" where openid =@id and isDelete='0'  ORDER BY orderTime DESC ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.VarChar,200)
            };
            parameters[0].Value = openid;

            HotelOrderInfo model = new HotelOrderInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public HotelOrderInfo GetModel(string outTradeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,hotelid,openid,oderName,orderNumber,wxOrderNumber,identityNumber,tel,arriveTime,leaveTime,roomType,orderTime,orderNum,price,orderStatus,isDelete,createDate,roomid,yuanjia,remark from wx_hotel_dingdan ");
            strSql.Append(" where orderNumber=@orderNumber and isDelete='0' ");
            SqlParameter[] parameters = {
                    new SqlParameter("@orderNumber", SqlDbType.VarChar,50)
            };
            parameters[0].Value = outTradeNo;

            HotelOrderInfo model = new HotelOrderInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }



        public DataSet GetWeChatRefundParams(int wid, int hotelid, int dingdanId, string refundCode, string modelName)
        {
            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@HotelID",SqlDbType = SqlDbType.Int,Value = hotelid},
                    new SqlParameter(){ParameterName = "@wid",SqlDbType = SqlDbType.Int,Value = wid},
                    new SqlParameter(){ParameterName = "@OrderID",SqlDbType = SqlDbType.Int,Value = dingdanId},
                    new SqlParameter(){ParameterName = "@refundCode",SqlDbType = SqlDbType.NVarChar,Value = refundCode},
                    new SqlParameter(){ParameterName = "@modelName",SqlDbType = SqlDbType.NVarChar,Value = modelName}
                };

            return DbHelperSQL.RunProcedure("usp_wx_hotel_tuidan_manage_WeChatDetail", sqlparams, "WeChatRefundDetail");
        }
    }
}
