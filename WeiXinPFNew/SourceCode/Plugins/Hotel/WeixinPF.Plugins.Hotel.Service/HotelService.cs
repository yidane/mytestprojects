using System;
using System.ServiceProcess;
using NServiceBus;
using WeixinPF.Plugins.Hotel.Service.AutoMapper;

namespace WeixinPF.Hotel.Plugins.Service
{
    partial class HotelService : ServiceBase
    {
        IBus bus;
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
            //加载AutoMapper配置。
            AutoMapperConfiguration.Configure();

            BusConfiguration busConfiguration = new BusConfiguration();            

            busConfiguration.EndpointName("WeixinPF.Hotel.Plugins.Service.HotelService");
            busConfiguration.PurgeOnStartup(true);
            busConfiguration.ApplyCommonConfiguration();

            bus = Bus.Create(busConfiguration).Start();

            Console.WriteLine("Service start");

        }

        protected override void OnStop()
        {
            if (bus != null)
            {
                bus.Dispose();
            }
            Console.WriteLine("Service stop");
        }
    }
}
