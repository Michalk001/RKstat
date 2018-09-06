using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

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

        List<string> playersName = new List<string>();

    

        public RKStat(string phpsessid)
        {
           

        }
        public void CreatePlayerNameList(string players)
        {
            playersName = players.Split(',').ToList();
        }
       

        
        public  void CreatDataPlayers()
        {

            Thread thread = new Thread(() =>
            {
                foreach (var item in playersName.Skip(playersName.Count() / 2))
                {
                    DataOfPlayer dataOfPlayer = new DataOfPlayer();
                    var d = dataOfPlayer.GetDataOfPlayer(item);
                    if (d != null)
                        playersData.Add(d);
                    Thread.Sleep(200);
                }
                
            });         
            Thread thread2 = new Thread(() =>
            {
                foreach (var item in playersName.Take((playersName.Count() / 2)))
                {
                    DataOfPlayer dataOfPlayer = new DataOfPlayer();
                    var d = dataOfPlayer.GetDataOfPlayer(item);
                    if (d != null)
                        playersData.Add(d);
                    Thread.Sleep(200);
                    }
            });
            
            thread.Start();
            thread2.Start();
            
            while (!(thread.ThreadState == ThreadState.Stopped) || !(thread2.ThreadState == ThreadState.Stopped)) ;


            }


        }
}
