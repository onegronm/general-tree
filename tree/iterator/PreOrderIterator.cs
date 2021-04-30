using general_tree.tree.node;
using System.Collections;
using System.Collections.Generic;


namespace general_tree.tree.iterator
{
    /**
     * The pre order iterator
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
            stack.Clear();
            stack.Push(this.root);

            Node<T> node = stack.Peek();

            while (stack.Count > 0)
            {
                node = stack.Pop();

                if (node.getFirstChild() != null)
                {
                    stack.Push(node.getFirstChild());
                    depth++;
                }

                if (node.getSibling() != null)
                {
                    stack.Push(node.getSibling());
                }

                yield return node;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
