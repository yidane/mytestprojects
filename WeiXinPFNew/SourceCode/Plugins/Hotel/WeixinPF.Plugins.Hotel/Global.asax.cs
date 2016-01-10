using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Threading;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Shared;

namespace WeixinPF.Hotel.Plugins
{
    public class Global : System.Web.HttpApplication
    {
        public static BusEntry Bus;

        protected void Application_Start(object sender, EventArgs e)
        {
            Bus = new BusEntry("WeixinPF.Hotel.Plugins");

            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }


        public override void Dispose()
        {
            Bus?.Dispose();

            base.Dispose();
        }

        
    }
}