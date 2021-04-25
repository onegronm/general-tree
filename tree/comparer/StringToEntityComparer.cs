using general_tree.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.comparer
{
    /**
     * Comparer that allows finding an entity node with a string key
     */
    public class StringToEntityComparer : StringToObjectComparer<Entity>
    {
        public bool compare(String find, Entity e)
        {
            try
            {
                int entityId = int.Parse(find);
                return entityId == e.EntityId ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}