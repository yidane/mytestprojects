using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Messages.Command;
using WeixinPF.Plugins.Hotel.Service.Application.Service;

namespace WeixinPF.Plugins.Hotel.Service.Handler
{
    public class MakeUseOfIdentifyingCodeHandler : IHandleMessages<MakeUseOfIdentifyingCode>
    {
        private IBus bus;

        public MakeUseOfIdentifyingCodeHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(MakeUseOfIdentifyingCode message)
        {
            Thread.Sleep(2000);
            bus.Return(IdentifyingCodeService.MakeUseOfIdentifyingCode(message.IdentifyingCodeId)?1:0);
        }
    }
}
