using general_tree.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.comparer
{
    /**
     * Comparer that allows finding a calculation node with a string key
     */
    public class StringToCalculationComparer : StringToObjectComparer<Calculation> 
    {
        public bool compare(String key, Calculation c)
        {
            try
            {
                int calculationId = int.Parse(key);
                return calculationId == c.CalculationId ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
