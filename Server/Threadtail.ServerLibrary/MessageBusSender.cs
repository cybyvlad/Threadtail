using System.Text;
using StructureMap;
using Threadtail.RabbitMqUtils;

namespace Threadtail.ServerLibrary
{
    public class MessageBusSender : IMessageBusSender
    {
        private void Send(string message, IChannelWrapper channelWrapper)
        {
            var messageBodyBytes = Encoding.UTF8.GetBytes(message);
            channelWrapper.Channel.BasicPublish(Settings.ExchangeName, Settings.RoutingKey, null,
                                                messageBodyBytes);
        }

        public void SendMessage(string rawUrl)
        {
            using (var channelWrapper = ObjectFactory.GetInstance<IChannelWrapper>())
            {
                Send(rawUrl, channelWrapper);
            }
        }
    }
}