using System.Security.Cryptography;

namespace WorkersAndQueues;

public class Producer : BackgroundService
{
    private readonly IMessageConsumer _consumer;
    private readonly ILogger<Producer> _logger;

    public Producer(IMessageConsumer consumer, ILogger<Producer> logger)
    {
        _consumer = consumer;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Producer running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
            await _consumer.Post(new Message($"Hello {RandomNumberGenerator.GetInt32(10) + 1} times!"), stoppingToken);
        }
    }
}