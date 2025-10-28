using System;
using Microsoft.Extensions.Configuration;

namespace PriceCompare.Core.Helpers
{
    public static class ConfigurationHelper
    {
        private static IConfigurationRoot _configuration;
        private static bool _initialized;

        private static void EnsureInitialized()
        {
            if (_initialized) return;
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("queries.json", optional: true, reloadOnChange: true); // load SqlQueries & StoredProcedures
            _configuration = builder.Build();
            _initialized = true;
        }

        public static string SqlServerConnectionString
        {
            get
            {
                EnsureInitialized();
                return _configuration.GetConnectionString("SqlServerConnection") ?? string.Empty;
            }
        }

        public static string OracleConnectionString
        {
            get
            {
                EnsureInitialized();
                return _configuration.GetConnectionString("OracleConnection") ?? string.Empty;
            }
        }

        public static string DealerPortalDbConnectionString
        {
            get
            {
                EnsureInitialized();
                return _configuration.GetConnectionString("DealerPortalDB") ?? string.Empty;
            }
        }

        public static string GetStoredProcedure(string key)
        {
            EnsureInitialized();
            var val = _configuration[$"StoredProcedures:{key}"];
            return string.IsNullOrWhiteSpace(val) ? key : val;
        }

        public static string GetSqlQuery(string key)
        {
            EnsureInitialized();
            return _configuration[$"SqlQueries:{key}"] ?? string.Empty;
        }
    }
}
