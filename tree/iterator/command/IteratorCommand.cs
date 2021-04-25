using general_tree.tree.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.iterator.command
{
    public interface IteratorCommand<T>
    {
        /**
         * Uses the command pattern to create an iterator
         */
        IEnumerable<Node<T>> execute(Node<T> root);
    }
}
