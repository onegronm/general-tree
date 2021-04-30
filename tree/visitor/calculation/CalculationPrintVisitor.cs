using general_tree.model;
using general_tree.tree.node;
using System;


namespace general_tree.tree.visitor.calculation
{
    public class CalculationPrintVisitor : Visitor<Calculation>
    {
        public void visit(Node<Calculation> node)
        {
            Calculation calc = node.value();
            if (calc != null)
            {
                Console.WriteLine("CalculationId: " + calc.CalculationId + ", Priority: " + calc.Priority);
            }
        }
    }
}
