using System;
using System.Collections.Generic;
using System.Text;

namespace RKstat.Models
{
    class PlayerList
    {
        FileOperation file;
        public PlayerList()
        {
            file = new FileOperation();
            file.SetPath(Config.Instance.PathPlayer);
            file.Open();
        }
        public string GetPlayerList()
        {
            var Data = file.Read();
            string playerList = "";
            if(Data.Count != 0)
            {
                foreach(var item in Data)
                {
                    playerList += item.Replace('"',' ').Trim(' ');
                }
                return playerList.Replace(" ","");
            }
            return null;
        }
    }
}
