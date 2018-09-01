using System;
using System.Collections.Generic;
using System.Text;

namespace RKstat
{
    class PlayerDataSave
    {
        string pathToSave;
        FileOperation file;
        List<Player> playerList;
        public PlayerDataSave(List<Player> playerList)
        {
            this.playerList = playerList;
            file = new FileOperation();
            pathToSave = Config.Instance.PathSave;
        }
        public void SaveArmyGeneral()
        {
            file.SetPath(pathToSave+"generalArmy-"+ GetDate()+".txt");
            file.Open();
            file.Save(CreatPlayerStringData());
        }

        private List<string> CreatPlayerStringData()
        {
            List<string> tmp = new List<string>();
            foreach(var item in playerList)
            {
                if(item.General == "true")
                tmp.Add(item.Name+";"+item.ArmyName);
            }
            return tmp;
        }
        private string GetDate()
        {
            string date = DateTime.Now.ToString("dd-MM-yyyy");
            return date;
        }
    }
}
