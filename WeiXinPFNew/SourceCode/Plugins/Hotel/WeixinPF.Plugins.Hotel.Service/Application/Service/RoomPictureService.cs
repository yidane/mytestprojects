using System.Collections.Generic;
using System.Data;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Infrastructure;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Service
{
    public class RoomPictureService
    {
        private readonly IRoomPictureRepository _roomPictureRepository;

        public RoomPictureService()
        {
            _roomPictureRepository=new RoomPictureRepository();
        }
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return _roomPictureRepository.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return _roomPictureRepository.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(RoomPictureInfo model)
        {
            return _roomPictureRepository.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(RoomPictureInfo model)
        {
            return _roomPictureRepository.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return _roomPictureRepository.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return _roomPictureRepository.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RoomPictureInfo GetModel(int id)
        {

            return _roomPictureRepository.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return _roomPictureRepository.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return _roomPictureRepository.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<RoomPictureInfo> GetModelList(string strWhere)
        {
            DataSet ds = _roomPictureRepository.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<RoomPictureInfo> DataTableToList(DataTable dt)
        {
            List<RoomPictureInfo> modelList = new List<RoomPictureInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                RoomPictureInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = _roomPictureRepository.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return _roomPictureRepository.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return _roomPictureRepository.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return _roomPictureRepository.GetList(PageSize,PageIndex,strWhere);
        //}
        
       
        public bool Deletepic(int roomid)
        {

            return _roomPictureRepository.Deletepic(roomid);
        }

        public DataSet GetList(int roomid)
        {
            return _roomPictureRepository.GetList(roomid);
        }
    }
}
