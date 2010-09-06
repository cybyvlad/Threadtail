#region Using directives
using System;
using System.Text;
using System.Threading;

using RabbitMQ.Client;

using Threadtail.RabbitMqUtils;

#endregion

namespace Consumer
{
    public class PollingConsumer : IDisposable
    {
        public PollingConsumer()
        {
            _channelWrapper = ChannelFactory.CreateChannel();
            _thread = new Thread(Start);
            _thread.Start(_channelWrapper.Channel);
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        ~PollingConsumer()
        {
            Dispose(false);
        }

        private readonly Thread _thread;
        // Track whether Dispose has been called.
        private bool _disposed;
        private readonly IChannelWrapper _channelWrapper;

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_thread != null)
                    {
                        _thread.Abort();
                    }

                    if (_channelWrapper != null)
                    {
                        _channelWrapper.Dispose();
                    }
                }

                // Note disposing has been done.
                _disposed = true;
            }
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        private static void Start(object state)
        {
            var channel = (IModel) state;

            while (true)
            {
                const bool noAck = false;
                var result = channel.BasicGet(Settings.QueueName, noAck);
                if (result == null)
                {
                    Console.WriteLine("No message available at this time");
                    Thread.Sleep(new TimeSpan(0, 0, 0, 0, 500));
                }
                else
                {
                    // acknowledge receipt of the message
                    channel.BasicAck(result.DeliveryTag, false);

//                    var props = result.BasicProperties;
                    var body = result.Body;
//                    Console.WriteLine(props);
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Message received. Message has the length = " + message.Length);
                }
            }
        }
    }
}