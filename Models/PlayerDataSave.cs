using RKstat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKstat.Models
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
            file.SetPath(pathToSave + "generalArmy-" + GetDate() + ".txt");
            file.Open();
            file.Save(CreatGeneralArmyStringData());
        }

        public void SaveAllDataPlayer()
        {
            file.SetPath(pathToSave + "allInfo-" + GetDate() + ".txt");
            file.Open();
            file.Save(CreateAllDataPlayer());
        }

        private List<string> CreateAllDataPlayer()
        {
            List<string> tmp = new List<string>
            {
                "Name;Level;Money;Province;City;Condition;FaithPoints;General;ArmyName;WayScience;Workshop"
            };
            foreach (var item in playerList)
            {
                if (item != null)
                    tmp.Add(item.Name + ";" + item.Level + ";" + item.Money + ";" + item.Province + ";" + item.City
                        + ";" + item.Condition + ";" + item.FaithPoints + ";" + item.General + ";" + item.ArmyName + ";" + item.WayScience + ";" + item.Workshop);
            }
            return tmp;
        }


        private List<string> CreatGeneralArmyStringData()
        {
            List<string> tmp = new List<string>
            {
                "Nick;ArmyName"
            };
            foreach (var item in playerList)
            {
                if(item != null)
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
