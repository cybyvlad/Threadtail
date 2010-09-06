#region Using directives
using System;
using System.Web;

#endregion

namespace Threadtail.Server.WebApp.App_Code
{
    public class HttpHandler : IHttpAsyncHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback asyncCallback, Object extraData)
        {
            var asynch = new AsyncOperation(asyncCallback, context, extraData);
            asynch.StartAsyncWork();

            return asynch;
        }

        public void EndProcessRequest(IAsyncResult result)
        {
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new InvalidOperationException();
        }
    }
}