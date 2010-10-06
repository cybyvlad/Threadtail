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

namespace Threadtail.ServerLibrary.Test
{
   /// <summary>
   /// Represents a pattern for proper disposing of objects using the <see cref="System.IDisposable"/> interface.
   /// </summary>
   /// <example>
   /// This example illustrates how to use the dispose pattern in an inherited class.
   /// <code>
   ///	protected override void Dispose(bool disposing) 
   ///	{
   ///		if(disposing) 
   ///		{
   ///			// 
   ///			// Free the state of managed objects.
   ///			//
   ///		}
   ///
   ///		//
   ///		// Free the state of unmanaged objects. i.e. set large fields to null.
   ///		//
   ///		
   ///		//
   ///		// Don't forget to call the base class.
   ///		//
   ///		base.Dispose(disposing);
   /// }
   /// </code>
   /// </example>
   [Serializable]
   public abstract class Disposable : IDisposable
   {
      private bool _isDisposed;

      #region Methods

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      #endregion

      #region Destructor

      /// <summary>
      /// Destroys the current instance, which calls any finalization code.
      /// </summary>
      ~Disposable()
      {
         Dispose(false);
      }

      #endregion

      #region Methods

      /// <summary>
      /// Checks whether the current instance is disposed.
      /// </summary>
      /// <exception cref="ObjectDisposedException">The exception that is thrown when an operation is performed on a disposed object.</exception>
      protected void CheckIsDisposed()
      {
         if (_isDisposed)
         {
            throw new ObjectDisposedException(string.Format("{0}:{1}", GetType().FullName, GetHashCode()),
                                              "Operations are not allowed on a disposed object.");
         }
      }

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      /// <param name="disposing"></param>
      protected virtual void Dispose(bool disposing)
      {
         if (disposing)
         {
            //
            // Free the state of managed objects.
            //
         }

         //
         // Free the state of unmanaged objects (for example, set large fields to null).
         //

         //
         // We are now disposed.
         //
         _isDisposed = true;
      }

      #endregion
   }
}