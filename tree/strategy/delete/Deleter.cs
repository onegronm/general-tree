using general_tree.tree.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.strategy.delete
{
    public abstract class Deleter<T>
    {
        public abstract List<Node<T>> delete(Node<T> root, Node<T> target, IEnumerable<Node<T>> iterator);
    }
}
