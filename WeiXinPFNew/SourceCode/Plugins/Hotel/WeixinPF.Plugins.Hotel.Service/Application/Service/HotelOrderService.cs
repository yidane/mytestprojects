using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using WeixinPF.Application;
using WeixinPF.Common.Enum;
using WeixinPF.Hotel.Plugins.Service.Application.Repository;
using WeixinPF.Hotel.Plugins.Service.Infrastructure;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Service
{
    public class HotelOrderService
    {
        private readonly IHotelOrderRepository _repository;

        public HotelOrderService()//(//IHotelOrderRepository repository)
        {
            _repository = new HotelOrderRepository();

        }
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return _repository.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return _repository.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(HotelOrderInfo model)
        {
            return _repository.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(HotelOrderInfo model)
        {
            return _repository.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return _repository.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HotelOrderInfo GetModel(int id)
        {

            return _repository.GetModel(id);
        }
        
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return _repository.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return _repository.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<HotelOrderInfo> GetModelList(string strWhere)
        {
            DataSet ds = _repository.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<HotelOrderInfo> DataTableToList(DataTable dt)
        {
            List<HotelOrderInfo> modelList = new List<HotelOrderInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                HotelOrderInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = _repository.DataRowToModel(dt.Rows[n]);
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
            return _repository.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return _repository.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return _repository.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        public DataSet GetList(string openid, int hotelid, string type = "all")
        {
            DataSet result = null;
            if (type == "all")
            {
                result = _repository.GetListWithSql(openid, hotelid, "", " order by orderTime  desc ");
            }
            else if (type == "pay")
            {

                result = _repository.GetListWithSql(openid, hotelid, " and   orderStatus =3", " order by orderTime   desc");
            }
            else if (type == "refund")
            {
                result = _repository.GetListWithSql(openid, hotelid, " and   orderStatus =7", " order by orderTime  desc ");
            }


            return result;
        }

        public bool Updatehotel(HotelOrderInfo model)
        {
            return _repository.Updatehotel(model);
        }

        public bool Update(int dingdanid, string status)
        {
            return _repository.Update(dingdanid, status);
        }

        public bool Update(int dingdanid)
        {
            return _repository.Update(dingdanid);
        }

        /// <summary>
        /// 获取用户订单
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="wid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet GetUserOrderList(string openid, int wid, string type)
        {
            DataSet result = null;
            if (type == "all")
            {
                result = _repository.GetUserOrderList(openid, wid, "", " order by orderTime  desc ");
            }
            else if (type == "pay")
            {

                result = _repository.GetUserOrderList(openid, wid, " orderStatus =3", " order by orderTime desc ");
            }
            else if (type == "refund")
            {
                result = _repository.GetUserOrderList(openid, wid, " orderStatus =7", " order by orderTime  desc ");
            }


            return result;
        }


        /// <summary>
        /// 获取用户保存过的信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public HotelOrderInfo GetLastUserModel(string openid)
        {

            return _repository.GetLastUserModel(openid);
        }

        /// <summary>
        /// 支付完成后更新状态
        /// </summary>
        /// <param name="outTradeNo">订单号</param>
	    public void PaySuccess(string outTradeNo)
        {
            var model = this.GetModel(outTradeNo);
            if (model != null)
            {
                this.Update(model.id, HotelStatusManager.OrderStatus.Payed.StatusId.ToString());
            }

        }

        /// <summary>
        /// 根据订单号获取模型
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
	    private HotelOrderInfo GetModel(string outTradeNo)
        {
            return _repository.GetModel(outTradeNo);
        }

        /// <summary>
        /// 获取微信所必须的退单参数
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="hotelid"></param>
        /// <param name="dingdanId"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public DataSet GetWeChatRefundParams(int wid, int hotelid, int dingdanId, string refundCode, string modelName = "Hotel")
        {
            return _repository.GetWeChatRefundParams(wid, hotelid, dingdanId, refundCode, modelName);

        }

        /// <summary>
        /// 退单完成后改变订单状态
        /// </summary>
        /// <param name="refundCode">订单号</param>
	    public void RefundComplete(string outTradeNo)
        {
            var model = this.GetModel(outTradeNo);
            if (model != null)
            {
                this.Update(model.id, HotelStatusManager.OrderStatus.Refunded.StatusId.ToString());
            }
        }
    }
}
