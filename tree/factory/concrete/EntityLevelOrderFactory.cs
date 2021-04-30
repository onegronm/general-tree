using general_tree.model;
using general_tree.tree.builder;
using general_tree.tree.iterator;
using general_tree.tree.visitor;
using general_tree.tree.visitor.entity;
using System;

namespace general_tree.tree.factory.concrete
{
    public class EntityLevelOrderFactory : GeneralTreeFactory
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
        public GeneralTree<T> getTree<T>() where T : class
        {
            try
            {
                TreeBuilder<Entity> builder = new EntityTreeBuilder();

                return
                    (GeneralTree<T>)builder
                    .insertWith(Constants.InsertStrategies.ENTITY_INSERT_STRATEGY)
                    .deleteWith(Constants.DeleteStrategies.SIMPLE_DELETE_STRATEGY)
                    .findWith(Constants.FinderStrategies.ENTITY_FINDER_STRATEGY)
                    .traverseWith(IteratorFactory<Entity>.LEVEL_ORDER)
                    .withVisitorCommandFactory(new EntityVisitorFactory())
                    .build();
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
            return null;
        }
    }
}
