#region Using directives
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Web;
using StructureMap;
using StructureMap.Pipeline;
using Threadtail.ServerLibrary;

#endregion

namespace Threadtail.Server.WebApp.App_Code
{
    public class HttpHandler : IHttpHandler
    {
        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
        /// </returns>
        public bool IsReusable
        {
            get { return false; }
        }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context)
        {
            var threadTailHttpContext = new ThreadtailHttpContext(context);

            var dictionary = new Dictionary<string, object>();
            dictionary.Add("context",threadTailHttpContext);

            var contextHandler = ObjectFactory.GetInstance<IHttpContextHandler>(new ExplicitArguments(dictionary));
            contextHandler.Process();
        }
    }
}