//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// Author: Michael J. Primeaux
// 

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.SessionState;

namespace Threadtail.ServerLibrary.Test
{
   /// <summary>
   /// Represents a mock <see cref="HttpContext"/>.
   /// </summary>
   public sealed class MockHttpContext : Disposable
   {
      private static readonly Dictionary<string, SessionItem> _sessionItems = new Dictionary<string, SessionItem>();

      private static readonly ReaderWriterLockSlim _ioLock =
         new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

      private readonly string _applicationPath;
      private readonly HttpCookieMode _cookieMode = HttpCookieMode.UseCookies;

      private readonly HttpContext _currentContext;
      private readonly SessionItem _sessionData;
      private readonly string _sessionID;
      private readonly string _virtualDirectory;

       /// <summary>
       /// Initializes a new instance of the <see cref="MockHttpContext"/> class using the specified application path and virtual directory.
       /// </summary>
       /// <param name="applicationPath"></param>
       /// <param name="virtualDirectory"></param>
       /// <param name="page"></param>
       /// <param name="query"></param>
       public MockHttpContext(string applicationPath, string virtualDirectory, string page, string query)
      {
         if (String.IsNullOrWhiteSpace(applicationPath))
         {
            throw new ArgumentException("Application path is empty!", "applicationPath");
         }

         if (String.IsNullOrWhiteSpace(virtualDirectory))
         {
            throw new ArgumentException("Virtual directory path is empty!", "virtualDirectory");
         }

         _applicationPath = applicationPath;
         _virtualDirectory = virtualDirectory;

         _currentContext = HttpContext.Current;

         if (null == HttpContext.Current)
         {
            AppDomain.CurrentDomain.SetData(".appDomain", "*");
            AppDomain.CurrentDomain.SetData(".appPath", applicationPath);
            AppDomain.CurrentDomain.SetData(".appVPath", virtualDirectory);
            AppDomain.CurrentDomain.SetData(".hostingVirtualPath", virtualDirectory);
            AppDomain.CurrentDomain.SetData(".hostingInstallDir", HttpRuntime.AspInstallDirectory);
            TextWriter tw = new StringWriter();
             var simpleWorkerRequest = new SimpleWorkerRequest(page, query, tw);
             
             HttpWorkerRequest wr = simpleWorkerRequest;
            HttpContext.Current = new HttpContext(wr);
            
            _sessionData = new SessionItem();
            _sessionData.Items = new SessionStateItemCollection();
            _sessionData.StaticObjects = SessionStateUtility.GetSessionStaticObjects(HttpContext.Current);
            _sessionID = Guid.NewGuid().ToString();

            _ioLock.EnterWriteLock();

            try
            {
               _sessionItems.Add(_sessionID, _sessionData);
            }
            finally
            {
               _ioLock.ExitWriteLock();
            }

            var container = new HttpSessionStateContainer(_sessionID, _sessionData.Items, _sessionData.StaticObjects, 10,
                                                          true, _cookieMode, SessionStateMode.Custom, false);
            SessionStateUtility.AddHttpSessionStateToContext(HttpContext.Current, container);
         }
      }

      /// <summary>
      /// Gets the virtual directory for the <see cref="MockHttpContext"/>
      /// </summary>
      public string VirtualDirectory
      {
         get
         {
            CheckIsDisposed();
            return _virtualDirectory;
         }
      }

      /// <summary>
      /// Gets the application path for the <see cref="MockHttpContext"/>
      /// </summary>
      public string ApplicationPath
      {
         get
         {
            CheckIsDisposed();
            return _applicationPath;
         }
      }

      /// <summary>
      /// Gets the sessions.
      /// </summary>
      /// <value>The sessions.</value>
      public static Dictionary<string, SessionItem> Sessions
      {
         get
         {
            _ioLock.EnterReadLock();

            try
            {
               return _sessionItems;
            }
            finally
            {
               _ioLock.ExitReadLock();
            }
         }
      }

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      /// <param name="disposing"></param>
      protected override void Dispose(bool disposing)
      {
         CheckIsDisposed();

         if (disposing)
         {
            //
            // Free the state of managed objects.
            //
            _ioLock.EnterWriteLock();

            try
            {
               _sessionItems.Remove(_sessionID);
            }
            finally
            {
               _ioLock.ExitWriteLock();
            }

            HttpContext.Current = _currentContext;
         }

         //
         // Free the state of unmanaged objects (for example, set large fields to null).
         //

         //
         // Call the base.
         //
         base.Dispose(disposing);
      }

      #region Nested type: SessionItem

      /// <summary>
      /// Represents a session item.
      /// </summary>
      public sealed class SessionItem
      {
         /// <summary>
         /// Initializes a new instance of the <see cref="SessionItem"/> class.
         /// </summary>
         internal SessionItem()
         {
         }

         /// <summary>
         /// Gets the items for the <see cref="SessionItem"/>.
         /// </summary>
         /// <value>The items.</value>
         public SessionStateItemCollection Items { get; internal set; }

         /// <summary>
         /// Gets the static objects for the <see cref="SessionItem"/>.
         /// </summary>
         /// <value>The static objects.</value>
         public HttpStaticObjectsCollection StaticObjects { get; internal set; }
      }

      #endregion
   }
}