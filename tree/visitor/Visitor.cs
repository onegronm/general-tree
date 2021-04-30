using general_tree.tree.node;


namespace general_tree.tree.visitor
{
    public interface Visitor<T>
    {
        void visit(Node<T> node);
    }
}
