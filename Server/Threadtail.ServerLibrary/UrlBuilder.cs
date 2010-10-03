using System;
using System.Text;
using System.Web;

namespace Threadtail.ServerLibrary
{
    public class UrlBuilder : IUrlBuilder
    {
        private readonly IThreadtailHttpContext _context;
        private readonly IJavaScriptTimeCalculator _javaScriptTimeCalculator;

        public UrlBuilder(IThreadtailHttpContext context, IJavaScriptTimeCalculator javaScriptTimeCalculator)
        {
            _context = context;
            _javaScriptTimeCalculator = javaScriptTimeCalculator;
        }

        public string BuildUrl()
        {
            return _context.Url + string.Format("&enx1={0}&evx1={1}&tx1={2}", "Browser", _context.BrowserName, _javaScriptTimeCalculator.GetCurrentDateAsLong());

//            var rawUrl = new StringBuilder();
//
//            rawUrl.Append(httpRequest.RawUrl);
//
            // Add additional info from the request.
//            var time = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
//
//            rawUrl.AppendFormat("&enx1={0}&evx1={1}&tx1={2}", "Browser", httpRequest.Browser.Browser, time);
//
//            return rawUrl.ToString();
        }
    }
}