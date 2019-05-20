using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNNFramework
{
    public class ConfusionMatrix
    {
        int[,] _Matrix;
        List<string> _Classes;

        public int TP { get; private set; } = 0;
        public int FP { get; private set; } = 0;
        public int TN { get; private set; } = 0;
        public int FN { get; private set; } = 0;

        public double Accuracy { get; private set; }
        public double Precision { get; private set; }
        public double Recall { get; private set; }
        public double FScore { get; private set; }

        public void Evaluate(KeyValuePair<string, DataObject>[] classifiedData, string checkIfItIs)
        {
            _Classes = new List<string> { };

            if (classifiedData == null)
            {
                return;
            }

            foreach (KeyValuePair<string, DataObject> pair in classifiedData) 
            {
                string actualClass = pair.Value.Class;
                if (!_Classes.Contains(actualClass.ToLower()))
                {
                    _Classes.Add(actualClass.ToLower());
                }
            }

            _Matrix = new int[_Classes.Count, _Classes.Count];

            for (int i = 0; i < classifiedData.Length; i++)
            {
                if (classifiedData[i].Key == null || classifiedData[i].Value.Class == null)
                    continue;
                int x = _Classes.IndexOf(classifiedData[i].Value.Class.ToLower());
                int y = _Classes.IndexOf(classifiedData[i].Key.ToLower());
                _Matrix[x, y] += 1;
            }

            int Tpos = _Classes.IndexOf(checkIfItIs.ToLower());

            for (int y = 0; y < _Classes.Count; y++)
            {
                for (int x = 0; x < _Classes.Count; x++)
                {
                    if (x == y)
                    {
                        if (x == Tpos)
                            TP += _Matrix[x, y];
                        else
                            TN += _Matrix[x, y];
                    }
                    else
                    {
                        if (x == Tpos)
                            FP += _Matrix[x, y];
                        else
                            FN += _Matrix[x, y];
                    }
                }
            }
            Accuracy = (TP + TN) / (double)(TP + TN + FP + FN);
            Precision = TP / (double)(TP + FP);
            Recall = TP / (double)(TP + FN);
            FScore = (2 * Recall * Precision) / (Recall + Precision);
        }

        public void PrintMatrix()
        {
            Console.WriteLine("\nhorizontal: actual\nvertical: predicted");
            foreach (var item in _Classes)
            {
                Console.Write("\t" + item);
            }
            for (int y = 0; y < _Classes.Count; y++)
            {
                Console.Write("\n" + _Classes[y]);
                for (int x = 0; x < _Classes.Count; x++)
                {
                    Console.Write("\t" + _Matrix[x, y]);
                }
            }
            Console.WriteLine();
        }

        public void PrintMetrics()
        {
            if (_Matrix == null)
            {
                Console.WriteLine("\n\nNo Data to be evaluated");
                return;
            }

            //Console.WriteLine("TP: " + TP + "\tTN: " + TN + "\tFP: " + FP + "\tFN: " + FN);
            Console.WriteLine("\n\nAccuracy: " + Accuracy);
            Console.WriteLine("Precision: " + Precision);
            Console.WriteLine("Recall: " + Recall);
            Console.WriteLine("FScore: " + FScore);
        }
    }
}
