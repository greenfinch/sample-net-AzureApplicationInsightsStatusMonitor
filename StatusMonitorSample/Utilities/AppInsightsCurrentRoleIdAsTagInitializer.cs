using System.Text.RegularExpressions;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Utilities
{
    public class AppInsightsCurrentRoleIdAsTagInitializer : Microsoft.ApplicationInsights.Extensibility.IContextInitializer
    {
        public void Initialize(TelemetryContext context)
        {
            context.Properties["Greenfinch - RoleName"] = RoleEnvironment.CurrentRoleInstance.Role.Name;
            context.Properties["Greenfinch - RoleInstanceId"] = InstanceId;
        }

        private string InstanceId
        {
            get
            {
                var instanceId = Regex.Match(RoleEnvironment.CurrentRoleInstance.Id, "\\d+$", RegexOptions.Compiled).Value;
                return string.IsNullOrWhiteSpace(instanceId)
                    ? "unable to get instance id"
                    : instanceId;
            }
        }
    }
}
