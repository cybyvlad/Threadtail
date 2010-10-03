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
        public void Url_WhenHttpContextContainsNoRawUrl_ThrowsContextContainsNoUrlException()
        {
            var ctx = new HttpContext(new HttpRequest("", "http://www.google.com", ""), new HttpResponse(null));
            
            var threadtailHttpContext = new ThreadtailHttpContext(ctx);

            Assert.Throws(typeof (ContextContainsNoUrlException), () => { var x = threadtailHttpContext.Url; });
        }

        [Test]
        public void Url_WhenHttpContextContainsRawUrl_UrlShouldReturnRawUrl()
        {
            var url = "http://www.google.com";

            var ctx = new HttpContext(new HttpRequest("", url, ""), new HttpResponse(null));

            var threadtailHttpContext = new ThreadtailHttpContext(ctx);

            Assert.AreEqual(threadtailHttpContext.Url, url);
        }

    }
}