using general_tree.model;
using general_tree.tree.comparer;
using general_tree.tree.strategy.delete;
using general_tree.tree.strategy.find;
using general_tree.tree.strategy.insert;

namespace general_tree.tree.builder
{
    public class CalculationTreeBuilder : TreeBuilder<Calculation>
    {
        public override void setStrategies()
        {
            finderMap.Add(Constants.FinderStrategies.CALCULATION_FINDER_STRATEGY,
                new SimpleFinder<Calculation>(new StringToCalculationComparer(), iteratorFactory.iterator(this.root, this.traverseRequest)));
            deleterMap.Add(Constants.DeleteStrategies.SIMPLE_DELETE_STRATEGY, new SimpleDeleteStrategy<Calculation>());
            inserterMap.Add(Constants.InsertStrategies.CALCULATION_INSERT_STRATEGY, new CalculationsInserterStrategy<Calculation>(finderMap[Constants.FinderStrategies.CALCULATION_FINDER_STRATEGY]));

            this.finder = finderMap[Constants.FinderStrategies.CALCULATION_FINDER_STRATEGY];
            this.deleter = deleterMap[Constants.DeleteStrategies.SIMPLE_DELETE_STRATEGY];
            this.inserter = inserterMap[Constants.InsertStrategies.CALCULATION_INSERT_STRATEGY];
        }
    }
}
