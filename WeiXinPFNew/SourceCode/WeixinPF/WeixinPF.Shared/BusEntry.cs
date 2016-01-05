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
            var pathName = string.Empty;

            if (Directory.Exists(PathHelper.GetRunningFolder() + "bin"))
            {
                pathName = PathHelper.GetRunningFolder() + "bin";
            }
            else
            {
                pathName = PathHelper.GetRunningFolder();
            }

            files.AddRange(Directory.GetFiles(pathName, "*.Plugins.dll"));
            files.AddRange(Directory.GetFiles(pathName, "*.Plugins.Service.exe"));

            if (files.Any())
            {
                foreach (var assemblyFileName in files)
                {
                    var fileName = assemblyFileName.Split('\\').Last();
                    var busName = string.Empty;

                    if (fileName.Split('.').Contains("Service"))
                    {
                        busName = fileName.Split('.')[1].ToLower() + "service";
                    }
                    else
                    {
                        busName = fileName.Split('.')[1].ToLower();
                    }

                    if (dictBus.Keys.Contains(busName))
                    {
                        continue;
                    }

                    BusConfiguration busConfiguration = new BusConfiguration();

                    busConfiguration.PurgeOnStartup(true);
                    busConfiguration.ApplyCommonConfiguration();
                    busConfiguration.EndpointName(fileName.Substring(0, fileName.Length - 4));
                    
                    dictBus.Add(busName, NServiceBus.Bus.Create(busConfiguration).Start());
                }
            }
        }

        public static void Start()
        {
            LoadPlugins();
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
