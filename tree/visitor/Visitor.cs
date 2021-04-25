using general_tree.tree.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.visitor
{
    public interface Visitor<T>
    {
        void visit(Node<T> node);
    }
}
