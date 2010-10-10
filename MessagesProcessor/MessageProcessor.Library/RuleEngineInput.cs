using System.Collections.Generic;
using MessageProcessor.Library.Events;

namespace MessageProcessor.Library
{
    public class RuleEngineInput : IRuleEngineInput
    {
        public RuleEngineInput(string siteId, string sessionId, SortedSet<AnalyticsEvent> analyticsEvents)
        {
            _siteId = siteId;
            _sessionId = sessionId;
            _analyticsEvents = analyticsEvents;
        }

        private readonly string _siteId;
        private readonly string _sessionId;
        private readonly SortedSet<AnalyticsEvent> _analyticsEvents;

        public string SiteId
        {
            get { return _siteId; }
        }

        public string SessionId
        {
            get { return _sessionId; }
        }

        public SortedSet<AnalyticsEvent> Events
        {
            get { return _analyticsEvents; }
        }
    }
}