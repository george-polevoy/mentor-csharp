namespace WorkersAndQueues;

public class Message
{
    public Message(string payload)
    {
        Payload = payload;
    }

    public string Payload { get; }
}