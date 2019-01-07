using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RKstat.Models
{
    class ParseHTML
    {

        public Player GetPlayer(string content)
        {
            Player player = new Player();
            HTTPClient.src.Model.ParseHTML parseHTML = new HTTPClient.src.Model.ParseHTML();
            parseHTML.LoadHtml(content);

            var playerNameTmp = parseHTML.GetStringFromNode("//*[@id='FPcontentBlocInfos']/div[2]/h1");
            if (playerNameTmp == null)
                return null;
            string playerName = playerNameTmp;
            player.Name = (playerName.Remove(0, playerName.IndexOf(':') + 1)).TrimStart(' ');
            player.Province = parseHTML.GetStringFromNode("//*[@id='FPcontentBlocInfos']/div[3]/div[2]/p[2]/text()");
            player.City = parseHTML.GetStringFromNode("//*[@id='FPcontentBlocInfos']/div[3]/div[2]/p[3]/text()");
            player.Level = parseHTML.GetStringFromNode("//*[@id='FPcontentBlocInfos']/div[6]/p[1]/text()");
            player.WayScience = parseHTML.GetStringFromNode("//*[@id='FPcontentBlocInfos']/div[6]/p[2]/text()");
            player.FaithPoints = parseHTML.GetStringFromNode("//*[@id='FPcontentBlocInfos']/div[6]/p[4]/text()").Split(" ")[0];
            string money = parseHTML.GetStringFromNode("//*[@id='FPcontentBlocInfos']/div[6]/p[5]/text()");

            player.Money = money;

            player.Active = IsActive(parseHTML.GetStringFromNode("//*[@id='FPcontentBlocInfos']/div[2]/p[3]"));

            if ((parseHTML.GetStringFromNode("//*[@id='FPcontentBlocInfos']/div[8]/p[1]")) != "")
                player.Workshop = (parseHTML.GetStringFromNode("//*[@id='FPcontentBlocInfos']/div[8]/p[1]")).Split(' ').ToList()[2].TrimEnd('.');
            else
                player.Workshop = "brak";

            if (parseHTML.GetStringFromNode("//*[@id='FPcontentBlocInfos']/div[6]/p[6]/span") == "")
            {
                player.Condition = "żyje";
            }
            else
            {
                player.Condition = (parseHTML.GetStringFromNode("//*[@id='FPcontentBlocInfos']/div[6]/p[6]/span").Split(' ').Last()).TrimEnd('.');


            }
            string armyName = null;
            if (armyName != null)
            {
                player.General = "true";
                player.ArmyName = armyName;
            }
            else
            {
                player.General = "false";
                player.ArmyName = "NoN";
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
        /*List<string> GetOfficeLists()
        {
            var tmp = htmlDoc.DocumentNode
                .SelectNodes("/html[1]/body[1]/div[1]/div[2]/div[3]/div[1]/div[1]/div[1]/p[4]/ul[1]/li");
            if (tmp == null)
                return null;


            return tmp.Select(x => x.InnerText).ToList();
        }*/

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
    }
}
