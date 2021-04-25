using general_tree.tree.iterator.command;
using general_tree.tree.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.iterator
{
    public class IteratorFactory<T>
    {
        public readonly static String PRE_ORDER = "pre-order";
        public readonly static String CHILD_PRE_ORDER = "child-pre-order";
        public readonly static String LEVEL_ORDER = "level-order";

        private Dictionary<String, IteratorCommand<T>> iteratorMap = new Dictionary<string, IteratorCommand<T>>();
        private String defaultOrder;

        public IteratorFactory(String defaultOrder)
        {
            this.defaultOrder = defaultOrder;

            iteratorMap[LEVEL_ORDER] = new LevelOrderCommand<T>();
            iteratorMap[PRE_ORDER] = new PreorderCommand<T>();
            iteratorMap[CHILD_PRE_ORDER] = new PreorderChildrenCommand<T>();
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
