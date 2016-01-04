using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using WeixinPF.Common;

namespace WeixinPF.Shared
{
    public class BusEntry
    {
        public static IDictionary<string, IBus> dictBus = new Dictionary<string, IBus>();
        private static void LoadPlugins()
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

        public static void Start()
        {
            if (!dictBus.Any())
            {
                LoadPlugins();
            }            
        }

        public static void Dispose()
        {
            if (dictBus.Any())
            {
                foreach (var item in dictBus)
                {
                    item.Value.Dispose();
                }
            }
        }
    }
}
