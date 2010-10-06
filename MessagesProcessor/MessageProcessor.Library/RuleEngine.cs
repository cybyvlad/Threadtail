using System.Collections.Generic;
using MessageProcessor.Library.Events;

namespace MessageProcessor.Library
{
    public class RuleEngine : IRuleEngine
    {
        private IRuleFilter _ruleFilter;
        private IEnumerable<IRule> _allRules;
        // TODO: Singleton from StructureMap.
        // TODO: Also send the siteId.

        public RuleEngine(IRuleLoader ruleLoader)
        {
            _allRules = ruleLoader.LoadRules();
        }

        public void ProcessAnalyticsEvents(string siteId, string sessionId, SortedSet<AnalyticsEvent> analyticsEvents)
        {
            var rulesForSite = _ruleFilter.FilterRulesForSite(siteId);
        }
    }
}