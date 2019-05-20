using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kNNFramework;

namespace kNN
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter filepath: ");
            string path = Console.ReadLine();
            Console.WriteLine("Seperatorcharacter for values:");
            char seperator = Console.ReadKey().KeyChar;
            Console.WriteLine("Number of columns: ");
            int numberOfColumns = Convert.ToInt32(Console.ReadLine());
            string[] dataSets = File.ReadAllLines(path);
            DataObject[] objects = DatasetPreparer.PrepareObjects(dataSets, seperator, numberOfColumns);

            KFold k = new KFold(new KNN(5));
            k.KFoldCrossValidation(objects, 10);
            Console.ReadLine();
            
        }
    }
}
