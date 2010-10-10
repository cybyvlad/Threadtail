using System;
using System.Collections.Generic;

namespace MessageProcessor.Library
{
    public interface IRuleLoader
    {
        IEnumerable<IRule> LoadRules();
    }
}