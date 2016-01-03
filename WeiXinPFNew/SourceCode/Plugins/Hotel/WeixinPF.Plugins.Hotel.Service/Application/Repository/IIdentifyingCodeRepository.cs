using System;
using System.Collections.Generic;
using WeixinPF.Application.BaseRepository;
using WeixinPF.Hotel.Plugins.Service.Models;

namespace WeixinPF.Hotel.Plugins.Service.Application.Repository
{
    public interface IIdentifyingCodeRepository:IRepository<IdentifyingCodeInfo>
    {
        void MakeUseOfIdentifyingCode(Guid identifyingCodeId);
        IList<IdentifyingCodeDetailSearchDTO> GetIdentifyingCodeDetailById(IdentifyingCodeInfo code);
    }
}