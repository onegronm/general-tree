using System;
using System.Collections.Generic;


namespace general_tree.tree.visitor
{
    public class VisitorFactory<T>
    {
        protected Dictionary<string, Visitor<T>> visitorMap 
            = new Dictionary<string, Visitor<T>>();

        /**
         * Factory method
         * @param commandRequest
         * @return
         */
        public Visitor<T> makeVisitor(string commandRequest)
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
