using general_tree.tree.iterator;
using general_tree.tree.node;
using general_tree.tree.strategy.find;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace general_tree.tree.strategy.insert
{
    public abstract class Inserter<T>
    {
        protected Finder<T> finder;
        protected IteratorFactory<T> iteratorFactory;

        public Inserter(Finder<T> finder)
        {
            this.finder = finder;
        }

        public Node<T> addNode(Node<T> root, Node<T> target, T value, IEnumerable<Node<T>> iterator)
        {
            preProcessor();
            Node<T> node = add(root, target, value, iterator);
            postProcessor();
            return node;
        }

        /**
         * The pre processor hook method. Use this to validate the input, for example.
         * @return
         */
        public virtual bool preProcessor()
        {
            return true;
        }

        /**
         * The post processor hook method. Use this to check for circular references, for example.
         * @return
         */
        public virtual bool postProcessor()
        {
            return false;
        }

        public abstract Comparer<T> getComparator();
        public abstract Node<T> add(Node<T> root, Node<T> target, T value, IEnumerable<Node<T>> iterator);
    }
}
