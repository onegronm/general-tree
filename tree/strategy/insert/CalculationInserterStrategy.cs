using general_tree.model;
using general_tree.tree.comparer;
using general_tree.tree.iterator;
using general_tree.tree.node;
using general_tree.tree.strategy.find;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.strategy.insert
{
    /**
     * The insert strategy for the calculation node tree
     * @param <T>
     */
    public class CalculationsInserterStrategy<T> : Inserter<Calculation>
    {
        Comparer<Calculation> comparer;

        public CalculationsInserterStrategy(Finder<Calculation> finder)
            :base(finder)
        {
            this.comparer = new CalculationComparer<Calculation>();
        }

        public override Node<Calculation> add(Node<Calculation> root, Node<Calculation> target, Calculation value, IEnumerable<Node<Calculation>> iterator)
        {
            if (target == null)
            {
                target = root;
            }

            Node<Calculation> newNode = new NodeImpl<Calculation>(target, null, null, value, iterator);

            if (value.ChildComponents.Count() > 0)
            {
                // the new node is added as a child of each component
                // a child node is a calculation that has the parent in its formula
                foreach (ChildComponent component in value.ChildComponents)
                {
                    Node<Calculation> parentNode = finder.find(component.id.ToString(), iterator);
                    if (parentNode != null && newNode.value().Priority > parentNode.value().Priority)
                    {
                        parentNode.insertChild(newNode);
                    }
                }
            }
            else
            {
                // if there are no child components, then add to the root node (highest priority calculations)
                if (target.getFirstChild() == null)
                {
                    target.insertChild(newNode);
                }
                else
                    target.insertSibling(newNode);
            }

            return newNode;
        }

        public override Comparer<Calculation> getComparator()
        {
            return comparer;
        }
    }
}
