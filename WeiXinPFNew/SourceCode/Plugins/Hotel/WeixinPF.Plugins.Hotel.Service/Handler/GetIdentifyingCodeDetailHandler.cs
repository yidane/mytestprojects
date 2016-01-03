using System;
using System.Collections.Generic;
using AutoMapper;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.Application.Service;
using WeixinPF.Hotel.Plugins.Service.Models;
using WeixinPF.Messages.RequestResponse;

namespace WeixinPF.Hotel.Plugins.Service.Handler
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
