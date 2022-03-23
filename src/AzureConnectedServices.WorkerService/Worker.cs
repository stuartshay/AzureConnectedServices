namespace AzureConnectedServices.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WorkerStateService _workerStateService;

        public Worker(ILogger<Worker> logger, WorkerStateService workerStateService)
        {
            _logger = logger;
            _workerStateService = workerStateService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _workerStateService.IsRunning = true;
                _logger.LogInformation("Worker iteration {0} running at: {time}", _workerStateService.Iteration, DateTimeOffset.Now);
                await Task.Delay(2500, stoppingToken);
                _workerStateService.Iteration++;
            }
            _workerStateService.IsRunning = false;
        }
    }
}
