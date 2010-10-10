using System.Collections.Generic;

namespace MessageProcessor.Library
{
    public class RuleLoader : IRuleLoader
    {
        public IEnumerable<IRule> LoadRules()
        {
            return new List<IRule>();
        }
    }
}