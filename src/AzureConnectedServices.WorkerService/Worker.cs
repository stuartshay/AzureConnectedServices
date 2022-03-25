using AzureConnectedServices.Core.Configuration;
using Microsoft.Extensions.Options;

namespace AzureConnectedServices.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WorkerStateService _workerStateService;
        private readonly Settings _settings;

        public Worker(ILogger<Worker> logger, WorkerStateService workerStateService, IOptions<Settings> settings)
        {
            _logger = logger;
            _workerStateService = workerStateService;
            _settings = settings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _workerStateService.IsRunning = true;
                
                _logger.LogInformation("Worker iteration {0} running at: {time}", _workerStateService.Iteration, DateTimeOffset.Now);
                _logger.LogInformation("ServiceBusConnectionString {0}", _settings?.ServiceBusConnectionString);

                await Task.Delay(2500, stoppingToken);
                _workerStateService.Iteration++;
            }
            _workerStateService.IsRunning = false;
        }
    }
}
