using System.Collections.Generic;

namespace MessageProcessor.Library
{
    public interface IRuleFilter
    {
        IEnumerable<IRule> FilterRulesForSite(string siteId);
    }
}