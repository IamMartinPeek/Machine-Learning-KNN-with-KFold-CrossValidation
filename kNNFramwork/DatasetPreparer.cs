using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNNFramework
{
    public static class DatasetPreparer
    {
        public static DataObject[] PrepareObjects(string[] dataSets, char seperator, int numberOfColumns)
        {
           
            List<DataObject> preparedObjects = new List<DataObject>();
            for(int i = 0; i < dataSets.Length; i++)
            {
                if(dataSets[i].Split(seperator).Length < numberOfColumns)
                {
                    continue;
                }
                preparedObjects.Add(new DataObject(dataSets[i], seperator));
            }
            return preparedObjects.ToArray(); ;
        }



       

    
        
    }
}
