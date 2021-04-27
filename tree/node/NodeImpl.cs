using general_tree.tree.iterator;
using general_tree.tree.visitor;
using System;
using System.Collections.Generic;


namespace general_tree.tree.node
{
    public class NodeImpl<T> : Node<T>
    {
        /**
         * A general tree using first child/next sibling representation
         * Since each node in a tree can have an arbitrary number of children, and that number is not known in advance, the general tree can be implemented using a first child/next sibling method. Each node will have TWO pointers: one to the leftmost child, and one to the rightmost sibling.
         * Link: https://en.wikipedia.org/wiki/Left-child_right-sibling_binary_tree
         * @param <T>
         */
        private Node<T> parent;
        private Node<T> firstChild;
        private Node<T> nextSibling;
        private T data;
        private IEnumerable<Node<T>> iterator;

        public NodeImpl(Node<T> parent, Node<T> firstChild, Node<T> nextSibling,
            T data, IEnumerable<Node<T>> iterator)
        {
            this.parent = parent;
            this.firstChild = firstChild;
            this.nextSibling = nextSibling;
            this.data = data;
            this.iterator = iterator;
        }

        public NodeImpl(Node<T> parent, Node<T> firstChild, Node<T> nextSibling)
        {
            this.parent = parent;
            this.firstChild = firstChild;
            this.nextSibling = nextSibling;
        }

        public T value()
        {
            return data;
        }

        public bool isLeaf()
        {
            return this.firstChild == null;
        }

        public Node<T> getParent()
        {
            return this.parent;
        }

        public Node<T> getFirstChild()
        {
            return firstChild;
        }

        public Node<T> getSibling()
        {
            return nextSibling;
        }

        public void setValue(T value)
        {
            this.data = value;
        }

        public void setParent(Node<T> parent)
        {
            this.parent = parent;
        }

        public void setFirstChild(Node<T> firstChild)
        {
            if (firstChild != null)
                firstChild.setParent(this);
            this.firstChild = firstChild;
        }

        public void setSibling(Node<T> sibling)
        {
            this.nextSibling = sibling;
        }

        public void insertChild(Node<T> child)
        {
            if (child != null)
            {
                if (this.firstChild == null)
                {
                    this.firstChild = child;
                }
                else if (this.nextSibling == null)
                {
                    this.nextSibling = child;
                }
            }
        }

        public void insertSibling(Node<T> sibling)
        {
            Node<T> firstChild = this.getFirstChild();
            if (firstChild.getSibling() == null)
            {
                firstChild.setSibling(sibling);
            }
            else
            {
                Node<T> currentSibling = firstChild;
                while (currentSibling.getSibling() != null)
                {
                    currentSibling = currentSibling.getSibling();
                }
                currentSibling.setSibling(sibling);
            }
        }

        public void removeChild(Node<T> childToRemove)
        {
            Node<T> firstChildTemp = this.firstChild;
            if (childToRemove.Equals(this.getFirstChild()))
            {
                this.setFirstChild(firstChildTemp.getSibling());
            }
            else
            {
                Node<T> priorSibling = null;
                Node<T> curSibling = this.firstChild.getSibling();
                while (curSibling != null)
                {
                    if (curSibling.Equals(childToRemove))
                    {
                        if (priorSibling != null)
                        {
                            priorSibling.setSibling(curSibling.getSibling());
                        }
                        else
                        {
                            this.firstChild.setSibling(curSibling.getSibling());
                        }
                        break;
                    }
                    priorSibling = curSibling;
                    curSibling = curSibling.getSibling();
                }
            }
        }

        /**
         * Entry point for visitor operations
         * @param visitor
         */
        public void accept(Visitor<T> visitor)
        {
            visitor.visit(this);
        }

        public Node<T> getRoot()
        {
            Node<T> root = this.parent;
            while (root.getParent() != null)
            {
                root = root.getParent();
            }
            return root;
        }

        public IEnumerable<Node<T>> children()
        {
            throw new NotImplementedException();
        }
    }
}