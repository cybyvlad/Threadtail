#region Using directives
using System;
using System.Text;
using MessageProcessor.Library;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

using Threadtail.RabbitMqUtils;

#endregion

namespace ProcessorCUI
{
    public class NotificationConsumer : IDisposable
    {
        public NotificationConsumer(IMessageHandler messageHandler, IChannelWrapper channelWrapper)
        {
            _messageHandler = messageHandler;
            _channelWrapper = channelWrapper;

            Listen();
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
        private readonly IMessageHandler _messageHandler;
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

        private void Listen()
        {
            var channel = _channelWrapper.Channel;

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
                    var message = Encoding.UTF8.GetString(body);

                    _messageHandler.Process(message);
                    

                    // TODO: stop?
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
    }
}