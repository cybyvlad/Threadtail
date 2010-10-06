#region Using directives

using System.Web;
using NUnit.Framework;

#endregion

namespace Threadtail.ServerLibrary.Test
{
    [TestFixture]
    public class ThreadtailHttpContextTests
    {
        [Test]
        public void Url_WhenHttpContextContainsRawUrl_UrlShouldReturnRawUrl()
        {
            string url = "x.jpg";

            using (var mockHttpContext = new MockHttpContext(@"C:\Intepub\wwwroot\", "/", url, ""))
            {
                var threadtailHttpContext = new ThreadtailHttpContext(HttpContext.Current);

                Assert.AreEqual(threadtailHttpContext.Url, "/" + url);
            }
        }

        [Test]
        public void BrowserName_WhenHttpContextContainsBrowserName_TheBrowserNameShouldBeReturned()
        {
            string url = "x.jpg";

            using (var mockHttpContext = new MockHttpContext(@"C:\Intepub\wwwroot\", "/", url, ""))
            {
                var threadtailHttpContext = new ThreadtailHttpContext(HttpContext.Current);

                Assert.AreEqual(threadtailHttpContext.Url, "/" + url);
            }
        }
    }
}