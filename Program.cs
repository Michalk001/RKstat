using System;

namespace RKstat
{
    class Program
    {
        static int Main(string[] args)
        {
            LoadConfig loadConfig = new LoadConfig();
            loadConfig.Load();
            if (Config.Instance.PHPSESSID == null || Config.Instance.PHPSESSID == "")
                return -1 ;
            RKStat rKStat = new RKStat(Config.Instance.PHPSESSID);
            PlayerList playerList = new PlayerList();
        

            rKStat.CreatePlayerNameList(playerList.GetPlayerList());
            rKStat.CreatDataPlayers();
            PlayerDataSave playerDataSave = new PlayerDataSave(rKStat.playersData);
            playerDataSave.SaveArmyGeneral();
           
            Console.ReadKey();
            return 0;
        }
    }
}
