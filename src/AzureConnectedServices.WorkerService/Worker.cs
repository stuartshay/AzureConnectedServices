using Azure.Messaging.ServiceBus;
using AzureConnectedServices.Models;
using System.Text.Json;

namespace AzureConnectedServices.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WorkerStateService _workerStateService;
        private readonly IConfiguration _configuration;

        private ServiceBusClient _client;
        private ServiceBusProcessor _processor;

        public Worker(ILogger<Worker> logger, WorkerStateService workerStateService, IConfiguration configuration)
        {
            _logger = logger;
            _workerStateService = workerStateService;
            _configuration = configuration;

            string key = "AzureConnectedServices:Settings:ServiceBusConnectionString";
            string connectionString = _configuration[key];

            _client = new ServiceBusClient(connectionString);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _workerStateService.IsRunning = true;

                string key = "AzureConnectedServices:Settings:ServiceBusConnectionString";
                string connectionString = _configuration[key];

                _logger.LogInformation("Worker iteration {0} running at: {time}", _workerStateService.Iteration, DateTimeOffset.Now);
                _logger.LogInformation("ServiceBusConnectionString {0}", connectionString);

                _processor = _client.CreateProcessor("weatherrequest", new ServiceBusProcessorOptions());
                _processor.ProcessMessageAsync += MessageHandler;
                _processor.ProcessErrorAsync += ErrorHandler;

                 await _processor.StartProcessingAsync();

                 await Task.Delay(2500, stoppingToken);
                 _workerStateService.Iteration++;
            }

          _workerStateService.IsRunning = false;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _processor.StopProcessingAsync();
            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _processor.DisposeAsync().GetAwaiter().GetResult();
            _client.DisposeAsync().GetAwaiter().GetResult();
        }

        private static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            var message = JsonSerializer.Deserialize<WeatherRequestModel>(body);

            Console.WriteLine($"Received: {message?.ToString()}");

            await args.CompleteMessageAsync(args.Message);
        }

        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

    }
}
