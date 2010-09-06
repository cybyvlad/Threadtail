#region Using directives
using System;
using System.Threading;
using System.Web;

using Threadtail.Server.MessageBus;

#endregion

namespace Threadtail.Server.WebApp.App_Code
{
    internal class AsyncOperation : IAsyncResult
    {
        #region Construction & Destruction
        public AsyncOperation(AsyncCallback callback, HttpContext context, Object state)
        {
            _callback = callback;
            _context = context;
            _state = state;
            _completed = false;
        }
        #endregion

        #region Fields
        private bool _completed;
        private readonly Object _state;
        private readonly AsyncCallback _callback;
        private readonly HttpContext _context;
        #endregion

        #region Properties
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
        #endregion

        #region Methods

        #region StartAsyncWork
        public void StartAsyncWork()
        {
            ThreadPool.QueueUserWorkItem(StartAsyncTask, null);
        }
        #endregion

        #region StartAsyncTask
        private void StartAsyncTask(Object workItemState)
        {
            var httpRequest = _context.Request;

            MessageBusSender.SendMessage(httpRequest.RawUrl);

            // Send also additional information extracted from Request.
            var generateRawUrl = RawUrlGenerator.GenerateRawUrl(httpRequest);

            _completed = true;
            _callback(this);
        }
        #endregion

        #endregion
    }
}