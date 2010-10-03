#region Using directives

#endregion

namespace Threadtail.ServerLibrary
{
    public interface IMessageBusSender
    {
        void SendMessage(string rawUrl);
    }
}