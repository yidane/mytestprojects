using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using NServiceBus;
using WeixinPF.Common;

namespace WeixinPF.Hotel.Plugins
{
    public class Global : System.Web.HttpApplication
    {
        public static IDictionary<string, IBus> dictBus;

        protected void Application_Start(object sender, EventArgs e)
        {
            dictBus = new Dictionary<string, IBus>();
            LoadPlugins();
            //BusConfiguration busConfiguration = new BusConfiguration();
            //busConfiguration.EndpointName("WeixinPF.Hotel.Plugins");
            //busConfiguration.PurgeOnStartup(true);
            //busConfiguration.ApplyCommonConfiguration();

            //Bus = NServiceBus.Bus.Create(busConfiguration).Start();
        }

        public void LoadPlugins()
        {
            var files = new List<string>();

            files.AddRange(Directory.GetFiles(PathHelper.GetRunningFolder() +"bin", "*.Plugins.dll"));
            files.AddRange(Directory.GetFiles(PathHelper.GetRunningFolder() +"bin", "*.Plugins.Service.exe"));

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
                        dictBus.Add(fileName.Split('.')[1].ToLower() +"service", NServiceBus.Bus.Create(busConfiguration).Start());
                    }
                    else
                    {
                        dictBus.Add(fileName.Split('.')[1].ToLower(), NServiceBus.Bus.Create(busConfiguration).Start());

                    }
                }
            }
        }


        public override void Dispose()
        {
            if (dictBus.Any())
            {
                foreach (var item in dictBus)
                {
                    item.Value.Dispose();
                }
            }

            base.Dispose();
        }
    }
}