using general_tree.tree.node;
using System.Collections.Generic;


namespace general_tree.tree.strategy.delete
{
    public class SimpleDeleteStrategy<T> : Deleter<T> 
    {
        public override List<Node<T>> delete(Node<T> root, Node<T> target, IEnumerable<Node<T>> iterator)
        {
            List<Node<T>> deletedNodes = new List<Node<T>>();
            Node<T> parent = target.getParent();
            parent.removeChild(target);

            foreach(Node<T> node in iterator)
            {
                deletedNodes.Add(node);
            }

            return deletedNodes;
        }
    }
}
