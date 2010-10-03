namespace Threadtail.ServerLibrary
{
    public interface IThreadtailHttpContext
    {
        string Url { get; }
        string BrowserName { get; }
    }
}