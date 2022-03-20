using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;
using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using AzureConnectedServices.Core.Configuration;

namespace AzureConnectedServices.Core.HealthChecks
{
    /// <summary>
    /// Version Health Check
    /// </summary>
    public class VersionHealthCheck : IHealthCheck
    {
        private readonly string _dnsHostName;

        private readonly string _applicationVersionNumber;

        private readonly DateTime _applicationBuildDate;

        private readonly string _environment;

        private readonly string _endpointsAppConfig;

        private readonly string _osNameAndVersion;

        private readonly ILogger<VersionHealthCheck> _logger;

        private readonly string _weatherRequestQueue;

        private readonly string _serviceBusConnectionString;

        public VersionHealthCheck(ILogger<VersionHealthCheck> logger, IOptionsSnapshot<Settings> settings)
        {
            _applicationVersionNumber = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();
            _applicationBuildDate = GetAssemblyLastModifiedDate();
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _endpointsAppConfig = Environment.GetEnvironmentVariable("ENDPOINTS_APPCONFIG");
            _weatherRequestQueue = settings?.Value?.WeatherRequestQueueUrl;
            _serviceBusConnectionString = settings?.Value?.ServiceBusConnectionString;
            _dnsHostName = Dns.GetHostName();
            _osNameAndVersion = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            _logger = logger;
        }

        /// <summary>
        /// Application Version Health Check
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var data = new Dictionary<string, object>
            {
                {"BuildDate", _applicationBuildDate},
                {"BuildVersion", _applicationVersionNumber},
                {"DNS HostName", _dnsHostName},
                {"Environment", _environment},
                {"App Config Enpoint", _endpointsAppConfig},
                {"OsNameAndVersion", _osNameAndVersion},
                {"WeatherRequestQueue", _weatherRequestQueue},
                {"ServiceBusConnectionString", _serviceBusConnectionString },
            };

            var healthStatus = !string.IsNullOrEmpty(_applicationVersionNumber) ? HealthStatus.Healthy : HealthStatus.Degraded;
            var healthCheckResult = new HealthCheckResult(healthStatus, _applicationVersionNumber, null, data);

            return Task.FromResult(healthCheckResult);
        }

        private static DateTime GetAssemblyLastModifiedDate()
        {
            FileInfo fileInfo = new FileInfo(Assembly.GetEntryAssembly()?.Location ?? string.Empty);
            DateTime lastModified = fileInfo.LastWriteTime;

            return lastModified.ToUniversalTime();
        }
    }
}
