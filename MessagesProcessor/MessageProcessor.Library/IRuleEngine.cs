using System;
using System.Collections.Generic;
using MessageProcessor.Library.Events;

namespace MessageProcessor.Library
{
    public interface IRuleEngine
    {
        void ProcessAnalyticsEvents(IRuleEngineInput ruleEngineInput);
    }
}