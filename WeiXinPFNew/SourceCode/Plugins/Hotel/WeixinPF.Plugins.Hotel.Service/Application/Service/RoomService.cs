using System.Collections.Generic;
using System.Data;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Infrastructure;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Service
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService()
        {
            _roomRepository = new RoomRepository();
        }

        public int GetMaxId()
        {
            return _roomRepository.GetMaxId();
        }

        public bool Exists(int id)
        {
            return _roomRepository.Exists(id);
        }

        public int Add(RoomInfo model)
        {
            return _roomRepository.Add(model);
        }

        public bool Update(RoomInfo model)
        {
            return _roomRepository.Update(model);
        }

        public bool Delete(int id)
        {

            return _roomRepository.Delete(id);
        }
        public bool DeleteList(string idlist)
        {
            return _roomRepository.DeleteList(idlist);
        }

        public RoomInfo GetModel(int id)
        {

            return _roomRepository.GetModel(id);
        }
        public DataSet GetList(string strWhere)
        {
            return _roomRepository.GetList(strWhere);
        }
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return _roomRepository.GetList(Top, strWhere, filedOrder);
        }
        public List<RoomInfo> GetModelList(string strWhere)
        {
            return _roomRepository.GetModelList(strWhere);
        }


        public DataSet GetAllList()
        {
            return GetList("");
        }


        public int GetRecordCount(string strWhere)
        {
            return _roomRepository.GetRecordCount(strWhere);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return _roomRepository.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return _roomRepository.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        public DataSet GetList(int hotelid)
        {
            return _roomRepository.GetList(hotelid);
        }
        public string GetRoomCode(int hotelid)
        {
            return _roomRepository.GetRoomCode(hotelid);
        }
    }
}
