using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using NServiceBus;
using WeixinPF.Common.Helper;
using WeixinPF.Messages.Command;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Plugins.Hotel.Functoin.Service
{
    /// <summary>
    /// HotelService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class HotelService : System.Web.Services.WebService
    {

        [WebMethod]
        public void HelloWorld(int wid, int shopId)
        {
            ShowAllRoomResponse res = null;

            IAsyncResult responseData = 
                Global.Bus
                .Send("WeixinPF.Plugins.Hotel.Service.HotelService", new ShowAllRoom() {ShopId = shopId, Wid = wid})
                .Register(response =>
                {
                    CompletionResult localResult = (CompletionResult)response.AsyncState;
                    res = localResult.Messages[0] as ShowAllRoomResponse;
                }, this);

            WaitHandle asyncWaitHandle = responseData.AsyncWaitHandle;
            asyncWaitHandle.WaitOne(10000);

            if (responseData.IsCompleted)
            {
                Context.Response.Write(string.Format("{{'RoomName':'{0}','Price':'{1}'}}", res.RoomName, res.Price));
                Context.Response.End();
            }
            
        }

        /// <summary>
        /// 获取酒店基本信息
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="openid"></param>
        [WebMethod]
        public void GetHotelInfo(int wid,string openid)
        {
            var hotelDto = new HotelDto()
            {
                Id = 1,
                Tel = "18311300760",
                Address = "神仙湾",
                JieShao = "七天商家介绍七天商家介绍七天商家介绍七天商家介绍七天商家介绍七天商家介绍七天商家介绍七天商家介绍七天商家介绍七天商家介绍七天商家介绍七天商家介绍"
            };
            Context.Response.Write(JSONHelper.Serialize(hotelDto,true));
            Context.Response.End();
        }

        [WebMethod]
        public void GetJson()
        {

            Context.Response.Write("{\"firstName\":\"Brett\",\"lastName\":\"McLaughlin\",\"email\":\"aaaa\"}");
            Context.Response.End();
        }

        [WebMethod]
        public void GetListJson()
        {
            Context.Response.Write("{\"orders\":[{\"firstName\":\"Brett\",\"lastName\":\"McLaughlin\",\"email\":\"aaaa\"},{\"firstName\":\"Jason\",\"lastName\":\"Hunter\",\"email\":\"bbbb\"},{\"firstName\":\"Elliotte\",\"lastName\":\"Harold\",\"email\":\"cccc\"}]}");
            Context.Response.End();
        }
    }

    public class HotelDto
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public string Tel { get; set; }
        public string JieShao { get; set; }
    }
}
