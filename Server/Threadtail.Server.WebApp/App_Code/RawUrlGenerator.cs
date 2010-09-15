#region Using directives
using System;
using System.Text;
using System.Web;

#endregion

namespace Threadtail.Server.WebApp.App_Code
{
    internal static class RawUrlGenerator
    {
        public static string GenerateRawUrl(HttpRequest httpRequest)
        {
            var rawUrl = new StringBuilder();

            rawUrl.Append(httpRequest.RawUrl);

            // Add additional info from the request.
            var time = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;

            rawUrl.AppendFormat("&enx1={0}&evx1={1}&tx1={2}", "Browser", httpRequest.Browser.Browser, time);

            return rawUrl.ToString();
        }
    }
}