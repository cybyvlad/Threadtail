namespace Threadtail.Server.RabbitMqUtils
{
    public static class Settings
    {
        private static string _exchangeName;
        private static string _queueName;
        private static string _routingKey;

        public static string ExchangeName
        {
            get
            {
                if (_exchangeName == null)
                {
                    _exchangeName = "ThreadtailExchange";
                }
                return _exchangeName;
            }
        }

        public static string QueueName
        {
            get
            {
                if (_queueName == null)
                {
                    _queueName = "ThreadtailQueye";
                }
                return _queueName;
            }
        }

        public static string RoutingKey
        {
            get
            {
                if (_routingKey == null)
                {
                    _routingKey = "ThreadtailRoutingKey";
                }
                return _routingKey;
            }
        }
    }
}