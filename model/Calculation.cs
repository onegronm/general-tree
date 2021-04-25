using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.model
{
    public class Calculation : Model
    {
        public int CalculationId;
        public int Priority;
        public IEnumerable<ChildComponent> ChildComponents;

        public Calculation(int calculationId, int priority)
        {
            CalculationId = calculationId;
            Priority = priority;
            ChildComponents = new List<ChildComponent>();
        }
    }
}
