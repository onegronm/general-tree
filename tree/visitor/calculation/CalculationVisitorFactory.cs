using general_tree.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace general_tree.tree.visitor.calculation
{
    public class CalculationVisitorFactory : VisitorFactory<Calculation>
    {
        public CalculationVisitorFactory() : base()
        {
            visitorMap["print"] = new CalculationPrintVisitor();
        }
    }
}
