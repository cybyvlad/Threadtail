namespace Threadtail.Server.RabbitMqUtils
{
    public static class ChannelFactory
    {
        public static ChannelWrapper CreateChannel()
        {
            return new ChannelWrapper();
        }
    }
}