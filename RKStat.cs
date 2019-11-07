using HtmlAgilityPack;
using RKstat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RKstat
{
    public enum TypeFieldNumber
    {
        Active = 2,
        Kingdom = 4,
        Province = 5,
        City = 6,
        Level = 12,
        FaithPoints=15,
        Money = 16,
        WayScience=13,
        Condition = 17,
        Workshop = 21,
    }

    class RKStat
    {
          

        public List<Player> playersData = new List<Player>();

        List<string> playerNames = new List<string>();

    
        public void CreatePlayerNameList(string players)
        {
            playerNames = players.Split(',').ToList();
        }
       
        private List<string> PlayersURL(string url, List<string> names)
        {
            List<string> tmp = new List<string>();
            foreach(var item in names)
            {
                tmp.Add(url + "="+ item);
            }
            return tmp;
        }
        
        public void CreatDataPlayers()
        {



            HTTPClient.src.Model.Get clientGet = new HTTPClient.src.Model.Get();
            clientGet.Delay = 500;
            clientGet.AddHeader("Accept-Encoding", "gzip, deflate, br");
            clientGet.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            clientGet.ContentType = "application/x-www-form-urlencoded";
            clientGet.SetCookie("PHPSESSID", Config.Instance.PHPSESSID, Config.Instance.UrlGame);
            var tasks = clientGet.GetAsync(PlayersURL(Config.Instance.UrlProfile, playerNames));

            Task.WaitAll(tasks);
            
            foreach(var item in tasks.Result)
            {
                ParseHTML parseHTML = new ParseHTML();
                playersData.Add(parseHTML.GetPlayer(item.Content));
            }
   

        }

        public bool PHPSessionCorrect()
        {
            HTTPClient.src.Model.Get clientGet = new HTTPClient.src.Model.Get();
            clientGet.AddHeader("Accept-Encoding", "gzip, deflate, br");
            clientGet.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            clientGet.ContentType = "application/x-www-form-urlencoded";
            clientGet.SetCookie("PHPSESSID", Config.Instance.PHPSESSID, Config.Instance.UrlGame);
            var tasks = clientGet.GetAsync(PlayersURL(Config.Instance.UrlProfile, new List<string> { "michalk001" }));
            Task.WaitAll(tasks);
            ParseHTML parseHTML = new ParseHTML();
            var d = parseHTML.GetPlayer(tasks.Result[0].Content);
            if (d != null)
                return true;
            return false;
        }

    }
}
