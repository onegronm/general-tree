using System;


namespace general_tree.tree.comparer
{
    public interface StringToObjectComparer<T>
    {
        bool compare(String key, T value);
    }
}
