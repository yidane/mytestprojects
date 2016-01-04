using System.Data;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Repository
{
    public interface IRoomPictureRepository
    {
        int GetMaxId();
        bool Exists(int id);
        int Add(RoomPictureInfo model);
        bool Update(RoomPictureInfo model);
        bool Delete(int id);
        bool DeleteList(string idlist);
        RoomPictureInfo GetModel(int id);
        RoomPictureInfo DataRowToModel(DataRow row);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        bool Deletepic(int roomid);
        DataSet GetList(int roomid);
    }
}
