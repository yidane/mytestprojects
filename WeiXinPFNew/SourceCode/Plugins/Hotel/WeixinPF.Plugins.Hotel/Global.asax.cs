using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using NServiceBus;

namespace WeixinPF.Plugins.Hotel
{
    public class Global : System.Web.HttpApplication
    {
        public static IBus Bus;
        
        protected void Application_Start(object sender, EventArgs e)
        {
//            BusConfiguration busConfiguration = new BusConfiguration();
//            busConfiguration.EndpointName("WeixinPF.Plugins.Hotel");
//            busConfiguration.PurgeOnStartup(true);
//            busConfiguration.ApplyCommonConfiguration();
//
//            Bus = NServiceBus.Bus.Create(busConfiguration).Start();
        }

        public override void Dispose()
        {
            if (Bus != null)
            {
                Bus.Dispose();
            }
            base.Dispose();
        }
    }
}