#region Using directives
using StructureMap;

using Threadtail.ServerLibrary;

#endregion

namespace Threadtail.Server.WebApp.App_Code
{
    public static class Bootstrapper
    {
        public static void InitializeStructureMap()
        {
            ObjectFactory.Initialize(InitializeInfrastructure);
        }

        private static void InitializeInfrastructure(IInitializationExpression e)
        {
            e.For<IMessageBusSender>().Use<MessageBusSender>();
        }
    }
}