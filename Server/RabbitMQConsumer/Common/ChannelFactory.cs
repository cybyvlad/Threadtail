namespace Common
{
    public static class ChannelFactory
    {
        public static ChannelWrapper CreateChannel()
        {
            return new ChannelWrapper();
        }
    }
}