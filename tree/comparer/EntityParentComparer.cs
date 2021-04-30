using general_tree.model;
using System.Collections.Generic;


namespace general_tree.tree.comparer
{
    /**
     * Comparer used to locate in the tree the parent of the node being added
     * @param <T>
     */
    public class EntityParentComparer<T> : Comparer<Entity>
    {
        public override int Compare(Entity e1, Entity e2)
        {
            if (e1 == null || e2 == null)
            {
                return -1;
            }
            if (e1.ParentId == e2.EntityId)
            {
                return 0;
            }
            return -1;
        }
    }
}
