namespace Threadtail.ServerLibrary
{
    public class HttpContextHandler : IHttpContextHandler
    {
        private readonly IMessageBusSender _messageBus;

        public HttpContextHandler(IThreadtailHttpContext context, IMessageBusSender messageBus, IUrlBuilder urlBuilder)
        {
            _messageBus = messageBus;
        }

        public void Process()
        {
            _messageBus.SendMessage("http://www.google.com");
        }
    }
}