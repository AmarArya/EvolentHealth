﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Routing;
using Ninject.Web.WebApi;

namespace EvolentHealth.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
