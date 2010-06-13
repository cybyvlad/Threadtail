#region Using directives
using System.Text;

using Threadtail.Server.RabbitMqUtils;

#endregion

namespace Threadtail.Server.MessageBus
{
    public class MessageBusSender
    {
        public static void SendMessage(string rawUrl)
        {
            using (var channelWrapper = ChannelFactory.CreateChannel())
            {
                Send(rawUrl, channelWrapper);
            }
        }

        private static void Send(string message, ChannelWrapper channelWrapper)
        {
            var messageBodyBytes = Encoding.UTF8.GetBytes(message);
            channelWrapper.Channel.BasicPublish(Settings.ExchangeName, Settings.RoutingKey, null,
                                                messageBodyBytes);
        }
    }
}