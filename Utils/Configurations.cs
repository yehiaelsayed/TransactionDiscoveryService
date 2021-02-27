using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransactonDiscovery.Utils
{
    public class Configurations
    {
        private static IConfiguration _Configuration;

        public Configurations(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public static string GetWalletServerUrl { get { return _Configuration["Wallet:serverUrl"]; } }
        public static int GetWalletMaxRequestLimit { get { return int.Parse(_Configuration["Wallet:maxRequestLimit"]); } }

        public static LogLevel GetLogLevel { get { return (LogLevel)Enum.Parse(typeof(LogLevel), _Configuration["Logging:LogLevel:Default"], true); } }

    }
}
