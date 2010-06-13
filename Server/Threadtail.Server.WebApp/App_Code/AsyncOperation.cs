#region Using directives
using System;
using System.Threading;
using System.Web;

using Threadtail.Server.MessageBus;

#endregion

namespace Threadtail.Server.WebApp
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
        private static readonly MessageBusSender _messageBusSender = new MessageBusSender();
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
        public void StartAsyncWork()
        {
            ThreadPool.QueueUserWorkItem(StartAsyncTask, null);
        }

        private void StartAsyncTask(Object workItemState)
        {
            var httpRequest = _context.Request;

            MessageBusSender.SendMessage(httpRequest.RawUrl);

            _completed = true;
            _callback(this);
        }
        #endregion
    }
}