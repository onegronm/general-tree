using general_tree.tree.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.strategy.delete
{
    public class SimpleDeleteStrategy<T> : Deleter<T> 
    {

        public override List<Node<T>> delete(Node<T> root, Node<T> target, IEnumerable<Node<T>> iterator)
        {
            List<Node<T>> deletedNodes = new List<Node<T>>();
            Node<T> parent = target.getParent();
            parent.removeChild(target);

            //while (iterator.GetEnumerator().MoveNext())
            //{
            //    deletedNodes.Add(iterator.GetEnumerator().Current);
            //}

            foreach(Node<T> node in iterator)
            {
                deletedNodes.Add(node);
            }

            return deletedNodes;
        }
    }
}
