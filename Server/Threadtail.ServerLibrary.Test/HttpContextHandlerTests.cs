#region Using directives

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
            var messageBusSenderMock = new Mock<IMessageBusSender>();

            const string url = "/x.jpg";
            var contextMock = Mock.Of<IThreadtailHttpContext>(httpContext => httpContext.Url == url &&
                                                                             httpContext.BrowserName == "Firefox");

            ObjectFactory.Initialize(delegate(IInitializationExpression e)
                                         {
                                             e.For<IUrlBuilder>().Use<UrlBuilder>();
                                             e.For<IThreadtailHttpContext>().Use(contextMock);
                                             e.For<IHttpContextHandler>().Use<HttpContextHandler>();
                                             e.For<IMessageBusSender>().Use(messageBusSenderMock.Object);
                                             e.For<IJavaScriptTimeCalculator>().Use<JavaScriptTimeCalculator>();
                                         });
            var handler = ObjectFactory.GetInstance<IHttpContextHandler>();

            handler.Process();

            messageBusSenderMock.Verify(sender => sender.SendMessage(("/x.jpg&enx1=Browser&evx1=Firefox&tx1=0")));
        }
    }
}