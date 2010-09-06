#region Using directives
using System;

using RabbitMQ.Client;

#endregion

namespace Threadtail.RabbitMqUtils
{
    public interface IChannelWrapper : IDisposable
    {
        IModel Channel { get; }
    }
}