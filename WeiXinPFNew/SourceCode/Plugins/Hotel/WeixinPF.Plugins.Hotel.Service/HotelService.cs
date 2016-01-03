using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using AutoMapper.Internal;
using NServiceBus;
using WeixinPF.Plugins.Hotel.Service.AutoMapper;
using WeixinPF.Common;

namespace WeixinPF.Hotel.Plugins.Service
{
    partial class HotelService : ServiceBase
    {
        //IBus bus;
        public static System.Collections.Generic.IDictionary<string, IBus> dictBus;
        public HotelService()
        {
            InitializeComponent();
            dictBus = new Dictionary<string, IBus>();
        }

        static void Main()
        {
            using (HotelService service = new HotelService())
            {
                if (Environment.UserInteractive)
                {
                    service.OnStart(null);

                    Console.WriteLine("Bus created and configured");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();

                    service.OnStop();

                    return;
                }
                Run(service);
            }
        }

        protected override void OnStart(string[] args)
        {
            LoadPlugins();
            //加载AutoMapper配置。
            AutoMapperConfiguration.Configure();
            Console.WriteLine("Service start");

        }



        public void LoadPlugins()
        {
            var files = new List<string>();

            files.AddRange(Directory.GetFiles(PathHelper.GetRunningFolder(), "*.Plugins.dll"));
            files.AddRange(Directory.GetFiles(PathHelper.GetRunningFolder(), "*.Plugins.Service.exe"));

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

        protected override void OnStop()
        {
            if (dictBus.Any())
            {
                foreach (var item in dictBus)
                {
                    item.Value.Dispose();
                }
            }
            
            Console.WriteLine("Service stop");
        }
    }
}
