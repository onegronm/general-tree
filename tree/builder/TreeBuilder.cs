using general_tree.model;
using general_tree.tree.comparer;
using general_tree.tree.iterator;
using general_tree.tree.node;
using general_tree.tree.strategy.delete;
using general_tree.tree.strategy.find;
using general_tree.tree.strategy.insert;
using general_tree.tree.visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace general_tree.tree.builder
{
    public class TreeBuilder<T>
    {
        protected Inserter<T> inserter;
        protected Deleter<T> deleter;
        protected Finder<T> finder;
        protected String traversalRequest;
        protected VisitorFactory<T> visitorFactory;
        protected Dictionary<String, Deleter<T>> deleterMap = new Dictionary<String, Deleter<T>>();
        protected Dictionary<String, Finder<T>> finderMap = new Dictionary<String, Finder<T>>();
        protected Dictionary<String, Inserter<T>> inserterMap = new Dictionary<String, Inserter<T>>();

        public TreeBuilder<T> insertWith(String insertRequest)
        {
            this.inserter = inserterMap[insertRequest];
            return this;
        }

        public TreeBuilder<T> deleteWith(String deleteRequest)
        {
            this.deleter = deleterMap[deleteRequest];
            return this;
        }

        public TreeBuilder<T> traverseWith(String traversalRequest)
        {
            this.traversalRequest = traversalRequest;
            return this;
        }

        public TreeBuilder<T> findWith(String findRequest)
        {
            this.finder = finderMap[findRequest];
            return this;
        }

        public TreeBuilder<T> withVisitorCommandFactory(VisitorFactory<T> factory)
        {
            this.visitorFactory = factory;
            return this;
        }
        
        public GeneralTree<T> build()
        {
            if (this.inserter == null){ throw new Exception("Missing inserter strategy."); }
            if (this.deleter == null){ throw new Exception("Missing delete strategy."); }
            if (this.finder == null){ throw new Exception("Missing finder strategy."); }
            if (this.traversalRequest == null){ throw new Exception("Missing traversal strategy."); }
            if (this.visitorFactory == null){ throw new Exception("Missing visitor factory."); }

            IteratorFactory<T> iteratorFactory = new IteratorFactory<T>(traversalRequest);

            Node<T> root = new NodeImpl<T>(
                    null,
                    null,
                    null,
                    iteratorFactory);

            GeneralTreeImpl<T> tree = new GeneralTreeImpl<T>(
                    root,
                    inserter,
                    deleter,
                    finder,
                    iteratorFactory,
                    visitorFactory
                    );

            return tree;
        }
    }
}
