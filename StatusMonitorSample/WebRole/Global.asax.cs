using System.Web;
using System.Web.Routing;

namespace WebRole
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Microsoft.ApplicationInsights.Extensibility
                .TelemetryConfiguration.Active.InstrumentationKey =
                System.Web.Configuration.WebConfigurationManager.AppSettings["AppInsightsTelemetryKey"];
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
