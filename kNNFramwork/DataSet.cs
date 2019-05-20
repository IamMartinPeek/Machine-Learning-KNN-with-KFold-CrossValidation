using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNNFramework
{
    public class DataSet
    {
        DataObject[] _TestData;
        DataObject[] _TrainData;

        public DataObject[] TestData { get => _TestData;}
        public DataObject[] TrainData { get => _TrainData; }

        public DataSet(DataObject[] data, int testPercentage)
        {
            int testSampleSize = Convert.ToInt32((double)data.Length / 100 * testPercentage)-1;
            _TestData = new DataObject[testSampleSize];
            _TrainData = new DataObject[data.Length - _TestData.Length];
            SplitData(data);
        }

        private void SplitData(DataObject[] data)
        {

            ShuffleData(data);
            for(int i = 0; i < data.Length; i++)
            {
                if(i < _TestData.Length)
                {
                    _TestData[i] = data[i];
                    continue;
                }
                _TrainData[i - _TestData.Length] = data[i];
            }
            
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


    }
}
