using System;

namespace ProcessorCUI
{
    public class AnalyticsEvent
    {
        public AnalyticsEvent(string name, string value, string time)
        {
            Name = name;
            Value = value;
            TimeSpan ts = new TimeSpan(long.Parse(time));
            TimeOfEvent= (new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc)+ts);
        }
        public string Name { get; private set; }
        public string Value { get; private set; }
        public DateTime TimeOfEvent { get; private set; }
    }
}