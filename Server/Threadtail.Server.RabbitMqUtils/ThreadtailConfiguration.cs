#region Using directives
using System;
using System.Collections.Generic;
using System.Configuration;

#endregion

namespace Threadtail.Server.RabbitMqUtils
{
    public class ThreadtailConfiguration
    {
        #region Construction & Destruction
        private ThreadtailConfiguration()
        {
            Settings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            foreach (string setting in ConfigurationManager.AppSettings)
            {
                if (setting.StartsWith("Threadtail/", StringComparison.InvariantCultureIgnoreCase))
                {
                    Settings[setting] = ConfigurationManager.AppSettings[setting];
                }
            }

            var rabbitMqPortStr = ConfigurationManager.AppSettings["Threadtail/RabbitMqPort"];
            RabbitMqPort = rabbitMqPortStr != null ? int.Parse(rabbitMqPortStr) : 5672;

            var rabbitMqHostStr = ConfigurationManager.AppSettings["Threadtail/RabbitMqHost"];
            RabbitMqHost = rabbitMqHostStr ?? "localhost";
        }
        #endregion

        #region Fields
        // TODO : don't use singleton
        private static readonly ThreadtailConfiguration _instance = new ThreadtailConfiguration();
        #endregion

        #region Properties
        public IDictionary<string, string> Settings { get; set; }
        public int RabbitMqPort { get; set; }
        public string RabbitMqHost { get; set; }

        public static ThreadtailConfiguration Instance
        {
            get { return _instance; }
        }
        #endregion

        #region Methods

        #region Privates
        #endregion

        #region Publics
//        #region LoadLoggingSettings
//        public void LoadLoggingSettings()
//        {
//            XmlConfigurator.ConfigureAndWatch(
//                new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));
//        }
//        #endregion
        #endregion

        #endregion
    }
}