using general_tree.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.factory
{
    public interface GeneralTreeFactory
    {
        GeneralTree<T> getTree<T>() where T : class;
    }
}
