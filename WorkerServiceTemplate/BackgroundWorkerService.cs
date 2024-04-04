public class BackgroundWorkerService : BackgroundService
{
    public BackgroundWorkerService(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger<BackgroundWorkerService>();
    }

    public ILogger Logger { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("BackgroundWorkerService is starting.");

        stoppingToken.Register(() => Logger.LogInformation("BackgroundWorkerService is stopping."));

        while (!stoppingToken.IsCancellationRequested)
        {
            Logger.LogInformation("BackgroundWorkerService is doing background work.");

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }

        Logger.LogInformation("BackgroundWorkerService has stopped.");
    }
}