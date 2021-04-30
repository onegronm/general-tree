using general_tree.model;
using System;


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
                if (e != null)
                {
                    int entityId = int.Parse(find);
                    return entityId == e.EntityId ? true : false;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }
    }
}