using general_tree.model;
using general_tree.tree.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.visitor.entity
{
    public class EntityPrintVisitor : Visitor<Entity> {
        public StringBuilder sb;

        public EntityPrintVisitor()
        {
            sb = new StringBuilder();
        }

        public void visit(Node<Entity> node)
        {
            Entity entity = node.value();
            if (entity != null)
            {
                Console.WriteLine("EntityId: " + entity.EntityId + ", Name: " + entity.EntityName + ", ParentId: " + entity.ParentId);
            }
        }
    }
}
