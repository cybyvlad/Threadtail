#region Using directives
using System;
using System.Threading;
using System.Web;

using Threadtail.Server.MessageBus;

#endregion

namespace Threadtail.ServerLibrary
{
    internal class AsyncOperation : IAsyncResult
    {
        #region Construction & Destruction
        public AsyncOperation(HttpContext context)
        {
            _context = context;
        }
        #endregion

        #region Fields
        private readonly HttpContext _context;
        #endregion

        #region Properties
        bool IAsyncResult.IsCompleted
        {
            get { return true; }
        }

        WaitHandle IAsyncResult.AsyncWaitHandle
        {
            get { return null; }
        }

        Object IAsyncResult.AsyncState
        {
            get { return null; }
        }

        bool IAsyncResult.CompletedSynchronously
        {
            get { return false; }
        }
        #endregion

        #region Methods

        #region Privates

        #region StartAsyncTask
        private void StartAsyncTask(Object workItemState)
        {
            var httpRequest = _context.Request;

            var rawUrl = RawUrlGenerator.GenerateRawUrl(httpRequest);
            MessageBusSender.SendMessage(rawUrl);
        }
        #endregion

        #endregion

        #region StartAsyncWork
        public void StartAsyncWork()
        {
//            ThreadPool.QueueUserWorkItem(StartAsyncTask, null);
            StartAsyncTask(null);
        }
        #endregion

        #endregion
    }
}