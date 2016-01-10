using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using AutoMapper.Internal;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Hotel.Plugins.Service.AutoMapper;
using WeixinPF.Shared;

namespace WeixinPF.Hotel.Plugins.Service
{
    partial class HotelService : ServiceBase
    {
        //IBus bus;
        public HotelService()
        {
            InitializeComponent();
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
            var bus = new BusEntry("WeixinPF.Hotel.Plugins.Service");
            //加载AutoMapper配置。
            //AutoMapperConfiguration.Configure();
            Console.WriteLine("Service start");

        }

        protected override void OnStop()
        {
            //BusEntry.Dispose();
            base.Dispose();
            
            Console.WriteLine("Service stop");
        }
    }
}
