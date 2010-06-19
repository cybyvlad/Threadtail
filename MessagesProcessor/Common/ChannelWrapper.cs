#region Using directives

using System;
using RabbitMQ.Client;

#endregion

namespace Common
{
    public class ChannelWrapper : IDisposable
    {
        public ChannelWrapper()
        {
            var factory = new ConnectionFactory();
            var protocol = Protocols.FromEnvironment();
            _connection = factory.CreateConnection(protocol, "localhost", 5672);
            _channel = _connection.CreateModel();

            // Declare Exchange
            _channel.ExchangeDeclare(Settings.ExchangeName, ExchangeType.Direct);

            // Declare Queue
            _channel.QueueDeclare(Settings.QueueName);

            // Bind the Exchange to the Queue
            _channel.QueueBind(Settings.QueueName, Settings.ExchangeName, Settings.RoutingKey, false, null);
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        ~ChannelWrapper()
        {
            Dispose(false);
        }


        // Track whether Dispose has been called.
        private bool _disposed;

        private readonly IModel _channel;
        private readonly IConnection _connection;

        public IModel Channel
        {
            get { return _channel; }
        }

        public IConnection Connection
        {
            get { return _connection; }
        }

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
                    if (_channel != null)
                    {
                        _channel.Close();
                    }
                    if (_connection != null)
                    {
                        _connection.Close();
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
    }
}