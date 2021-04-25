using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.comparer
{
    public interface StringToObjectComparer<T>
    {
        bool compare(String key, T value);
    }
}
