using general_tree.tree.iterator;
using general_tree.tree.node;
using general_tree.tree.strategy.delete;
using general_tree.tree.strategy.find;
using general_tree.tree.strategy.insert;
using general_tree.tree.visitor;
using System;
using System.Collections.Generic;

namespace general_tree.tree.builder
{
    public abstract class TreeBuilder<T>
    {
        protected string insertRequest;
        protected string deleteRequest;
        protected string findRequest;
        protected string traverseRequest;
        protected Inserter<T> inserter;
        protected Deleter<T> deleter;
        protected Finder<T> finder;
        protected VisitorFactory<T> visitorFactory;
        protected IteratorFactory<T> iteratorFactory = new IteratorFactory<T>();
        protected Dictionary<string, Deleter<T>> deleterMap = new Dictionary<string, Deleter<T>>();
        protected Dictionary<string, Finder<T>> finderMap = new Dictionary<string, Finder<T>>();
        protected Dictionary<string, Inserter<T>> inserterMap = new Dictionary<string, Inserter<T>>();
        protected Node<T> root = new NodeImpl<T>();

        public TreeBuilder<T> insertWith(string insertRequest)
        {
            this.insertRequest = insertRequest;
            return this;
        }

        public TreeBuilder<T> deleteWith(string deleteRequest)
        {
            this.deleteRequest = deleteRequest;
            return this;
        }

        public TreeBuilder<T> traverseWith(string traversalRequest)
        {
            this.traverseRequest = traversalRequest;
            this.iteratorFactory.setDefaultOrder(traversalRequest);
            return this;
        }

        public TreeBuilder<T> findWith(string findRequest)
        {
            this.findRequest = findRequest;
            return this;
        }

        public TreeBuilder<T> withVisitorCommandFactory(VisitorFactory<T> factory)
        {
            this.visitorFactory = factory;
            return this;
        }
        
        public GeneralTree<T> build()
        {
            setStrategies();

            if (this.inserter == null) { throw new Exception("Missing inserter strategy."); }
            if (this.deleter == null) { throw new Exception("Missing delete strategy."); }
            if (this.finder == null) { throw new Exception("Missing finder strategy."); }
            if (this.traverseRequest == null) { throw new Exception("Missing traversal strategy."); }
            if (this.visitorFactory == null) { throw new Exception("Missing visitor factory."); }

            GeneralTreeImpl<T> tree = new GeneralTreeImpl<T>(
                    root,
                    inserter,
                    deleter,
                    finder,
                    iteratorFactory.iterator(root, this.traverseRequest),
                    visitorFactory
                    );

            return tree;
        }

        public abstract void setStrategies();
    }
}
