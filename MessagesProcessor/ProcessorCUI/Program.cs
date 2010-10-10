#region Using directives

#endregion

#region Using directives
using System;
using StructureMap;

#endregion

namespace ProcessorCUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Bootstrapper.InitializeStructureMap();

            var notificationConsumer = ObjectFactory.GetInstance<NotificationConsumer>();
            
            Console.WriteLine("Finished");
            notificationConsumer.Dispose();
        }
    }
}