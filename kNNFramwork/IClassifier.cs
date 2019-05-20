using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNNFramework
{
    public interface IClassifier
    {
        KeyValuePair<string, DataObject>[] Classifie(DataObject[] objects);
        void Train(DataObject[] trainData);
    }
}
