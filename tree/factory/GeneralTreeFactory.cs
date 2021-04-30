namespace general_tree.tree.factory
{
    public interface GeneralTreeFactory
    {
        GeneralTree<T> getTree<T>() where T : class;
    }
}
