using System.Web;
using System.Web.Routing;
using Utilities;

namespace WebRole
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Load the instrumentation key from Web.config
            Microsoft.ApplicationInsights.Extensibility
                .TelemetryConfiguration.Active.InstrumentationKey =
                System.Web.Configuration.WebConfigurationManager.AppSettings["AppInsightsTelemetryKey"];
            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
