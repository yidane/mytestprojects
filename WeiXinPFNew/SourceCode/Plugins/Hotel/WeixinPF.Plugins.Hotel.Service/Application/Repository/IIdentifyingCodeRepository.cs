using System;
using System.Collections.Generic;
using WeixinPF.Application.BaseRepository;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Application.Repository
{
    public interface IIdentifyingCodeRepository:IRepository<IdentifyingCodeInfo>
    {
        void MakeUseOfIdentifyingCode(Guid identifyingCodeId);
        IList<IdentifyingCodeDetailSearchDTO> GetIdentifyingCodeDetailById(IdentifyingCodeInfo code);
    }
}