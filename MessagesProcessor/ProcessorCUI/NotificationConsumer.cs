#region Using directives
using System;
using System.Text;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

using Threadtail.RabbitMqUtils;

#endregion

namespace ProcessorCUI
{
    public class NotificationConsumer : IDisposable
    {
        public NotificationConsumer()
        {
            _channelWrapper = ChannelFactory.CreateChannel();
            Start(_channelWrapper.Channel);
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        ~NotificationConsumer()
        {
            Dispose(false);
        }

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

        private static void Start(IModel channel)
        {
            var consumer = new QueueingBasicConsumer(channel);
            channel.BasicConsume(Settings.QueueName, null, consumer);
            while (true)
            {
                try
                {
                    // Queue.Dequeue is blocking...
                    var e = (BasicDeliverEventArgs) consumer.Queue.Dequeue();

                    // acknowledge receipt of the message
                    channel.BasicAck(e.DeliveryTag, false);

                    var body = e.Body;
                    var rawUrl = Encoding.UTF8.GetString(body);
//                    Console.WriteLine(string.Format("Message [{0}] received.", message));

                    ProcessMessage(rawUrl);
                }
                catch (OperationInterruptedException ex)
                {
                    // The consumer was removed, either through
                    // channel or connection closure, or through the
                    // action of IModel.BasicCancel().
                    break;
                }
            }
        }

        private static void ProcessMessage(string rawUrl)
        {
            String querystring = null;

            // Check to make sure some query string variables exist.
            var qPosition = rawUrl.IndexOf('?');
            if (qPosition == -1)
            {
                Console.WriteLine("No query string variables!");
                return;
            }

            if (qPosition < rawUrl.Length - 1)
            {
                querystring = rawUrl.Substring(qPosition + 1);
            }
            else
            {
                Console.WriteLine("No query string variables!");
                return;
            }
            var mp = new MessageProcessor();
            MessageProcessor.ProcessMessage(querystring);
        }
    }
}