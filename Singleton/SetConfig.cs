using System;
using System.Collections.Generic;
using System.Text;

using IniFile.Models;

namespace RKstat
{
    class SetConfig
    {
        private static SetConfig setConfig = null;
        public static SetConfig Instance
        {
            get
            {
                if (setConfig == null)
                    setConfig = new SetConfig();
                return setConfig;
            }
        }

        public void ConfigINI(SectionIni sectionIni)
        {
            foreach (var item in sectionIni.Attribute)
            {
                switch (item.Key)
                {
                    case "PHPSESSID": Config.Instance.PHPSESSID = item.Value; break;
                    case "UrlGame": Config.Instance.UrlGame = item.Value; break;
                    case "UrlProfile": Config.Instance.UrlProfile = item.Value; break;
                    case "PathPlayer": Config.Instance.PathPlayer = item.Value; break;
                    case "PathSave": Config.Instance.PathSave = item.Value; break;
                    case "PathLang": Config.Instance.PathLang = item.Value; break;
                }
            }
        }
        public void LangINI(SectionIni sectionIni)
        {
            foreach (var item in sectionIni.Attribute)
            {
                switch (item.Key)
                {
                    case "PHPSESSIDCorrect": Lang.Instance.PHPSESSIDCorrect = item.Value; break;
                    case "PHPSESSIDEmpty": Lang.Instance.PHPSESSIDEmpty = item.Value; break;
                    case "PHPSESSIDWrong": Lang.Instance.PHPSESSIDWrong = item.Value; break;
                    case "DownloadCompleted": Lang.Instance.DownloadCompleted = item.Value; break;
                    case "DownloadProfileStart": Lang.Instance.DownloadProfileStart = item.Value; break;
                }
            }
        }

    }
}
