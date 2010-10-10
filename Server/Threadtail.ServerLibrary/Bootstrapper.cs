using StructureMap;
using Threadtail.RabbitMqUtils;

namespace Threadtail.ServerLibrary
{
    public static class Bootstrapper
    {
        public static void InitializeStructureMap()
        {
            ObjectFactory.Initialize(InitializeInfrastructure);
        }

        private static void InitializeInfrastructure(IInitializationExpression e)
        {
            // Transient
            e.For<IHttpContextHandler>().Use<HttpContextHandler>();
            e.For<IChannelWrapper>().Use<ChannelWrapper>();
            
            // Singleton
            e.For<IMessageBusSender>().Singleton().Use<MessageBusSender>();
            e.For<IUrlBuilder>().Singleton().Use<UrlBuilder>();
            e.For<IJavaScriptTimeCalculator>().Singleton().Use<JavaScriptTimeCalculator>();

        }
    }
}