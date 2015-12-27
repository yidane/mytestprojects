using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.BaseRepository;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Application.Repository
{
    public interface IHotelRepository
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
        int Add(HotelInfo model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(HotelInfo model);

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
        HotelInfo GetModel(int id);

        List<HotelInfo> GetModelList(string strWhere);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        HotelInfo DataRowToModel(DataRow row);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

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
        DataSet GetList();
    }
}
