using System;
using System.Collections.Generic;
using System.Web;
using MessageProcessor.Library.Events;

namespace MessageProcessor.Library
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IRuleEngine _ruleEngine;

        public MessageHandler(IRuleEngine ruleEngine)
        {
            _ruleEngine = ruleEngine;
        }

        #region IMessageHandler Members

        public void Process(string message)
        {
            String querystring;

            // Check to make sure some query string variables exist.
            var qPosition = message.IndexOf('?');
            if (qPosition == -1)
            {
                Console.WriteLine("No query string variables!");
                return;
            }

            if (qPosition < message.Length - 1)
            {
                querystring = message.Substring(qPosition + 1);
            }
            else
            {
                Console.WriteLine("No query string variables!");
                return;
            }

            ProcessQueryString(querystring);
        }

        #endregion

        private void ProcessQueryString(string querystring)
        {
            // Parse the query string variables into a NameValueCollection.
            var qscoll = HttpUtility.ParseQueryString(querystring);

            var sessionId = qscoll["ssid"];
            qscoll.Remove("ssid");

            var list = new SortedSet<AnalyticsEvent>();
            for (var i = 0; i < qscoll.Count; i += 3)
            {
                var analyticsEvent = new AnalyticsEvent(qscoll[i], qscoll[i + 1], qscoll[i + 2]);
                list.Add(analyticsEvent);
            }

            Console.WriteLine(_i++);
//            Console.WriteLine(sessionId);
//            _ruleEngine.ProcessAnalyticsEvents("TODO", sessionId, list);
        }

        private static int _i;
    }
}