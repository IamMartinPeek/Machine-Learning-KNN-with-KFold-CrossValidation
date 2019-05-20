using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConfusionMatrix
{
    public class DatasetReader
    {
        string _FilePath;

        public DatasetReader(string filePath)
        {
            _FilePath = filePath;
        }

        public List<string> GetDatasets()
        {
            List<string> dataSets = new List<string>();
            StreamReader sr = new StreamReader(_FilePath);
            while(!sr.EndOfStream)
            {
                dataSets.Add(sr.ReadLine());
            }
            sr.Close();
            return dataSets;
        }
    }
}
