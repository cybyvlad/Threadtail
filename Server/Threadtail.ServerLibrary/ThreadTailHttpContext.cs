#region Using directives
using System;
using System.Web;

#endregion

namespace Threadtail.ServerLibrary
{
    public class ThreadtailHttpContext : IThreadtailHttpContext
    {
        private readonly HttpContext _context;

        public ThreadtailHttpContext(HttpContext context)
        {
            _context = context;
        }

        public string Url
        {
            get
            {
                return _context.Request.RawUrl;
            }
        }

        public string BrowserName
        {
            get { return _context.Request.Browser.Browser; }
        }
    }
}