using general_tree.tree.visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.node
{
    public interface Node<T>
    {
        T value();
        bool isLeaf();
        Node<T> getParent();
        Node<T> getFirstChild();
        Node<T> getSibling();
        void setValue(T value);
        void setParent(Node<T> parent);
        void setFirstChild(Node<T> firstChild);
        void setSibling(Node<T> sibling);
        void insertChild(Node<T> node);
        void insertSibling(Node<T> node);
        void removeChild(Node<T> childToRemove);
        IEnumerable<Node<T>> children();
        void accept(Visitor<T> visitor);
        Node<T> getRoot();
    }
}
