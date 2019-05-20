using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNNFramework
{
    public class KFold
    {
        IClassifier _Classifier;

        public KFold(IClassifier classifier)
        {
            _Classifier = classifier;
        }

        public void KFoldCrossValidation(DataObject[] data, int k)
        {
            ShuffleData(data);
            List<DataObject[]> splittedParts = SplitArrayInKParts(data, k);
            List<KeyValuePair<string, DataObject>> predictedObjects = new List<KeyValuePair<string, DataObject>>();
            System.Diagnostics.Stopwatch a = new System.Diagnostics.Stopwatch();
            for (int i = 0; i < splittedParts.Count; i++)
            {
                
                 DataObject[] trainData = GetTrainDataForKFoldValidation(splittedParts, i);
                _Classifier.Train(trainData);
                a.Start();
                KeyValuePair<string, DataObject>[] predicted = _Classifier.Classifie(splittedParts[i]);
                a.Stop();
                predictedObjects.AddRange(predicted);
            }
            ConfusionMatrix cm = new ConfusionMatrix();
            cm.Evaluate(predictedObjects.ToArray(), "");
            cm.PrintMatrix();
            cm.PrintMetrics();
            Console.WriteLine("Elapsed time: " + a.Elapsed);
        }

        private DataObject[] GetTrainDataForKFoldValidation(List<DataObject[]> splittedParts, int indexOfTestData)
        {
            List<DataObject> trainData = new List<DataObject>();
            for(int i = 0; i < splittedParts.Count; i++)
            {
                if(i == indexOfTestData)
                {
                    continue;
                  
                }
                trainData.AddRange(splittedParts[i]);
            }
            return trainData.ToArray();
        }

        private void ShuffleData(DataObject[] data)
        { 
            Random rnd = new Random();
            int n = data.Length;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n);
                DataObject value = data[k];
                data[k] = data[n];
                data[n] = value;
            }
        }

        private List<DataObject[]> SplitArrayInKParts(DataObject[] data, int k)
        {
            List<DataObject[]> splittedData = new List<DataObject[]>();
            int testSize = Convert.ToInt32((double)data.Length / k)-1;
            for (int i = 0; i < data.Length; i += testSize)
            {
                DataObject[] part = new DataObject[testSize];
                if (i+testSize > data.Length)
                {
                    break;
                }
                Array.Copy(data, i, part, 0, testSize);
                
                
                splittedData.Add(part);
            }
            return splittedData;
        }

    }
}
