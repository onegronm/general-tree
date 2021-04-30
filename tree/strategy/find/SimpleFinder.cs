using general_tree.tree.comparer;
using general_tree.tree.node;
using System;
using System.Collections.Generic;


namespace general_tree.tree.strategy.find
{
    public class SimpleFinder<T> : Finder<T> {
        private IEnumerable<Node<T>> iterator;
        private StringToObjectComparer<T> stringToObjectComparator;

        public SimpleFinder(StringToObjectComparer<T> stringToObjectComparator, IEnumerable<Node<T>> iterator)
        {
            this.iterator = iterator;
            this.stringToObjectComparator = stringToObjectComparator;
        }

        /**
         * Find a node in tree by string key using the instance's iterator
         * @param value
         * @param comparer
         * @param iterator
         * @return
         */
        public override Node<T> find(string key)
        {
            foreach (Node<T> node in iterator)
            {
                if (stringToObjectComparator.compare(key, node.value()))
                {
                    return node;
                };
            }
            return null;
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
