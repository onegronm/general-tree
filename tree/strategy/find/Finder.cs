using general_tree.tree.node;
using System;
using System.Collections.Generic;


namespace general_tree.tree.strategy.find
{
    public abstract class Finder<T>
    {
        public abstract Node<T> find(T value, Comparer<T> comparer, IEnumerable<Node<T>> iterator);
        public abstract Node<T> find(string key, IEnumerable<Node<T>> iterator);
        public abstract Node<T> find(string key);
    }
}
