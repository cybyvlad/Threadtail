using System;

namespace ProcessorCUI
{
    public class AnalyticsEvent:IComparable<AnalyticsEvent>
    {
        public AnalyticsEvent(string name, string value, string time)
        {
            Name = name;
            Value = value;
            var ts =TimeSpan.FromMilliseconds(long.Parse(time));
            TimeOfEvent= (new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc).Add(ts));
        }
        public string Name { get; private set; }
        public string Value { get; private set; }
        public DateTime TimeOfEvent { get; private set; }
        public int CompareTo(AnalyticsEvent other)
        {
            return DateTime.Compare(TimeOfEvent, other.TimeOfEvent);
        }
    }
}