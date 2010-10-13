using System.Collections.Generic;

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

        public void ProcessAnalyticsEvents(IRuleEngineInput ruleEngineInput)
        {
            //var rulesForSite = _ruleFilter.FilterRulesForSite(ruleEngineInput.SiteId);

            // Get context from Mongo
        }
    }
}