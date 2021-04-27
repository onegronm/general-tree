using general_tree.tree.iterator;
using general_tree.tree.node;
using general_tree.tree.strategy.delete;
using general_tree.tree.strategy.find;
using general_tree.tree.strategy.insert;
using general_tree.tree.visitor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace general_tree.tree
{
    public class GeneralTreeImpl<T> : GeneralTree<T>, IEnumerable<Node<T>> {
        Inserter<T> inserter;
        Deleter<T> deleter;
        Finder<T> finder;
        Node<T> root;
        IteratorFactory<T> iteratorFactory;
        VisitorFactory<T> visitorFactory;

        public GeneralTreeImpl(Node<T> root, Inserter<T> inserter, Deleter<T> deleter, Finder<T> finder, IteratorFactory<T> iteratorFactory, VisitorFactory<T> visitorFactory)
        {
            this.inserter = inserter;
            this.deleter = deleter;
            this.finder = finder;
            this.root = root;
            this.iteratorFactory = iteratorFactory;
            this.visitorFactory = visitorFactory;
        }

        public Node<T> getRoot()
        {
            return this.root;
        }

        public void setRoot(Node<T> newRoot)
        {
            if (newRoot != null)
            {
                // 1. remove the new root as child of its existing parent
                Node<T> newRootParent = newRoot.getParent();
                newRootParent.removeChild(newRoot);
                newRoot.setParent(null);

                // 2. take the existing root and add it as a child of the new root
                Node<T> curRoot = this.root;
                newRoot.insertChild(curRoot.getFirstChild());
                newRoot.insertChild(curRoot.getSibling());

                this.root = newRoot;
            }
        }

        public void setRoot(T value)
        {
            Node<T> newRoot = inserter.addNode(root, null, value, this.iterator());
            setRoot(newRoot);
        }

        public Node<T> find(T value, Comparer<T> comparator)
        {
            return finder.find(value, comparator, this.iterator());
        }

        public Node<T> find(String key)
        {
            return finder.find(key, this.iterator());
        }

        public void addChild(T value, Comparer<T> comparator)
        {
            Node<T> target = find(value, comparator);
            inserter.addNode(root, target, value, this.iterator());
        }

        public void addChild(T value)
        {
            Node<T> target = find(value, this.inserter.getComparator());
            inserter.addNode(root, target, value, this.iterator());
        }

        public void print()
        {
            Visitor<T> visitor = visitorFactory.makeVisitor("print");

            IEnumerable<Node<T>> it = this.iterator();

            foreach (Node<T> node in it)
            {
                node.accept(visitor);
            }
        }

        public List<Node<T>> delete(String key)
        {
            List<Node<T>> deletedNodes = null;
            Node<T> target = find(key);
            if (target != null)
            {
                deletedNodes = deleter.delete(root, target, this.iterator());
            }
            return deletedNodes;
        }

        public List<Node<T>> delete(Node<T> target)
        {
            List<Node<T>> deletedNodes = null;
            if (target != null)
            {
                deletedNodes = deleter.delete(root, target, this.iterator());
            }
            return deletedNodes;
        }

        public void clear()
        {
            // TO-DO:
        }

        public IEnumerable<Node<T>> iterator()
        {
            return iteratorFactory.iterator(this.root);
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            return iterator().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
