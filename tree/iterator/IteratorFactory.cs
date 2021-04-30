using general_tree.tree.iterator.command;
using general_tree.tree.node;
using System;
using System.Collections.Generic;


namespace general_tree.tree.iterator
{
    public class IteratorFactory<T>
    {
        public readonly static string PRE_ORDER = "pre-order";
        public readonly static string CHILD_PRE_ORDER = "child-pre-order";
        public readonly static string LEVEL_ORDER = "level-order";

        private Dictionary<string, IteratorCommand<T>> iteratorMap = new Dictionary<string, IteratorCommand<T>>();
        private string defaultOrder;

        public IteratorFactory()
        {
            iteratorMap[LEVEL_ORDER] = new LevelOrderCommand<T>();
            iteratorMap[PRE_ORDER] = new PreorderCommand<T>();
            iteratorMap[CHILD_PRE_ORDER] = new PreorderChildrenCommand<T>();
        }

        public void setDefaultOrder(string request)
        {
            this.defaultOrder = request;
        }

        /**
         * Returns an iterator with the default traversal method
         * @param node
         * @return
         */
        public IEnumerable<Node<T>> iterator(Node<T> node)
        {
            return iterator(node, defaultOrder);
        }

        /**
         * Returns an iterator with the specified traversal method
         * @param node
         * @param traversalOrderRequest
         * @return
         */
        public IEnumerable<Node<T>> iterator(Node<T> node, String traversalOrderRequest)
        {
            IteratorCommand<T> command = iteratorMap[traversalOrderRequest];

            if (command != null)
            {
                // execute the command to create the iterator
                return command.execute(node);
            }
            else
            {
                throw new ArgumentException(traversalOrderRequest + " is not a supported traversal order.");
            }
        }
    }
}
