using System;
using System.Threading;

namespace RKstat
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Start");
            LoadConfig loadConfig = new LoadConfig();
            loadConfig.Load();
            if (Config.Instance.PHPSESSID == null || Config.Instance.PHPSESSID == "")
            {
                Console.WriteLine("PHPSession is empty");
                Console.ReadKey();
                return -1;
            }
            RKStat rKStat = new RKStat();
            if (!rKStat.PHPSessionCorrect())
            {
                Console.WriteLine("PHPSession is wrong");
                Console.ReadKey();
                return -1;
            }
            PlayerList playerList = new PlayerList();
            rKStat.CreatePlayerNameList(playerList.GetPlayerList());   
            rKStat.CreatDataPlayers();
            PlayerDataSave playerDataSave = new PlayerDataSave(rKStat.playersData);
            playerDataSave.SaveArmyGeneral();
            playerDataSave.SaveAllDataPlayer();
            Console.ReadKey();
            return 0;
        }
    }
}
