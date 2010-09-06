#region Using directives
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

            rawUrl.Append(@"/x.jpg?");

            return rawUrl.ToString();
        }
    }
}