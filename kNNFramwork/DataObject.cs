using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNNFramework
{
    public class DataObject
    {
        List<double> _Data;
        string _Class;

        public List<double> Data { get => _Data; }
        public string Class { get => _Class; }

        public DataObject(string line, char seperator)
        {
            _Data = new List<double>();
            string[] splittedLine = line.Split(seperator);
            for(int i = 0; i < (splittedLine.Length-1); i++)
            {
                try
                {
                    _Data.Add(double.Parse(splittedLine[i], CultureInfo.InvariantCulture));
                }
                catch
                {
                    _Data.Add(0);
                }
                
            }
            _Class = splittedLine[splittedLine.Length - 1];
            
        }

        
    }
}
