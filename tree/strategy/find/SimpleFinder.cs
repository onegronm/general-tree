using general_tree.tree.comparer;
using general_tree.tree.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.strategy.find
{
    public class SimpleFinder<T> : Finder<T> {
        StringToObjectComparer<T> stringToObjectComparator;

        public SimpleFinder(StringToObjectComparer<T> stringToObjectComparator)
        {
            this.stringToObjectComparator = stringToObjectComparator;
        }

        /**
         * Find a node in tree by generic value using the supplied iterator
         * @param value
         * @param comparer
         * @param iterator
         * @return
         */
        public override Node<T> find(T value, Comparer<T> comparer, IEnumerable<Node<T>> iterator)
        {
            //while (iterator.MoveNext())
            //{
            //    Node<T> element = iterator.Current;
            //    if (comparer.Compare(value, element.value()) == 0)
            //    {
            //        return element;
            //    };
            //}
            foreach(Node<T> node in iterator)
            {
                if (comparer.Compare(value, node.value()) == 0)
                {
                    return node;
                };
            }
            return null;
        }

        /**
         * Find a node by a string key using the supplied iterator
         * @param value
         * @param iterator
         * @return
         */
        public override Node<T> find(String value, IEnumerable<Node<T>> iterator)
        {
            //while (iterator.MoveNext())
            //{
            //    Node<T> element = iterator.Current;
            //    if (stringToObjectComparator.compare(value, element.value()))
            //    {
            //        return element;
            //    };
            //}

            foreach(Node<T> node in iterator)
            {
                if (stringToObjectComparator.compare(value, node.value()))
                {
                    return node;
                };
            }
            return null;
        }
    }
}
