using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RKstat
{
    class ReadData
    {

        FileStream file;
        public ReadData(string filePath)
        {
            file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
           
        }
        public ReadData()
        {
           

        }
        public void SetPath(string filePath)
        {
            file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);

        }
        public string GetPlayerList()
        {
            byte[] players = new byte[file.Length];
            file.Read(players, 0, (int)file.Length);
            return Encoding.UTF8.GetString(players);
            
        }
    }
}
