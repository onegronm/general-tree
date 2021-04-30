using general_tree.tree.node;
using System.Collections.Generic;


namespace general_tree.tree.iterator.command
{
    public class PreorderCommand<T> : IteratorCommand<T>
    {
        public IEnumerable<Node<T>> execute(Node<T> root)
        {
            return new PreOrderIterator<T>(root);
        }
    }
}
