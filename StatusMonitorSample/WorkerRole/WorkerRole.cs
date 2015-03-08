using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Owin.Hosting;
using Utilities;

namespace WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);
        private IDisposable _app = null;

        public override void Run()
        {
            Trace.TraceInformation("WorkerRole is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // Load the instrumentation key from Web.config
            Microsoft.ApplicationInsights.Extensibility
                .TelemetryConfiguration.Active.InstrumentationKey =
                CloudConfigurationManager.GetSetting("AppInsightsTelemetryKey");

            // Tag the telemetry with the current azure role id
            Microsoft.ApplicationInsights.Extensibility
                .TelemetryConfiguration.Active.ContextInitializers
                .Add(new AppInsightsCurrentRoleIdAsTagInitializer());


            var endpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["WorkerRoleEndpoint"];
            string baseUri = String.Format("{0}://{1}", endpoint.Protocol, endpoint.IPEndpoint);
            Trace.TraceInformation(string.Format("Starting OWIN ad {0}", baseUri), "Information");

            _app = WebApp.Start<Startup>(new StartOptions(baseUri));

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("WorkerRole has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole is stopping");

            if (_app != null)
            {
                _app.Dispose();
            }

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerRole has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                await Task.Delay(1000);
            }
        }
    }
}
