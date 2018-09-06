using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RKstat
{
    class FileOperation
    {
        private StreamReader fileRead;
        private StreamWriter fileWrite;
        private FileStream file;
        private string path;
        

        public void SetPath(string filePath)
        {
            path = filePath;

        }
        public void Open()
        {
            file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fileRead = new StreamReader(file, Encoding.UTF8);
            fileWrite = new StreamWriter(file, Encoding.UTF8);
            
        }
        public void Save(List<string> data)
        {
          
            foreach(var item in data)
            {
                fileWrite.WriteLine(item);
                fileWrite.Flush();
                
            }
        }
        
        public List<string> Read()
        {

            List<string> data = new List<string>();
            while (!fileRead.EndOfStream)
            {
                data.Add(fileRead.ReadLine());
            }

            return data;
            
        }
    }
}
