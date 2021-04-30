using general_tree.model;
using general_tree.tree.builder;
using general_tree.tree.iterator;
using general_tree.tree.visitor;
using general_tree.tree.visitor.calculation;

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
                .traverseWith(IteratorFactory<Calculation>.LEVEL_ORDER)
                .insertWith(Constants.InsertStrategies.CALCULATION_INSERT_STRATEGY)
                .deleteWith(Constants.DeleteStrategies.SIMPLE_DELETE_STRATEGY)
                .findWith(Constants.FinderStrategies.CALCULATION_FINDER_STRATEGY)
                .withVisitorCommandFactory(new CalculationVisitorFactory())
                .build();
        }
    }
}
