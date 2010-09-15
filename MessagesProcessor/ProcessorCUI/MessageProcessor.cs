#region Using directives
using System;
using System.Collections.Generic;
using System.Web;

using ProcessorCUI.Data;

#endregion

namespace ProcessorCUI
{
    public class MessageProcessor
    {
        public static void ProcessMessage(string querystring)
        {
            // Parse the query string variables into a NameValueCollection.
            var qscoll = HttpUtility.ParseQueryString(querystring);

            var sessionId = qscoll["ssid"];
            qscoll.Remove("ssid");

            var list = new SortedSet<AnalyticsEvent>();
            for (var i = 0; i < qscoll.Count; i += 3)
            {
                var m = new AnalyticsEvent(qscoll[i], qscoll[i+1], qscoll[i+2]);
                if (m.EventType != EEventType.MouseMove)
                {
                    list.Add(m);
                }
            }

            Console.WriteLine();
            Console.WriteLine("UserId=" + sessionId);
            Console.WriteLine("Performed the following actions");
            foreach (var analyticsEvent in list)
            {
                Console.WriteLine("Event:" + analyticsEvent.Name);
                Console.WriteLine("Value:" + analyticsEvent.Value);
                Console.WriteLine("Time:" + analyticsEvent.TimeOfEvent);
            }
        }
    }
}