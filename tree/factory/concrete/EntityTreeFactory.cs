﻿using general_tree.model;
using general_tree.tree.builder;
using general_tree.tree.factory.abstractFactory;
using general_tree.tree.iterator;
using general_tree.tree.visitor;
using general_tree.tree.visitor.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.factory.concrete
{
    /**
     * Concrete factory responsible for building an Entity node general tree
     */
    public class EntityTreeFactory : GeneralTreeFactory
    {
        private class EntityVisitorFactory : VisitorFactory<Entity>
        {
            public EntityVisitorFactory() : base()
            {
                visitorMap["print"] = new EntityPrintVisitor();
            }
        }

        /**
         * Invokes the builder with entity strategies
         * @return
         */
        public GeneralTree<T> getTree<T>() where T: class
            {
                try
                {
                    TreeBuilder<Entity> builder = new TreeBuilder<Entity>();

                    return
                        (GeneralTree<T>)builder
                        .insertWith(Constants.InsertStrategies.ENTITY_INSERT_STRATEGY)
                        .deleteWith(Constants.DeleteStrategies.SIMPLE_DELETE_STRATEGY)
                        .findWith(Constants.FinderStrategies.ENTITY_FINDER_STRATEGY)
                        .traverseWith(IteratorFactory<Entity>.PRE_ORDER)
                        .withVisitorCommandFactory(new EntityVisitorFactory())
                        .build();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n" + e.InnerException + "\n" + e.StackTrace);
                }
                return null;
            }
    }
}