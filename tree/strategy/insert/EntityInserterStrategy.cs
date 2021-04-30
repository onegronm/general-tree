using general_tree.model;
using general_tree.tree.comparer;
using general_tree.tree.node;
using general_tree.tree.strategy.find;
using System.Collections.Generic;


namespace general_tree.tree.strategy.insert
{
    public class EntityInserterStrategy<T> : Inserter<Entity>
    {
        Comparer<Entity> comparer;

        public EntityInserterStrategy(Finder<Entity> finder)
            :base(finder)
        {
            comparer = new EntityParentComparer<Entity>();
        }

        public override Node<Entity> add(Node<Entity> root, Node<Entity> target, Entity value)
        {
            if (target == null)
            {
                target = root;
            }

            Node<Entity> newNode = new NodeImpl<Entity>(target, null, null, value);

            if (target.getFirstChild() == null)
            {
                target.insertChild(newNode);
            }
            else
                target.insertSibling(newNode);

            return newNode;
        }

        public override Comparer<Entity> getComparator()
        {
            return comparer;
        }
    }
}
