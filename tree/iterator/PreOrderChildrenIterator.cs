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
            Node<T> node = stack.Peek();

            while (stack.Count > 0)
            {
                node = stack.Pop();

                if (node.getSibling() != null)
                {
                    stack.Push(node.getSibling());
                }
                else
                {
                    depth++;
                }

                yield return node;
            }
        }
    }
}
