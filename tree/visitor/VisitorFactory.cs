using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.visitor
{
    public class VisitorFactory<T>
    {
        protected Dictionary<String, Visitor<T>> visitorMap 
            = new Dictionary<string, Visitor<T>>();

        /**
         * Factory method
         * @param commandRequest
         * @return
         */
        public Visitor<T> makeVisitor(String commandRequest)
        {
            Visitor<T> visitor = visitorMap[commandRequest];
            if (visitor != null)
            {
                return visitor;
            }
            return null;
        }
    }
}
