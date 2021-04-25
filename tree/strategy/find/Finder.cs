using general_tree.tree.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.strategy.find
{
    public abstract class Finder<T>
    {
        public abstract Node<T> find(T value, Comparer<T> comparer, IEnumerable<Node<T>> iterator);
        public abstract Node<T> find(String value, IEnumerable<Node<T>> iterator);
    }
}
