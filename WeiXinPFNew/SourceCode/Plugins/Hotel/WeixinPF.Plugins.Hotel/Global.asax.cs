using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Shared;

namespace WeixinPF.Hotel.Plugins
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            BusEntry.Start();
            //BusConfiguration busConfiguration = new BusConfiguration();
            //busConfiguration.EndpointName("WeixinPF.Hotel.Plugins");
            //busConfiguration.PurgeOnStartup(true);
            //busConfiguration.ApplyCommonConfiguration();

            //Bus = NServiceBus.Bus.Create(busConfiguration).Start();
        }


        public override void Dispose()
        {
            BusEntry.Dispose();

            base.Dispose();
        }
    }
}