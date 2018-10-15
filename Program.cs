using System;
using System.Threading;
using IniFile;
namespace RKstat
{
    class Program
    {
        static int Main(string[] args)
        {

          
            IniFile.IniFile iniFile = new IniFile.IniFile("config.txt");
            var sectionINI = iniFile.Get("Config");

            SetConfig.Instance.ConfigINI(iniFile.Get("Config"));
            IniFile.IniFile langFile = new IniFile.IniFile(Config.Instance.PathLang);
            SetConfig.Instance.LangINI(langFile.Get("ENG"));
           
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
