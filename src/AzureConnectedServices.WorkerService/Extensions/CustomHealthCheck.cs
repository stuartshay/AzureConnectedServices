using System.Net;
using TinyHealthCheck.HealthChecks;
using TinyHealthCheck.Models;

namespace AzureConnectedServices.WorkerService.Extensions
{
    public class CustomHealthCheck : IHealthCheck
    {
        private readonly ILogger<CustomHealthCheck> _logger;
        private readonly WorkerStateService _workerStateService;

        public CustomHealthCheck(ILogger<CustomHealthCheck> logger, WorkerStateService workerStateService)
        {
            _logger = logger;
            _workerStateService = workerStateService;
        }

        public async Task<IHealthCheckResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("This is an example of accessing the DI containers for logging. You can access any service that is registered");

            if (_workerStateService.IsRunning)
                return new JsonHealthCheckResult(
                new
                {
                  Status = "Healthy!",
                  Iteration = _workerStateService.Iteration,
                  IsServiceRunning = _workerStateService.IsRunning,
                }, HttpStatusCode.OK);

                return new JsonHealthCheckResult(
                new
                {
                    Status = "Unhealthy!",
                    Iteration = _workerStateService.Iteration,
                    IsServiceRunning = _workerStateService.IsRunning,
                    ErrorMessage = "We went over 10 iterations, so the service worker quit!"
                }, HttpStatusCode.InternalServerError);
        }
    }
}
