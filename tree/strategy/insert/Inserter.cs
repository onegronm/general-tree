using general_tree.tree.node;
using general_tree.tree.strategy.find;
using System.Collections.Generic;

namespace general_tree.tree.strategy.insert
{
    public abstract class Inserter<T>
    {
        protected Finder<T> finder;

        public Inserter(Finder<T> finder)
        {
            this.finder = finder;
        }

        public Node<T> addNode(Node<T> root, Node<T> target, T value)
        {
            preProcessor();
            Node<T> node = add(root, target, value);
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
        public abstract Node<T> add(Node<T> root, Node<T> target, T value);
    }
}
