using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace WebAPI.Configuration
{
    public static class WebAPIConfig
    {
        public static void Register(HttpConfiguration config)
        {
            IHttpRoute defaultRoute = config.Routes.CreateRoute("api/{controller}/{id}",
                new { id = RouteParameter.Optional }, null);

            config.Routes.Add("DefaultApi", defaultRoute);
        }
    }
}