using WeixinPF.Model.Common;

namespace WeixinPF.Application.System.Interface
{
    public interface ISiteConfigRepository
    {
        siteconfig loadConfig(string configFilePath);

        siteconfig saveConifg(siteconfig model, string configFilePath);
    }
}