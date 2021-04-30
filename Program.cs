using general_tree.model;
using general_tree.tree;
using general_tree.tree.factory.abstractFactory;
using general_tree.tree.node;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace general_tree
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
             * This section creates an 'Entity' hierarchy where nodes are processed in any order (pre order is chosen by default)
             */
            TreeAbstractFactory treeAbsFactory = new TreeAbstractFactory();
            GeneralTree<Entity> entities = treeAbsFactory.CreateTree<Entity>(TreeAbstractFactory.ENTITY_PRE_ORDER);

            // adding nodes
            Entity entity1 = new Entity(1, "Test Entity 1", -1);
            Entity entity2 = new Entity(2, "Test Entity 2", 1);
            Entity entity3 = new Entity(3, "Test Entity 3", 1);
            Entity entity4 = new Entity(4, "Test Entity 4", 2);
            Entity entity5 = new Entity(5, "Test Entity 5", 2);
            Entity entity6 = new Entity(6, "Test Entity 6", 3);
            Entity entity7 = new Entity(7, "Test Entity 7", 3);
            Entity root = new Entity(8, "Root Entity", 3);

            entities.addChild(entity1);
            entities.addChild(entity2);
            entities.addChild(entity3);
            entities.addChild(entity4);
            entities.addChild(entity5);
            entities.addChild(entity6);
            entities.addChild(entity7);
            entities.addChild(root);
            entities.setRoot(entities.find("8"));
            
            // printing nodes
            entities.print();
            Console.WriteLine("\n");

            // finding nodes
            Console.WriteLine(entities.find("1").value().EntityName);
            Console.WriteLine(entities.find("3").value().EntityName);
            Console.WriteLine(entities.find("5").value().EntityName);
            Console.WriteLine("\n");
            
            // finding a node and printing the children
            var children = entities.getChildren(entities.find("1"));
            foreach (Node<Entity> e in children)
            {
                if (e.value() != null)
                    Console.WriteLine(e.value().EntityName);
            }
            Console.WriteLine("\n");

            // Demonstrate deleting a node and its children
            List<Node<Entity>> deletedNodes = entities.delete("1");
            foreach (Node<Entity> node in deletedNodes)
            {
                if (node.value() != null)
                {
                    Console.WriteLine(node.value().EntityName);
                }
            }

            Console.WriteLine("\n");

            /**
             * This section creates a 'Calculation' hierarchy where nodes are processed by priority (level order). Nodes of the same priority exist in the same level
             * Although it's a different complex type, we can use the same general tree structure but define different strategies in the builder
             */
            GeneralTree<Calculation> calcTree = treeAbsFactory.CreateTree<Calculation>(TreeAbstractFactory.CALCULATION);

            try
            {
                string filePath = "C:\\Data\\Code\\repos\\onegronm\\general-tree\\calculations.json";
                using (StreamReader r = new StreamReader(filePath))
                {
                    List<Calculation> calcs = new List<Calculation>();
                    string json = r.ReadToEnd();
                    calcs = JsonConvert.DeserializeObject<List<Calculation>>(json);
                    foreach(Calculation c in calcs)
                    {
                        calcTree.addChild(c);
                    }
                }
            } catch (Exception e)
            {
                Logger.Log(e);
            }

            calcTree.print();

            Console.Read();
        }
    }
}
