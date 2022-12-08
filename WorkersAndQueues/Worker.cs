using System.Threading.Channels;

namespace WorkersAndQueues;

public class Worker : BackgroundService, IMessageConsumer
{
    private readonly ILogger<Worker> _logger;

    private readonly Channel<Message> _channel = Channel.CreateBounded<Message>(10);

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var msg in _channel.Reader.ReadAllAsync(stoppingToken))
        {
            _logger.LogInformation($"Worker received msg: {msg.Payload}");
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        }
    }

    public async Task Post(Message item, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(item, cancellationToken);
    }
}