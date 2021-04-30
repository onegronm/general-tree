using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.model
{
    public class Entity : BaseModel
    {
        public int EntityId;
        public String EntityName;
        public int ParentId;

        public Entity(int id, String name, int parentId)
        {
            EntityId = id;
            EntityName = name;
            ParentId = parentId;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            Entity that = (Entity)obj;
            return EntityId == that.EntityId;
        }
    }
}
