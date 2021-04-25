﻿using general_tree.tree.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.iterator.command
{
    public class PreorderChildrenCommand<T> : IteratorCommand<T>
    {
        public IEnumerable<Node<T>> execute(Node<T> root)
        {
            return new PreOrderChildrenIterator<T>(root);
        }
    }
}
