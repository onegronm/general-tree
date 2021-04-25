using general_tree.model;
using general_tree.tree.comparer;
using general_tree.tree.strategy.delete;
using general_tree.tree.strategy.find;
using general_tree.tree.strategy.insert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace general_tree.tree.builder
{
    public class CalculationTreeBuilder : TreeBuilder<Calculation>
    {
        public CalculationTreeBuilder()
        {
            finderMap[Constants.FinderStrategies.CALCULATION_FINDER_STRATEGY] = new SimpleFinder<Calculation>(new StringToCalculationComparer());
            deleterMap[Constants.DeleteStrategies.SIMPLE_DELETE_STRATEGY] = new SimpleDeleteStrategy<Calculation>();
            inserterMap[Constants.InsertStrategies.CALCULATION_INSERT_STRATEGY] = new CalculationsInserterStrategy<Calculation>(finderMap[Constants.FinderStrategies.CALCULATION_FINDER_STRATEGY]);
        }
    }
}
