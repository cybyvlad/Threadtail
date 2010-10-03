#region Using directives
using Moq;

using NUnit.Framework;

using StructureMap;

#endregion

namespace Threadtail.ServerLibrary.Test
{
    [TestFixture]
    public class UrlBuilderTest
    {
        [Test]
        public void BuildUrl_WhenContextContainsUrl_TheResultContainsTheUrl()
        {
            var threadtailHttpContext =
                Mock.Of<IThreadtailHttpContext>(context => context.Url == "http://www.google.com");

            ObjectFactory.Initialize(
                delegate(IInitializationExpression e)
                    {
                        e.For<IThreadtailHttpContext>().Use(threadtailHttpContext);
                        e.For<IUrlBuilder>().Use<UrlBuilder>();
                        e.For<IJavaScriptTimeCalculator>().Use<JavaScriptTimeCalculator>();
                    });

            var urlBuilder = ObjectFactory.GetInstance<IUrlBuilder>();

            var url = urlBuilder.BuildUrl();

            StringAssert.Contains("http://www.google.com", url);
        }

        [Test]
        public void BuildUrl_WhenContextContainsUrl_TheResultContainsTheUrlAndTheBrowserName()
        {
            var threadtailHttpContext =
                Mock.Of<IThreadtailHttpContext>(x => x.Url == "http://www.google.com" && x.BrowserName == "BrowserName");
            var timeCalculator = Mock.Of<IJavaScriptTimeCalculator>(x => x.GetCurrentDateAsLong() == 1000);

            ObjectFactory.Initialize(
                delegate(IInitializationExpression e)
                    {
                        e.For<IThreadtailHttpContext>().Use(threadtailHttpContext);
                        e.For<IUrlBuilder>().Use<UrlBuilder>();
                        e.For<IJavaScriptTimeCalculator>().Use(timeCalculator);
                    });

            var urlBuilder = ObjectFactory.GetInstance<IUrlBuilder>();

            var url = urlBuilder.BuildUrl();

            StringAssert.Contains("http://www.google.com&enx1=Browser&evx1=BrowserName&tx1=1000", url);
        }

        [Test]
        public void BuildUrl_WhenContextContainsUrlAndBrowserNameIsFireFox_TheResultContainsTheUrlAndTheBrowserName()
        {
            var threadtailHttpContext =
                Mock.Of<IThreadtailHttpContext>(x => x.Url == "http://www.google.com" && x.BrowserName == "Firefox");
            var timeCalculator = Mock.Of<IJavaScriptTimeCalculator>(x => x.GetCurrentDateAsLong() == 1000);

            ObjectFactory.Initialize(
                delegate(IInitializationExpression e)
                    {
                        e.For<IThreadtailHttpContext>().Use(threadtailHttpContext);
                        e.For<IUrlBuilder>().Use<UrlBuilder>();
                        e.For<IJavaScriptTimeCalculator>().Use(timeCalculator);
                    });

            var urlBuilder = ObjectFactory.GetInstance<IUrlBuilder>();

            var url = urlBuilder.BuildUrl();

            StringAssert.Contains("http://www.google.com&enx1=Browser&evx1=Firefox&tx1=1000", url);
        }

        [Test]
        public void
            BuildUrl_WhenContextContainsUrlAndTheGenerationTimeIs1001_TheResultContainsTheUrlAndTheGenerationTimeIs1001()
        {
            var threadtailHttpContext =
                Mock.Of<IThreadtailHttpContext>(x => x.Url == "http://www.google.com" && x.BrowserName == "Firefox");
            var timeCalculator = Mock.Of<IJavaScriptTimeCalculator>(x => x.GetCurrentDateAsLong() == 1001);

            ObjectFactory.Initialize(
                delegate(IInitializationExpression e)
                    {
                        e.For<IThreadtailHttpContext>().Use(threadtailHttpContext);
                        e.For<IUrlBuilder>().Use<UrlBuilder>();
                        e.For<IJavaScriptTimeCalculator>().Use(timeCalculator);
                    });

            var urlBuilder = ObjectFactory.GetInstance<IUrlBuilder>();

            var url = urlBuilder.BuildUrl();

            StringAssert.Contains("http://www.google.com&enx1=Browser&evx1=Firefox&tx1=1001", url);
        }
    }
}