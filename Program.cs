using System;
using System.Threading;

namespace RKstat
{
    class Program
    {
        static int Main(string[] args)
        {
            LoadConfig loadConfig = new LoadConfig();
            loadConfig.Load();
            LoadLang loadLang = new LoadLang();
            loadLang.Load();
            if (Config.Instance.PHPSESSID == null || Config.Instance.PHPSESSID == "")
            {
                Console.WriteLine($"{Lang.Instance.PHPSESSIDEmpty}");
                Console.ReadKey();
                return -1;
            }
            RKStat rKStat = new RKStat();
            if (!rKStat.PHPSessionCorrect())
            {
                Console.WriteLine($"{Lang.Instance.PHPSESSIDWrong}");
                Console.ReadKey();
                return -1;
            }
            Console.WriteLine($"{Lang.Instance.PHPSESSIDCorrect}\n{Lang.Instance.DownloadProfileStart}");
            PlayerList playerList = new PlayerList();
            rKStat.CreatePlayerNameList(playerList.GetPlayerList());   
            rKStat.CreatDataPlayers();
            PlayerDataSave playerDataSave = new PlayerDataSave(rKStat.playersData);
            playerDataSave.SaveArmyGeneral();
            playerDataSave.SaveAllDataPlayer();
            Console.WriteLine($"{Lang.Instance.DownloadCompleted}");
            Console.ReadKey();
            return 0;
        }
    }
}
