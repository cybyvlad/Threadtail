using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using ProcessorCUI;

namespace Consumer
{
    public class MessageProcessor
    {
        public void ProcessMessage(string querystring)
        {
            // Parse the query string variables into a NameValueCollection.
            var qscoll = HttpUtility.ParseQueryString(querystring);

            //get the session ID first
            var userId = qscoll["ssid"];
            qscoll.Remove("ssid");
            
            int numberOfEvents = qscoll.Count/3;
            var list = new SortedSet<AnalyticsEvent>();
            for (int i = 0; i < numberOfEvents;i++ )
            {
                var m = new AnalyticsEvent(qscoll["en" + i], qscoll["ev" + i], qscoll["t" + i]);
                list.Add(m);
            }
            Console.WriteLine("UserId="+userId);
            Console.WriteLine("Performed the following actions");
            foreach (var analyticsEvent in list)
            {
                Console.WriteLine("Event:"+analyticsEvent.Name);
                Console.WriteLine("Value:"+analyticsEvent.Value);
                Console.WriteLine("Time:"+analyticsEvent.TimeOfEvent);
            }


        }
    }
}