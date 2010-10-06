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
            e.For<IChannelWrapper>().Use<ChannelWrapper>();
        }

    }
}