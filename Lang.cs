using System;
using System.Collections.Generic;
using System.Text;

namespace RKstat
{
    class Lang
    {
        private static Lang lang = null;
        public static Lang Instance
        {
            get
            {
                if (lang == null)
                    lang = new Lang();
                return lang;
            }
        }

        public string PHPSESSIDCorrect { get; set; } = "";
        public string PHPSESSIDWrong { get; set; } = "";
        public string PHPSESSIDEmpty { get; set; } = "";
        public string DownloadProfileStart { get; set; } = "";
        public string DownloadCompleted { get; set; } = "";
    }
}
