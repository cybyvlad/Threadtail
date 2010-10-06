namespace Threadtail.ServerLibrary
{
    public class HttpContextHandler : IHttpContextHandler
    {
        private readonly IThreadtailHttpContext _context;
        private readonly IMessageBusSender _messageBus;
        private readonly IUrlBuilder _urlBuilder;

        public HttpContextHandler(IThreadtailHttpContext context, IMessageBusSender messageBus, IUrlBuilder urlBuilder)
        {
            _context = context;
            _messageBus = messageBus;
            _urlBuilder = urlBuilder;
        }

        public void Process()
        {
            var message = _urlBuilder.BuildUrl(_context);
            _messageBus.SendMessage(message);
        }
    }
}