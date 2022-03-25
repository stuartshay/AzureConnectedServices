namespace AzureConnectedServices.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WorkerStateService _workerStateService;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, WorkerStateService workerStateService, IConfiguration configuration)
        {
            _logger = logger;
            _workerStateService = workerStateService;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _workerStateService.IsRunning = true;

                string key = "AzureConnectedServices:Settings:ServiceBusConnectionString";
                string message = _configuration[key];

                _logger.LogInformation("Worker iteration {0} running at: {time}", _workerStateService.Iteration, DateTimeOffset.Now);
                _logger.LogInformation("ServiceBusConnectionString {0}", message);

                await Task.Delay(2500, stoppingToken);
                _workerStateService.Iteration++;
            }
            _workerStateService.IsRunning = false;
        }
    }
}
