#region Using directives

using System;
using System.Diagnostics;
using System.Threading;
using System.Web;

#endregion

namespace Threadtail.Server.WebApp
{
    //public class HttpHandler : IHttpHandler
    //{
    //    private const string SSID = "ssid";

    //    /// <summary>
    //    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
    //    /// </summary>
    //    /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. </param>
    //    public void ProcessRequest(HttpContext context)
    //    {
    //        Debug.WriteLine("");
    //        var queryString = context.Request.QueryString;

    //        Debug.WriteLine("querystring =[" + queryString + "]");
    //        Debug.WriteLine("Session ID = " + queryString[SSID]);

    //        for (var i = 1; i < queryString.AllKeys.Length; i++)
    //        {
    //            var eventName = queryString.AllKeys[i];
    //            var eventValue = queryString[eventName];

    //            Debug.WriteLine(eventName + " -> " + eventValue);
    //        }
    //        Debug.WriteLine("");
    //    }

    //    /// <summary>
    //    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
    //    /// </summary>
    //    /// <returns>
    //    /// true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
    //    /// </returns>
    //    public bool IsReusable
    //    {
    //        get { return true; }
    //    }
    //}

    public class HttpHandler : IHttpAsyncHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback asyncCallback, Object extraData)
        {
            var asynch = new AsynchOperation(asyncCallback, context, extraData);
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