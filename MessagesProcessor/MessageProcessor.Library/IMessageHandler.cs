namespace MessageProcessor.Library
{
    public interface IMessageHandler
    {
        void Process(string message);
    }
}