using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree
{
    public class Constants
    {
        public class FinderStrategies
        {
            public static readonly String ENTITY_FINDER_STRATEGY = "Entity Finder Strategy";
            public static readonly String CALCULATION_FINDER_STRATEGY = "Calculation Finder Strategy";
        }

        public class InsertStrategies
        {
            public static readonly String ENTITY_INSERT_STRATEGY = "Entity Insert Strategy";
            public static readonly String CALCULATION_INSERT_STRATEGY = "Calculation Insert Strategy";
        }

        public class DeleteStrategies
        {
            public static readonly String SIMPLE_DELETE_STRATEGY = "Simple Delete Strategy";
        }
    }
}
