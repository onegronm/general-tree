using general_tree.tree.factory.concrete;
using System;
using System.Collections.Generic;


namespace general_tree.tree.factory.abstractFactory
{
    /**
     * Abstract factory for building the different types of trees (entity, calculation, directory, geohash, etc.)
     */
    public class TreeAbstractFactory
    {
        public readonly static String ENTITY_PRE_ORDER = "Entity Pre Order Tree";
        public readonly static String ENTITY_LEVEL_ORDER = "Entity Level Order Tree";
        public readonly static String CALCULATION = "Calculations Tree";

        Dictionary<String, GeneralTreeFactory> factoryMap
            = new Dictionary<string, GeneralTreeFactory>();

        public TreeAbstractFactory()
        {
            factoryMap[ENTITY_PRE_ORDER] = new EntityTreeFactory();
            factoryMap[ENTITY_LEVEL_ORDER] = new EntityLevelOrderFactory();
            factoryMap[CALCULATION] = new CalculationTreeFactory();
        }

        public GeneralTree<T> CreateTree<T>(String type) where T: class
        {
            GeneralTreeFactory factory;
            factoryMap.TryGetValue(type, out factory);
            GeneralTree<T> tree = factory.getTree<T>();

            return tree;
        }
    }
}
