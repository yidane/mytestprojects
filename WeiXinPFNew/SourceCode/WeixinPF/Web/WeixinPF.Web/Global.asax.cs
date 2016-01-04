using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using NServiceBus;
using WeixinPF.Common;

namespace WeixinPF.Web
{
    public class Global : System.Web.HttpApplication
    {
        public static IDictionary<string, IBus> dictBus;

        protected void Application_Start(object sender, EventArgs e)
        {
            dictBus = new Dictionary<string, IBus>();
            LoadPlugins();
        }

        public void LoadPlugins()
        {
            var files = new List<string>();

            files.AddRange(Directory.GetFiles(PathHelper.GetRunningFolder() + "bin", "*.Plugins.dll"));
            files.AddRange(Directory.GetFiles(PathHelper.GetRunningFolder() + "bin", "*.Plugins.Service.exe"));

            if (files.Any())
            {
                foreach (var assemblyFileName in files)
                {
                    var fileName = assemblyFileName.Split('\\').Last();
                    BusConfiguration busConfiguration = new BusConfiguration();

                    busConfiguration.PurgeOnStartup(true);
                    busConfiguration.ApplyCommonConfiguration();
                    busConfiguration.EndpointName(fileName.Substring(0, fileName.Length - 4));

                    if (fileName.Split('.').Contains("Service"))
                    {
                        dictBus.Add(fileName.Split('.')[1].ToLower() + "service", NServiceBus.Bus.Create(busConfiguration).Start());
                    }
                    else
                    {
                        dictBus.Add(fileName.Split('.')[1].ToLower(), NServiceBus.Bus.Create(busConfiguration).Start());

                    }
                }
            }
        }
    }
}