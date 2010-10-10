using MessageProcessor.Library;
using StructureMap;
using Threadtail.RabbitMqUtils;

namespace ProcessorCUI
{
    public class Bootstrapper
    {
        public static void InitializeStructureMap()
        {
            ObjectFactory.Initialize(InitializeInfrastructure);
        }

        private static void InitializeInfrastructure(IInitializationExpression e)
        {
            e.For<IMessageHandler>().Use<MessageHandler>();
            e.For<IChannelWrapper>().Use<ChannelWrapper>();
            e.For<IRuleLoader>().Use<RuleLoader>();
            e.For<IRuleEngine>().Use<RuleEngine>();
            e.For<NotificationConsumer>().Use<NotificationConsumer>();
        }

    }
}