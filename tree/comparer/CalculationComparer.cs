using general_tree.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.comparer
{
    /**
     * Comparison logic used to find a calculation node
     */
    public class CalculationComparer<T> : Comparer<Calculation>
    {       
        public override int Compare(Calculation c1, Calculation c2)
        {
            if (c1 != null && c2 != null && (c1.CalculationId == c2.CalculationId))
            {
                return 0;
            }
            return -1;
        }
    }
}
