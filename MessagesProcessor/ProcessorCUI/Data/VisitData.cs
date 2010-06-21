using System;
using System.Collections.Concurrent;

namespace ProcessorCUI.Data
{
    public class VisitData
    {
        public DateTime LastActivity { get; set; }
        public ConcurrentDictionary<string, PageEvents> Pages
        {get; set; }

    }
}