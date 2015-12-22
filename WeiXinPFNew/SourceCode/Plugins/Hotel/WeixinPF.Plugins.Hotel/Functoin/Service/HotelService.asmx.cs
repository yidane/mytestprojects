using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using NServiceBus;
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
    }
}
