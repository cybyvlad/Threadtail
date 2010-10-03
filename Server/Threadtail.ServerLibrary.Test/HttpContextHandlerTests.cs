#region Using directives
using System.Web;

using Moq;

using NUnit.Framework;

using StructureMap;

#endregion

namespace Threadtail.ServerLibrary.Test
{
    [TestFixture]
    public class HttpContextHandlerTests
    {
        [Test]
        public void Process_WhenTheContextContainsARawUrl_TheUrlIsSentToTheQueue()
        {
            const string url = "http://www.google.com";

            var ctx = new HttpContext(new HttpRequest("", url, ""), new HttpResponse(null));

            var messageBusSenderMock = new Mock<IMessageBusSender>();

            var context = new ThreadtailHttpContext(ctx);

            ObjectFactory.Initialize(delegate(IInitializationExpression e)
                                         {
                                             e.For<IUrlBuilder>().Use<UrlBuilder>();
                                             e.For<IThreadtailHttpContext>().Use(context);
                                             e.For<IHttpContextHandler>().Use<HttpContextHandler>();
                                             e.For<IMessageBusSender>().Use(messageBusSenderMock.Object);
                                             e.For<IJavaScriptTimeCalculator>().Use<JavaScriptTimeCalculator>();
                                         });
            var handler = ObjectFactory.GetInstance<IHttpContextHandler>();

            handler.Process();

            messageBusSenderMock.Verify(sender => sender.SendMessage(url));
        }
    }
}