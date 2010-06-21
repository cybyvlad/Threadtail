using System.Collections.Generic;

namespace ProcessorCUI.Data
{
    public class PageEvents
    {
        public PageEvents()
        {
            Events = new SortedSet<AnalyticsEvent>();
        }

        public SortedSet<AnalyticsEvent> Events { get; set; }
    }
}