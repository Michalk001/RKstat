using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RKstat
{
    class SaveData
    {

        FileStream file;
        public SaveData(string filePath)
        {
            file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public void PlayerOfCountry(List<Player> player)
        {
           foreach(var item in player)
            {
                byte[] data = new UTF8Encoding(true)
                    .GetBytes(item.Name + ";" + item.Province + ";" + item.City + ";" + item.Level + ";" + item.FaithPoints + ";" +
                    item.Money + ";" + item.WayScience + ";" + item.Active + ";" + item.Workshop + ";" + item.Condition);
                file.Write(data, 0, data.Length);
                byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                file.Write(newline, 0, newline.Length);
            }
            file.Close();
        }

    }


}
