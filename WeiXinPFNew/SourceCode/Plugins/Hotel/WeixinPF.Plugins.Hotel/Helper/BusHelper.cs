using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeixinPF.Hotel.Plugins.Helper
{
    /// <summary>
    /// 调用bus服务帮助类
    /// </summary>
    public  class BusHelper
    {
        public static TResult Send<TResult>( dynamic requestMessage,string serviceAddress= Constants.HotelServiceAddress)
            where TResult : class
        {
            var result = Global.Bus.Send<TResult>(serviceAddress, requestMessage);
            if (!result.IsSuccess)
            {
                throw new Exception(result.Message);
            }
            return result.Data;
        }
    }
}