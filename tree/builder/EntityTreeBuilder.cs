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
    public class EntityTreeBuilder : TreeBuilder<Entity>
    {
        public EntityTreeBuilder()
        {
            finderMap[Constants.FinderStrategies.ENTITY_FINDER_STRATEGY] = new SimpleFinder<Entity>(new StringToEntityComparer());
            deleterMap[Constants.DeleteStrategies.SIMPLE_DELETE_STRATEGY] = new SimpleDeleteStrategy<Entity>();
            inserterMap[Constants.InsertStrategies.ENTITY_INSERT_STRATEGY] = new EntityInserterStrategy<Entity>(finderMap[Constants.FinderStrategies.ENTITY_FINDER_STRATEGY]);
        }
    }
}
