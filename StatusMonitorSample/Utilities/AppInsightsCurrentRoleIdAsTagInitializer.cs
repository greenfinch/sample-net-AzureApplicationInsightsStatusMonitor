using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Utilities
{
    public class AppInsightsCurrentRoleIdAsTagInitializer : Microsoft.ApplicationInsights.Extensibility.IContextInitializer
    {
        public void Initialize(TelemetryContext context)
        {
            context.Properties["tags"] = RoleEnvironment.CurrentRoleInstance.Id;
        }
    }
}
