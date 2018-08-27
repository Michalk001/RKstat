using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
        HtmlDocument htmlDoc;
        

        public List<Player> playersData = new List<Player>();

        List<string> playersName = new List<string>();

        ParseDate parseDate = new ParseDate();

        ParseHtml htmlParser = new ParseHtml();
        string urlRK = "https://www.krolestwa.com/FichePersonnage.php?login=";
        public RKStat(string phpsessid)
        {
            htmlParser.SetCookie("PHPSESSID", phpsessid, "https://www.krolestwa.com");

        }
        public void CreatePlayerNameList(string players)
        {
            playersName = players.Split(',').ToList();
        }
        void GetDataOfPlayer()
        {
            Player player = new Player();
            string playerName = htmlDoc.DocumentNode.SelectNodes("//div[@class='FPContentBlocInfosElem']")[0]
                                .SelectNodes("//h1")[0].InnerText;

            player.Name = (playerName.Remove(0, playerName.IndexOf(':') + 1)).TrimStart(' ');
            player.Province = GetValueByParagraf((int)TypeFieldNumber.Province).Split(" ")[1];
            player.City = GetValueByParagraf((int)TypeFieldNumber.City);
            player.Level = GetValueByParagraf((int)TypeFieldNumber.Level).Split(" ")[1];
            player.WayScience = GetValueByParagraf((int)TypeFieldNumber.WayScience);
            player.FaithPoints = GetValueByParagraf((int)TypeFieldNumber.FaithPoints).Split(" ")[0];
            string money = GetValueByParagraf((int)TypeFieldNumber.Money).Remove(0,9);

            player.Money = money.Remove(money.Length -7).Replace(" ","");

            player.Active = IsActive(GetValueByParagraf((int)TypeFieldNumber.Active));

            if ((GetValueByParagraf((int)TypeFieldNumber.Workshop)) != "")
                player.Workshop = (GetValueByParagraf((int)TypeFieldNumber.Workshop)).Split(' ').ToList()[2].TrimEnd('.');
            else
                player.Workshop = "brak";

            if (GetValueByParagraf((int)TypeFieldNumber.Condition) == "")
            {
                player.Condition = "żyje";
            }
            else
            {
                player.Condition = (GetValueByParagraf((int)TypeFieldNumber.Condition).Split(' ').ToList()[3]).TrimEnd('.');
            }
            playersData.Add(player);
        }

        private string IsActive(string rawDate)
        {
            

            List<string> tmpDate = rawDate.Split(' ').ToList();

            DateTime dateTime = DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy")).AddDays(-2);
            DateTime parseDateTime = DateTime.Parse(parseDate.GetDate(tmpDate[2], tmpDate[3], tmpDate[4]));

            if (parseDateTime >= dateTime)
            {
                return "true";
            }
            else
                return "false";
            
        }
        public  void CreatDataPlayers()
        {
            foreach(var  item in playersName)
            {
             
                htmlParser.SetDomain(urlRK + item);
                htmlDoc = htmlParser.GetHtml();
                GetDataOfPlayer();         
            }

        }

        string GetValueByParagraf(int number)
        {
            var tmp = htmlDoc.DocumentNode.SelectNodes("//div[@class='FPContentBlocInfosElem']")[0]
                          .SelectNodes("//p")[number].InnerText;
            return System.Net.WebUtility.HtmlDecode((tmp.Remove(0, tmp.IndexOf(":") + 1)).TrimStart(' '));
        }

    }
}
