using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Data.Entity;

namespace ELMS_WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }
        //protected void Application_BeginRequest()
        //{
        //    string[] allowedOrigin = new string[] { "http://localhost:4200", "http://localhost:2052" };
        //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", allowedOrigin);
        //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET,POST");
        //}
        protected void Application_BeginRequest()
        {
            string[] allowedOrigin = new string[] { "http://localhost:4200", "http://localhost:2052" };
            var origin = HttpContext.Current.Request.Headers["Origin"];
            if (origin != null && allowedOrigin.Contains(origin))
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", origin);
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET,POST");
            }
        }
    }
}
