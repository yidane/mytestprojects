using System.Threading;
using NServiceBus;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Messages.Command;

namespace WeixinPF.Hotel.Plugins.Service.Handler
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
