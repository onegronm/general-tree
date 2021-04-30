using general_tree.tree.node;
using System.Collections.Generic;


namespace general_tree.tree.strategy.delete
{
    public abstract class Deleter<T>
    {
        public abstract List<Node<T>> delete(Node<T> root, Node<T> target, IEnumerable<Node<T>> iterator);
    }
}
