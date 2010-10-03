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
                var rawUrl = _context.Request.RawUrl;

                if (!String.IsNullOrWhiteSpace(rawUrl))
                {
                    return rawUrl;
                }

                throw new ContextContainsNoUrlException();
            }
        }

        public string BrowserName
        {
            get { throw new NotImplementedException(); }
        }
    }
}