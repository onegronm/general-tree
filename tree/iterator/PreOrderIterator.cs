using general_tree.tree.node;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.iterator
{
    /**
     * The level order iterator
     */
    public class PreOrderIterator<T> : IEnumerable<Node<T>>
    {
        protected Node<T> root;
        protected int depth = 0;
        protected Stack<Node<T>> stack = new Stack<Node<T>>();

        public PreOrderIterator()
        {
        }

        public PreOrderIterator(Node<T> root)
        {
            this.root = root;
            stack.Push(this.root);
        }

        public virtual IEnumerator<Node<T>> GetEnumerator()
        {
            Node<T> temp = stack.Peek();

            if (stack.Count == 0)
            {
                yield break;
            }

            root = stack.Pop();

            if (root.getFirstChild() != null)
            {
                stack.Push(root.getFirstChild());
                depth++;
            }

            if (root.getSibling() != null)
            {
                stack.Push(root.getSibling());
            }

            yield return temp;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
