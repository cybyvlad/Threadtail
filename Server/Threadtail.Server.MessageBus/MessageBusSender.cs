#region Using directives
using System.Text;

using Threadtail.RabbitMqUtils;

#endregion

namespace Threadtail.Server.MessageBus
{
    public static class MessageBusSender
    {
        #region Methods

        #region Privates

        #region Send
        private static void Send(string message, IChannelWrapper channelWrapper)
        {
            var messageBodyBytes = Encoding.UTF8.GetBytes(message);
            channelWrapper.Channel.BasicPublish(Settings.ExchangeName, Settings.RoutingKey, null,
                                                messageBodyBytes);
        }
        #endregion

        #endregion

        #region Publics

        #region SendMessage
        public static void SendMessage(string rawUrl)
        {
            using (var channelWrapper = ChannelFactory.CreateChannel())
            {
                Send(rawUrl, channelWrapper);
            }
        }
        #endregion

        #endregion

        #endregion
    }
}