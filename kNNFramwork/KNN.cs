using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNNFramework
{
    public class KNN : IClassifier
    {
        int _K = 5;
        DataObject[] _InputData;
 
        public KNN(int k)
        {
            _K = k; 
        }

        public void Train(DataObject[] trainData)
        {
            _InputData = trainData;
        }
            
        public KeyValuePair<string, DataObject>[]  Classifie(DataObject[] objects)
        {
            KeyValuePair<string, DataObject>[] classifiedObjects = new KeyValuePair<string, DataObject>[objects.Length];
            for(int i = 0; i < objects.Length; i++)
            {
                DataObject[] nearestNeighbours = FindNearestNeighbours(objects[i]);
                string predictedClass = ClassifieSingleObject(nearestNeighbours);
                classifiedObjects[i] = new KeyValuePair<string, DataObject>(predictedClass, objects[i]);
            }
            return classifiedObjects;
        }

        private DataObject[] FindNearestNeighbours(DataObject singleObject)
        {
            Dictionary<DataObject, double> distances = new Dictionary<DataObject, double>(_K);
            for (int i = 0; i < _InputData.Length; i++)
            {
                double distance = CalculateDistance(singleObject, _InputData[i]);
                if (distances.Count < _K)
                {
                    distances.Add(_InputData[i], distance);
                    continue;
                }
                foreach (KeyValuePair<DataObject, double> pair in distances)
                {
                    if (distance < pair.Value)
                    {
                        distances.Remove(pair.Key);
                        distances.Add(_InputData[i], distance);
                        break;
                    }
                }
                

            }
            return distances.Keys.ToArray();
        }
        private double CalculateDistance(DataObject newObject, DataObject neighbourObject)
        {
            double distance = 0;
            for (int i = 0; i < newObject.Data.Count; i++)
            {
                distance += Math.Pow((newObject.Data[i] - neighbourObject.Data[i]), 2);
            }
            return Math.Sqrt(distance);
        }

        private string ClassifieSingleObject(DataObject[] nearestNeighbours)
        {
            Dictionary<string, int> foundClasses = new Dictionary<string, int>();
            for(int i = 0; i < nearestNeighbours.Length; i++)
            {
                if(foundClasses.ContainsKey(nearestNeighbours[i].Class))
                {
                    foundClasses[nearestNeighbours[i].Class]++;
                    continue;
                }
                foundClasses.Add(nearestNeighbours[i].Class, 1);
            }

            string predictedClass = "";
            int max = 0;
            foreach(KeyValuePair<string, int> pair in foundClasses)
            {
                if(pair.Value > max)
                {
                    max = pair.Value;
                    predictedClass = pair.Key;
                }
            }
            return predictedClass;
        }


    }
}
