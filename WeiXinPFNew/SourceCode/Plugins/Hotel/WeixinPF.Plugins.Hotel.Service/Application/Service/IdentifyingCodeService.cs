using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Plugins.Hotel.Service.Application.Repository;
using WeixinPF.Plugins.Hotel.Service.Infrastructure;
using WeixinPF.Plugins.Hotel.Service.Models;

namespace WeixinPF.Plugins.Hotel.Service.Application.Service
{
    public class IdentifyingCodeService
    {
        public static IdentifyingCodeInfo GetConfirmIdentifyingCodeInfo(int shopId, string identifyingCode, string moduleName, int wid)
        {
            
            if (shopId == 0 || string.IsNullOrEmpty(identifyingCode) || string.IsNullOrEmpty(moduleName))
            {
                return null;
            }

            using (var context = new HotelDbContext())
            {
                IIdentifyingCodeRepository repository = new IdentifyingCodeRepository(context); //改造方向：依赖注入，彻底去除对Infrastructure层的依赖

                return
                    repository.Get(
                        item =>
                        item.ShopId == shopId.ToString(CultureInfo.InvariantCulture)
                        && item.IdentifyingCode.Equals(identifyingCode)
                        && item.ModuleName.Equals(moduleName)
                        && item.Wid.Equals(wid)).FirstOrDefault();
            }
        }

        public static IdentifyingCodeInfo GetIdentifyingCodeInfoByIdentifyingCodeId(Guid identifyingCodeId, string moduleName, int wid)
        {
            if (identifyingCodeId == null || Guid.Empty.Equals(identifyingCodeId))
            {
                return null;
            }

            using (var context = new HotelDbContext())
            {
                IIdentifyingCodeRepository repository = new IdentifyingCodeRepository(context); //改造方向：依赖注入，彻底去除对Infrastructure层的依赖

                return
                    repository.Get(
                            item => item.IdentifyingCodeId.Equals(identifyingCodeId)
                                    && item.ModuleName.Equals(moduleName)
                                    && item.Wid.Equals(wid)).FirstOrDefault();
            }
        }

        public static bool MakeUseOfIdentifyingCode(Guid identifyingCodeId)
        {
            if (identifyingCodeId == null || Guid.Empty.Equals(identifyingCodeId))
            {
                return false;
            }

            using (var context = new HotelDbContext())
            {
                IIdentifyingCodeRepository repository = new IdentifyingCodeRepository(context); //改造方向：依赖注入，彻底去除对Infrastructure层的依赖

                repository.MakeUseOfIdentifyingCode(identifyingCodeId);
            }

            return true;
        }

        public static IList<IdentifyingCodeDetailSearchDTO> GetIdentifyingCodeDetailById(Guid identifyingCodeId, string moduleName)
        {
            if (Guid.Empty.Equals(identifyingCodeId) || string.IsNullOrEmpty(moduleName))
            {
                return null;
            }

            var identifyingCode = new IdentifyingCodeInfo(){IdentifyingCodeId = identifyingCodeId, ModuleName = moduleName};

            using (var context = new HotelDbContext())
            {
                IIdentifyingCodeRepository repository = new IdentifyingCodeRepository(context); //改造方向：依赖注入，彻底去除对Infrastructure层的依赖

                return repository.GetIdentifyingCodeDetailById(identifyingCode);
            }
        }
    }
}
