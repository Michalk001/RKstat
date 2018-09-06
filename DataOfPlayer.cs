using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RKstat
{
    class DataOfPlayer
    {
        ParseHtml htmlParser = new ParseHtml();
        HtmlDocument htmlDoc;
        public DataOfPlayer()
        {
            htmlParser.SetCookie("PHPSESSID", Config.Instance.PHPSESSID, Config.Instance.UrlGame);
        }

        public Player GetDataOfPlayer(string name)
        {
            Player player = new Player();
            htmlParser.SetDomain(Config.Instance.UrlProfile + name);
            htmlDoc = htmlParser.GetHtml();

            var playerNameTmp = htmlDoc.DocumentNode.SelectNodes("//div[@class='FPContentBlocInfosElem']");
            if (playerNameTmp == null)
                return null;
            string playerName = name;
            player.Name = (playerName.Remove(0, playerName.IndexOf(':') + 1)).TrimStart(' ');
            player.Province = GetValueByParagraf((int)TypeFieldNumber.Province);
            player.City = GetValueByParagraf((int)TypeFieldNumber.City);
            player.Level = GetValueByParagraf((int)TypeFieldNumber.Level).Split(" ")[1];
            player.WayScience = GetValueByParagraf((int)TypeFieldNumber.WayScience);
            player.FaithPoints = GetValueByParagraf((int)TypeFieldNumber.FaithPoints).Split(" ")[0];
            string money = GetValueByParagraf((int)TypeFieldNumber.Money).Remove(0, 9);

            player.Money = money.Remove(money.Length - 7).Replace(" ", "");

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
                player.Condition = (GetValueByParagraf((int)TypeFieldNumber.Condition).Split(' ').Last()).TrimEnd('.');
                
            }
            string armyName = GetArmyName(GetOfficeLists());
            if (armyName != null)
            {
                player.General = "true";
                player.ArmyName = armyName;
            }
            else
            {
                player.General = "false";
                player.ArmyName = "brak";
            }


            return player;
        }
        private string IsActive(string rawDate)
        {

            ParseDate parseDate = new ParseDate();
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
        List<string> GetOfficeLists()
        {
            var tmp = htmlDoc.DocumentNode
                .SelectNodes("/html[1]/body[1]/div[1]/div[2]/div[3]/div[1]/div[1]/div[1]/p[4]/ul[1]/li");
            if (tmp == null)
                return null;


            return tmp.Select(x => x.InnerText).ToList();
        }

        string GetArmyName(List<string> offices)
        {
            if (offices == null)
                return null;
            foreach (var item in offices)
            {

                if (item.ToLower().Contains("dowódca"))
                {
                    return item.Replace("dowódca armii ", "").Replace("\"", "");
                }

            }
            return null;
        }
        string GetValueByParagraf(int number)
        {
            var tmp = htmlDoc.DocumentNode.SelectNodes("//div[@class='FPContentBlocInfosElem']")[0]
                          .SelectNodes("//p")[number].InnerText;
            return System.Net.WebUtility.HtmlDecode((tmp.Remove(0, tmp.IndexOf(":") + 1)).TrimStart(' '));
        }
    }
}
