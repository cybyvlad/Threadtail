using System.Collections.Generic;
using MessageProcessor.Library.Events;

namespace MessageProcessor.Library
{
    public interface IRuleEngineInput
    {
        string SiteId { get; }
        string SessionId { get; }
        SortedSet<AnalyticsEvent> Events { get; }
    }
}