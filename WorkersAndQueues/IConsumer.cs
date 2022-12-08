namespace WorkersAndQueues;

public interface IMessageConsumer
{
    Task Post(Message item, CancellationToken cancellationToken);
}