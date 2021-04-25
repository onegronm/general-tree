using general_tree.tree.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree
{
    public interface GeneralTree<T>
    {
        /**
         * Clears the tree
         */
        void clear();

        /**
         * Returns the root of the tree
         * @return
         */
        Node<T> getRoot();

        /**
         * Adds a child node with the supplied comparator
         * @param value
         * @param comparator
         */
        void addChild(T value, Comparer<T> comparator);

        /**
         * Adds a child node with the default comparator supplied by the builder
         * @param value
         */
        void addChild(T value);

        /**
         * Find a node by a generic value with the supplied comparator
         * @param value
         * @param comparator
         * @return
         */
        Node<T> find(T value, Comparer<T> comparator);

        /**
         * Find a node by a string key with the comparator supplied by the builder
         * @param key
         * @return
         */
        Node<T> find(String key);

        /**
         * Print all nodes in the tree
         */
        void print();

        /**
         * Set a new root node for the tree
         * @param root
         */
        void setRoot(Node<T> root);

        /**
         * Set a new root node for the tree. The node is created if it doesn't exist
         * @param value
         */
        void setRoot(T value);

        /**
         * Delete a node locating it by a string key
         * @param key
         * @return
         */
        List<Node<T>> delete(String key);

        /**
         * Delete the supplied node object from the tree. Returns the list of nodes removed
         * @param target
         * @return
         */
        List<Node<T>> delete(Node<T> target);

        IEnumerable<Node<T>> iterator();
    }
}
