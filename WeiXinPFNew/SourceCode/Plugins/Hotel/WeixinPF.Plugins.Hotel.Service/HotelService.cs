using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace WeixinPF.Plugins.Hotel.Service
{
    partial class HotelService : ServiceBase
    {
        IBus bus;
        public HotelService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            BusConfiguration busConfiguration = new BusConfiguration();            

            busConfiguration.EndpointName("WeixinPF.Plugins.Hotel.Service.HotelService");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.EnableInstallers();
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
