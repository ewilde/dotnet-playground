using System;
using System.Web.Http;
using Owin;

namespace Reporting.Minion
{
    public class Minion
    {
        public static event EventHandler<EventArgs> Start;

        public static event EventHandler<EventArgs> Stop;

        public static event EventHandler<ChangeMessageSizeEventArgs> ChangeMessageSize;

        public static void OnChangeMessageSize(ChangeMessageSizeEventArgs args)
        {
            var handler = ChangeMessageSize;
            if (handler != null) handler(null, args);
        }

        public static void OnStart()
        {
            var handler = Start;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        public static void OnStop()
        {
            var handler = Stop;
            if (handler != null) handler(null, EventArgs.Empty);
        }

        // This code configures Web API. The Minion class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();

            config.EnableSystemDiagnosticsTracing();

            // Attribute routing.
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }  
    }
}