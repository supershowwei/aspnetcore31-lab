using System;
using AspNetCore31Lab.Protocol.Physical;

namespace AspNetCore31Lab.Physical
{
    public class AppConfig : IConfig
    {
        private static readonly Lazy<AppConfig> Lazy = new Lazy<AppConfig>(() => new AppConfig());

        private AppConfig()
        {
        }

        public static AppConfig Instance => Lazy.Value;
    }
}