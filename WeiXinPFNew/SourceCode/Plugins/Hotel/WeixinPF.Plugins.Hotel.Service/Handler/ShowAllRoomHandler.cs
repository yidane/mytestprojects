using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Messages.Command;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Plugins.Hotel.Service.Handler
{
    public class ShowAllRoomHandler:IHandleMessages<ShowAllRoom>
    {
        IBus bus;

        public ShowAllRoomHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(ShowAllRoom message)
        {
            bus.Reply(new ShowAllRoomResponse() {RoomName = "大床房", Price = decimal.Parse("234.5")});
        }
    }
}
