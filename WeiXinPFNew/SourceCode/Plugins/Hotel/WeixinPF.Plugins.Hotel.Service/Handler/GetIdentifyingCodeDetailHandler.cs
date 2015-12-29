using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Messages.RequestResponse;
using WeixinPF.Plugins.Hotel.Service.Application.Service;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Handler
{
    public class GetIdentifyingCodeDetailHandler:IHandleMessages<GetIdentifyingCodeDetailRequest>
    {
        private IBus bus;

        public GetIdentifyingCodeDetailHandler(IBus bus)
        {
            this.bus = bus;
            Mapper.CreateMap<IdentifyingCodeDetailSearchDTO, IdentifyingCodeDetailEntity>();
        }

        public void Handle(GetIdentifyingCodeDetailRequest message)
        {
            var obj = IdentifyingCodeService.GetIdentifyingCodeDetailById(message.IdentifyingCodeId, message.ModuleName);
            List<IdentifyingCodeDetailEntity> response = null;
            response = obj.MapTo<List<IdentifyingCodeDetailEntity>>();

            try
            {
                bus.Reply(new GetIdentifyingCodeDetailResponse() {Details = response});
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }
    }
}
