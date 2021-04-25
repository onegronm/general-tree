using general_tree.tree.node;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.iterator
{
    /**
     * Iterator that returns the direct children of a node
     */
    public class PreOrderChildrenIterator<T> : PreOrderIterator<T>
    {

        public PreOrderChildrenIterator(Node<T> root)
        {
            this.root = root;
            if (this.root.getFirstChild() != null)
            {
                this.stack.Push(this.root.getFirstChild());
                this.depth++;
            }
        }

        public override IEnumerator<Node<T>> GetEnumerator()
        {
            if (stack.Count == 0)
            {
                yield break;
            }

            Node<T> temp = stack.Peek();

            if (stack.Count > 0)
            {
                root = stack.Pop();

                if (root.getSibling() != null)
                {
                    stack.Push(root.getSibling());
                }
                else
                {
                    depth++;
                }
            }

            yield return temp;
        }
    }
}
