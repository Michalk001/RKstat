using System;
using System.Collections.Generic;
using System.Text;

namespace RKstat
{
    class LoadConfig
    {
        FileOperation file;
        
        Dictionary<string, string> dataConfig = new Dictionary<string, string>();
        public LoadConfig()
        {
            file = new FileOperation();
            file.SetPath("config.ini");
            file.Open();
        }

        public void Load()
        {
            ReadData();
     
            foreach(var item in dataConfig)
            {
                switch (item.Key)
                {
                    case "PHPSESSID" : Config.Instance.PHPSESSID = item.Value; break;
                    case "UrlGame" : Config.Instance.UrlGame = item.Value; break;
                    case "UrlProfile" : Config.Instance.UrlProfile = item.Value; break;
                    case "PathPlayer": Config.Instance.PathPlayer = item.Value; break;
                    case "PathSave": Config.Instance.PathSave = item.Value; break;
                }
            }
      
        }

        private void ReadData()
        {
            List<string> data = new List<string>();
            data = file.Read();
            if(data.Count != 0)
            {
                foreach(var item in data)
                {
                   
                    var tmp = item.Replace(" ","").Split('=',2);
                    if (tmp.Length != 2 )
                        continue;
                    if (tmp[0] == "" || tmp[1] == "")
                        continue;
                    dataConfig.Add(tmp[0], tmp[1]);
                }
            }
        }

    }
}
