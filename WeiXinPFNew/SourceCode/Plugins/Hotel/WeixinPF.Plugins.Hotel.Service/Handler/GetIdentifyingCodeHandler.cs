using System;
using NServiceBus;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
{
    public class GetIdentifyingCodeHandler:IHandleMessages<GetIdentifyingCodeRequest>
    {
        private IBus bus;

        public GetIdentifyingCodeHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(GetIdentifyingCodeRequest message)
        {
            Console.WriteLine("Receive GetIdentifyingCodeRequest MessageNumber is :{0}",message.Number);
            var info = IdentifyingCodeService.GetConfirmIdentifyingCodeInfo(message.ShopId, message.Number,
                message.ModuleName, message.Wid);

            var response = new GetIdentifyingCodeResponse()
            {
                CreateTime = info.CreateTime,
                IdentifyingCode = info.IdentifyingCode,
                IdentifyingCodeId = info.IdentifyingCodeId,
                ModifyTime = info.ModifyTime,
                ModuleName = info.ModuleName,
                OrderCode = info.OrderCode,
                OrderId = info.OrderId,
                ProductCode = info.ProductCode,
                ProductId = info.ProductId,
                ShopId = info.ShopId,
                Wid = info.Wid,
                Status = info.Status
            };

            bus.Reply(response);
            Console.WriteLine("Response GetIdentifyingCodeResponse Message");
        }
    }
}
