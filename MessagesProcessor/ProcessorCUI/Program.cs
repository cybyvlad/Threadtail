#region Using directives

#endregion

#region Using directives
using System;

#endregion

namespace ProcessorCUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var notificationConsumer = new NotificationConsumer();
            Console.WriteLine("Finished");
            notificationConsumer.Dispose();
        }
    }
}