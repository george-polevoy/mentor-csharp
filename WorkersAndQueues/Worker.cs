using System.Threading.Channels;

namespace WorkersAndQueues;

public class Worker : BackgroundService, IMessageConsumer
{
    private readonly ILogger<Worker> _logger;
    private readonly Channel<Message> _channel = Channel.CreateBounded<Message>(new BoundedChannelOptions(10)
    {
        FullMode = BoundedChannelFullMode.DropOldest
    });

public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var msg in _channel.Reader.ReadAllAsync(stoppingToken))
        {
            _logger.LogInformation($"Worker received msg: {msg.Payload}");
        }
    }

    public async Task Post(Message item, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(item, cancellationToken);
    }
}