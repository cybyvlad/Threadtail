#region Using directives

using System;
using System.Diagnostics;
using System.Threading;
using System.Web;

#endregion

namespace Threadtail.Server.WebApp
{
    internal class AsynchOperation : IAsyncResult
    {
        private bool _completed;
        private readonly Object _state;
        private readonly AsyncCallback _callback;
        private readonly HttpContext _context;
        private const string SSID = "ssid";

        bool IAsyncResult.IsCompleted
        {
            get { return _completed; }
        }

        WaitHandle IAsyncResult.AsyncWaitHandle
        {
            get { return null; }
        }

        Object IAsyncResult.AsyncState
        {
            get { return _state; }
        }

        bool IAsyncResult.CompletedSynchronously
        {
            get { return false; }
        }

        public AsynchOperation(AsyncCallback callback, HttpContext context, Object state)
        {
            _callback = callback;
            _context = context;
            _state = state;
            _completed = false;
        }

        public void StartAsyncWork()
        {
            ThreadPool.QueueUserWorkItem(StartAsyncTask, null);
        }

        private void StartAsyncTask(Object workItemState)
        {
            Debug.WriteLine("========================================");

            var queryString = _context.Request.QueryString;
            Debug.WriteLine("querystring =[" + queryString + "]");
            Debug.WriteLine("Session ID = " + queryString[SSID]);

            for (var i = 1; i < queryString.AllKeys.Length; i++)
            {
                var eventName = queryString.AllKeys[i];
                var eventValue = queryString[eventName];

                Debug.WriteLine(eventName + " -> " + eventValue);
            }

            Debug.WriteLine("========================================");

            _completed = true;
            _callback(this);
        }
    }
}