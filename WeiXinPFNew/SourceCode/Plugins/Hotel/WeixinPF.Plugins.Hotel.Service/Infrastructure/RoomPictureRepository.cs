using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WeixinPF.DBUtility;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Plugins.Hotel.Service.Application.Repository;

namespace WeixinPF.Hotel.Plugins.Service.Infrastructure
{
    public class RoomPictureRepository: IRoomPictureRepository
    {

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "wx_hotel_roompic");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_hotel_roompic");
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
        public int Add(RoomPictureInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_hotel_roompic(");
            strSql.Append("roomid,hotelid,title,sortpicid,roomPic,roomPictz,createDate)");
            strSql.Append(" values (");
            strSql.Append("@roomid,@hotelid,@title,@sortpicid,@roomPic,@roomPictz,@createDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@roomid", SqlDbType.Int,4),
                    new SqlParameter("@hotelid", SqlDbType.Int,4),
                    new SqlParameter("@title", SqlDbType.VarChar,200),
                    new SqlParameter("@sortpicid", SqlDbType.Int,4),
                    new SqlParameter("@roomPic", SqlDbType.VarChar,200),
                    new SqlParameter("@roomPictz", SqlDbType.VarChar,200),
                    new SqlParameter("@createDate", SqlDbType.DateTime)};
            parameters[0].Value = model.roomid;
            parameters[1].Value = model.hotelid;
            parameters[2].Value = model.title;
            parameters[3].Value = model.sortpicid;
            parameters[4].Value = model.roomPic;
            parameters[5].Value = model.roomPictz;
            parameters[6].Value = model.createDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(RoomPictureInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_hotel_roompic set ");
            strSql.Append("roomid=@roomid,");
            strSql.Append("hotelid=@hotelid,");
            strSql.Append("title=@title,");
            strSql.Append("sortpicid=@sortpicid,");
            strSql.Append("roomPic=@roomPic,");
            strSql.Append("roomPictz=@roomPictz,");
            strSql.Append("createDate=@createDate");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@roomid", SqlDbType.Int,4),
                    new SqlParameter("@hotelid", SqlDbType.Int,4),
                    new SqlParameter("@title", SqlDbType.VarChar,200),
                    new SqlParameter("@sortpicid", SqlDbType.Int,4),
                    new SqlParameter("@roomPic", SqlDbType.VarChar,200),
                    new SqlParameter("@roomPictz", SqlDbType.VarChar,200),
                    new SqlParameter("@createDate", SqlDbType.DateTime),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.roomid;
            parameters[1].Value = model.hotelid;
            parameters[2].Value = model.title;
            parameters[3].Value = model.sortpicid;
            parameters[4].Value = model.roomPic;
            parameters[5].Value = model.roomPictz;
            parameters[6].Value = model.createDate;
            parameters[7].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_hotel_roompic ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_hotel_roompic ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RoomPictureInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,roomid,hotelid,title,sortpicid,roomPic,roomPictz,createDate from wx_hotel_roompic ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            RoomPictureInfo model = new RoomPictureInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return ds.Tables[0].Rows.Count > 0 ? DataRowToModel(ds.Tables[0].Rows[0]) : null;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RoomPictureInfo DataRowToModel(DataRow row)
        {
            RoomPictureInfo model = new RoomPictureInfo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["roomid"] != null && row["roomid"].ToString() != "")
                {
                    model.roomid = int.Parse(row["roomid"].ToString());
                }
                if (row["hotelid"] != null && row["hotelid"].ToString() != "")
                {
                    model.hotelid = int.Parse(row["hotelid"].ToString());
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["sortpicid"] != null && row["sortpicid"].ToString() != "")
                {
                    model.sortpicid = int.Parse(row["sortpicid"].ToString());
                }
                if (row["roomPic"] != null)
                {
                    model.roomPic = row["roomPic"].ToString();
                }
                if (row["roomPictz"] != null)
                {
                    model.roomPictz = row["roomPictz"].ToString();
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,roomid,hotelid,title,sortpicid,roomPic,roomPictz,createDate ");
            strSql.Append(" FROM wx_hotel_roompic ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
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
            strSql.Append(" id,roomid,hotelid,title,sortpicid,roomPic,roomPictz,createDate ");
            strSql.Append(" FROM wx_hotel_roompic ");
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
            strSql.Append("select count(1) FROM wx_hotel_roompic ");
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
            strSql.Append(")AS Row, T.*  from wx_hotel_roompic T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        
        public bool Deletepic(int roomid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_hotel_roompic ");
            strSql.Append(" where roomid=@roomid");
            SqlParameter[] parameters = {
                    new SqlParameter("@roomid", SqlDbType.Int,4)
            };
            parameters[0].Value = roomid;

            var rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }

        public DataSet GetList(int roomid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,roomid,hotelid,title,sortpicid,roomPic,roomPictz,createDate ");
            strSql.Append(" FROM wx_hotel_roompic where roomid='" + roomid + "' ");

            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
