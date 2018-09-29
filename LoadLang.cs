using System;
using System.Collections.Generic;
using System.Text;

namespace RKstat
{
    class LoadLang
    {
        FileOperation file;

        Dictionary<string, string> data = new Dictionary<string, string>();
        public LoadLang()
        {

            file = new FileOperation();
            
        }

        public void Load()
        {
            if (Config.Instance.PathLang == null || Config.Instance.PathLang == "")
                return ;
            file.SetPath(Config.Instance.PathLang);
            file.Open();
            ReadData();

            foreach (var item in data)
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

        private void ReadData()
        {
            List<string> data = new List<string>();
            data = file.Read();
            if (data.Count != 0)
            {
                foreach (var item in data)
                {

                    var tmp = item.TrimEnd(' ').TrimStart(' ').Split('=', 2);
                    if (tmp.Length != 2)
                        continue;
                    if (tmp[0] == "" || tmp[1] == "")
                        continue;
                    this.data.Add(tmp[0], tmp[1]);
                }
            }
        }

    }
}
