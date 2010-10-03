using System.Text;

using Threadtail.RabbitMqUtils;

namespace Threadtail.ServerLibrary
{
    public class MessageBusSender : IMessageBusSender
    {
        #region Methods

        #region Privates

        #region Send
        private void Send(string message, IChannelWrapper channelWrapper)
        {
            var messageBodyBytes = Encoding.UTF8.GetBytes(message);
            channelWrapper.Channel.BasicPublish(Settings.ExchangeName, Settings.RoutingKey, null,
                                                messageBodyBytes);
        }
        #endregion

        #endregion

        #region Publics

        #region SendMessage
        public void SendMessage(string rawUrl)
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