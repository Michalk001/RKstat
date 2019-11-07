using System;
using System.Threading;
using System.Threading.Tasks;
using IniFile;
using RKstat.Models;

namespace RKstat
{
    class Program
    {

        public static string  SelectMenu()
        {
            string choice = "";
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Logowanie\n2 - Wczytanie PHPSESSID z pliku\n");
                choice = Console.ReadLine();
            } while (choice != "1" && choice != "2");
            return choice;
        }

        public static void Login()
        {
            Console.Clear();
            Console.WriteLine("Podaj nick: ");
            var nick = Console.ReadLine();
            Console.WriteLine("Podaj haslo: ");
            string pass = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            HTTPClient.src.Model.Post post = new HTTPClient.src.Model.Post
            {
                AllowAutoRedirect = false
            };
            post.Delay = 1000;
            post.AddHeader("Accept-Encoding", "gzip, deflate, br");
            post.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            post.ContentType = "application/x-www-form-urlencoded";
            var task = post.PostAsync("https://www.krolestwa.com/ConnexionKC.php", "login="+nick+"&password="+pass);
            Task.WaitAll(task);
            var cookiesHT = task.Result.Headers.GetValues("Set-Cookie");
            HTTPClient.src.Model.Cookies cookies = new HTTPClient.src.Model.Cookies();
            cookies.ParseCookies(cookiesHT);
            var c = cookies.Get("PHPSESSID");
            Config.Instance.PHPSESSID = c;
        }

        static int Main(string[] args)
        {

          
            IniFile.IniFile iniFile = new IniFile.IniFile("config.ini");
            var sectionINI = iniFile.Get("Config");

            SetConfig.Instance.ConfigINI(iniFile.Get("CONFIG"));

            if(SelectMenu() == "1")
            {
                Login();
            }


            IniFile.IniFile langFile = new IniFile.IniFile(Config.Instance.PathLang);
            SetConfig.Instance.LangINI(langFile.Get("LANG"));
           
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
            Console.WriteLine("");
            Console.ReadKey();
            return 0;
        }


    }
}
