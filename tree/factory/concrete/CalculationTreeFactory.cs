using general_tree.model;
using general_tree.tree.builder;
using general_tree.tree.iterator;
using general_tree.tree.visitor;
using general_tree.tree.visitor.calculation;
using general_tree.tree.visitor.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace general_tree.tree.factory.concrete
{
    /**
     * Concrete factory responsible for building an Calculation node general tree
     */
    public class CalculationTreeFactory : GeneralTreeFactory
    {
        private class CalculationVisitorFactory : VisitorFactory<Calculation>
        {
            public CalculationVisitorFactory() : base()
            {
                visitorMap["print"] = new CalculationPrintVisitor();
            }
        }

        public GeneralTree<T> getTree<T>() where T:class
        {
            TreeBuilder<Calculation> builder = new CalculationTreeBuilder();

            return
                (GeneralTree<T>)builder
                .insertWith(Constants.InsertStrategies.ENTITY_INSERT_STRATEGY)
                .deleteWith(Constants.DeleteStrategies.SIMPLE_DELETE_STRATEGY)
                .findWith(Constants.FinderStrategies.ENTITY_FINDER_STRATEGY)
                .traverseWith(IteratorFactory<Entity>.PRE_ORDER)
                .withVisitorCommandFactory(new CalculationVisitorFactory())
                .build();
        }
    }
}
