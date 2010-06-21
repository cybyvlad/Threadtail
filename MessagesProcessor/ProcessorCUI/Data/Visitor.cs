using System.Collections.Concurrent;

namespace ProcessorCUI.Data
{
    public class Visitor
    {
        public int Id { get; set; }
        public string LatestSSID { get; set; }
        
        public Visitor()
        {
            BrowserInfo = new BrowserData();
        }
        public BrowserData BrowserInfo { get; set; }

        public ConcurrentDictionary<string, VisitData> Visits { get; set; }

    }
}