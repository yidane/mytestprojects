using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Plugins.Hotel.Service.Application.Service;

namespace WeixinPF.Plugins.Hotel.Service.Handler
{
    public class GetByIdentifyingCodeIdHandler:IHandleMessages<GetByIdnetifyingCodeIdRequest>
    {
        private IBus bus;

        public GetByIdentifyingCodeIdHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(GetByIdnetifyingCodeIdRequest message)
        {
            var info = IdentifyingCodeService.GetIdentifyingCodeInfoByIdentifyingCodeId(message.IdentifyingCodeId, message.ModuleName, message.Wid);

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
        }
    }
}
