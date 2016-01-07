using System.Web.Services;
using WeixinPF.Common.Helper;

namespace WeixinPF.Common.Extension
{
    public static class WebServiceExtension
    {
        /// <summary>
        /// 输出Json
        /// </summary>
        /// <param name="service"></param>
        /// <param name="data"></param>
        /// <param name="dateTimeFormat">时间序列化格式</param>
        public static void WriteJson(this WebService service, object data, string dateTimeFormat = "yyyy-MM-dd")
        {
            var context = service.Context;

            context.Response.ContentType = "application/json";
            context.Response.Write(JSONHelper.Serialize(data, dateTimeFormat));
//            context.Response.End();
        }
    }
}

