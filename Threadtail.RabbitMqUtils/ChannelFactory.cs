namespace Threadtail.RabbitMqUtils
{
    public static class ChannelFactory
    {
        public static IChannelWrapper CreateChannel()
        {
            return new ChannelWrapper();
        }
    }
}