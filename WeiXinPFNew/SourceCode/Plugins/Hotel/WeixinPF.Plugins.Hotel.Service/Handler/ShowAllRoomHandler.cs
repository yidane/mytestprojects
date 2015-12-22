using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Messages.Command;

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
            Console.WriteLine("process ShowAllRoom");
        }
    }
}
