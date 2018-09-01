using System;
using System.Collections.Generic;
using System.Text;

namespace RKstat
{
    public sealed class Config
    {
        private static Config config = null;
        public static Config Instance
        {
            get
            {
                if (config == null)
                    config = new Config();
                return config;
            }
        }

        public string PHPSESSID { get; set; }
        public string UrlProfile { get; set; }
        public string UrlGame { get; set; }
        public string PathPlayer { get; set; }
        public string PathSave { get; set; }

    }
}
