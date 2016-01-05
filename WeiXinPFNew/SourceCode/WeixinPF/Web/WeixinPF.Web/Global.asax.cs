using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using NServiceBus;
using WeixinPF.Common;
using WeixinPF.Shared;

namespace WeixinPF.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            BusEntry.Start();
        }

        public override void Dispose()
        {
            //BusEntry.Dispose();

            base.Dispose();
        }
    }
}