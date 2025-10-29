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
                .AddJsonFile("queries.json", optional: true, reloadOnChange: true);
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
            var sp = _configuration[$"Sql:StoredProcedures:{key}"];
            if (!string.IsNullOrWhiteSpace(sp)) 
                return sp;
            
            sp = _configuration[$"Oracle:StoredProcedures:{key}"];
            if (!string.IsNullOrWhiteSpace(sp)) 
                return sp;
            
            sp = _configuration[$"StoredProcedures:{key}"];
            return string.IsNullOrWhiteSpace(sp) ? key : sp;
        }

        public static string GetSqlQuery(string key)
        {
            EnsureInitialized();
            var q = _configuration[$"Sql:Queries:{key}"];
            if (!string.IsNullOrWhiteSpace(q)) 
                return q;
            
            q = _configuration[$"Oracle:Queries:{key}"];
            if (!string.IsNullOrWhiteSpace(q)) 
                return q;
            
            q = _configuration[$"SqlQueries:{key}"];
            return q ?? string.Empty;
        }
    }
}
