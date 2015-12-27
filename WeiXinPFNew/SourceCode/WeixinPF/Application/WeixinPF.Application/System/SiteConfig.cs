using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeixinPF.Application.System.Interface;
using WeixinPF.Common;
using WeixinPF.Common.Enum;
using WeixinPF.Common.Helper;
using WeixinPF.Model.System;

namespace WeixinPF.Application.System
{
    public class SiteConfig
    {
        public SiteConfig(ISiteConfigRepository repository)
        {
            this._repository = repository;
        }

        private readonly ISiteConfigRepository _repository;//iteconfig();

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public siteconfig loadConfig()
        {
            var model = CacheHelper.Get<siteconfig>(MXKeys.CACHE_SITE_CONFIG);
            if (model == null)
            {
                CacheHelper.Insert(MXKeys.CACHE_SITE_CONFIG, this._repository.loadConfig(Utils.GetXmlMapPath(MXKeys.FILE_SITE_XML_CONFING)),
                    Utils.GetXmlMapPath(MXKeys.FILE_SITE_XML_CONFING));
                model = CacheHelper.Get<siteconfig>(MXKeys.CACHE_SITE_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public siteconfig saveConifg(siteconfig model)
        {
            return this._repository.saveConifg(model, Utils.GetXmlMapPath(MXKeys.FILE_SITE_XML_CONFING));
        }
    }
}
