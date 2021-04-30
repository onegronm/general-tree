using general_tree.model;
using general_tree.tree.comparer;
using general_tree.tree.strategy.delete;
using general_tree.tree.strategy.find;
using general_tree.tree.strategy.insert;

namespace general_tree.tree.builder
{
    public class EntityTreeBuilder : TreeBuilder<Entity>
    {
        public override void setStrategies()
        {
            finderMap.Add(Constants.FinderStrategies.ENTITY_FINDER_STRATEGY, new SimpleFinder<Entity>(
                new StringToEntityComparer(),
                iteratorFactory.iterator(this.root, this.traverseRequest)));
            deleterMap.Add(Constants.DeleteStrategies.SIMPLE_DELETE_STRATEGY, new SimpleDeleteStrategy<Entity>());
            inserterMap.Add(Constants.InsertStrategies.ENTITY_INSERT_STRATEGY, new EntityInserterStrategy<Entity>(finderMap[Constants.FinderStrategies.ENTITY_FINDER_STRATEGY]));
        
            this.finder = finderMap[Constants.FinderStrategies.ENTITY_FINDER_STRATEGY];
            this.deleter = deleterMap[Constants.DeleteStrategies.SIMPLE_DELETE_STRATEGY];
            this.inserter = inserterMap[Constants.InsertStrategies.ENTITY_INSERT_STRATEGY];
        }
    }
}
