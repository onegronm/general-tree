# Generic General Tree

## Purpose

The purpose of this project is to create a reusable general tree data structure. A general tree is a tree in which each node can have zero or many child nodes. There are many examples of entities with relationships that can be represented by a general tree such as directories, family trees, business entities, geohashes, among many others. Development teams might find themselves writing tree structures from scratch when there's complex parent/child relationships that need to be traversed in a certain order and that require unique operations to be processed for each node. This may lead to tightly coupled solutions with little reusability. This library provides an extensible framework for general trees where the client can combine different strategies to achieve the desired functionality for insertion, deletion, location, and many other tasks.

## Architecture

### tree.factory
Contains the abstract factory or the "factory of factories" responsible for creating the general tree concrete factory. Each  general tree implementation has its own factory and builder. An easy method for storing the different implementations when you want to reuse those across your application.

### tree.builder
The builder responsible for creating the implementation of the General Tree with the given strategies for inserting, deleting, traversing, finding, and visiting nodes.

### tree.comparer
This namespace contains the different comparers used for locating nodes in the general tree. Each comparer typically maps to the primary key of the concrete type belonging to the tree.

### tree.iterator
This namespace contains the implementations for the iterators supporting the different traversal methods in the tree. Only pre-order, children only, and level-order are implemented. The IteratorFactory uses the command pattern to return the instance of the iterator when executing the iterator command.

### tree.node
Contains the interface and implementation of the general tree using the first child/next sibling representation.

### tree.strategy
Contains the implementations of the strategies for inserting, finding, and deleting nodes. Each strategy is subclassed from one of the abstract base classes (Inserter, Finder, and Deleter). The abstract classes contain the preProcessor() and postProcessor() hook methods for implementing additional validation of the strategy operations.

### tree.visitor
Uses the visitor, iterator, and bridge patterns to support operations that are unique to the concrete type of the tree in a loosely coupled manner while respecting the traversal order requested by the client. Currently, only print visitors are implemented but this could be expanded to support any operation needed on the nodes.

## How to use it
The application can be tested simply by running it as a command line tool or through the debugger in Visual Studio. In order to extend this library, complete the following steps:

1. If you want to implement new strategies, do this by extending the Finder, Deleter, and Inserter abstract classes in the 'strategy' namespace.
```java
public class EntityInserterStrategy<T> : Inserter<Entity>
{
    Comparer<Entity> comparer;

    public EntityInserterStrategy(Finder<Entity> finder)
        :base(finder)
    {
        comparer = new EntityParentComparer<Entity>();
    }

    public override Node<Entity> add(Node<Entity> root, Node<Entity> target, Entity value)
    {
        if (target == null)
        {
            target = root;
        }

        Node<Entity> newNode = new NodeImpl<Entity>(target, null, null, value);

        if (target.getFirstChild() == null)
        {
            target.insertChild(newNode);
        }
        else
            target.insertSibling(newNode);

        return newNode;
    }
}
```

2. If you want to implement new iterators, do this by extending IEnumerable<Node<T>> and updating the iterator factory in the 'iterator' namespace.
```java
public class PreOrderIterator<T> : IEnumerable<Node<T>>
{
    protected Node<T> root;
    protected int depth = 0;
    protected Stack<Node<T>> stack = new Stack<Node<T>>();

    public PreOrderIterator()
    {
    }

    public PreOrderIterator(Node<T> root)
    {
        this.root = root;
        stack.Push(this.root);
    }

    public virtual IEnumerator<Node<T>> GetEnumerator()
    {
        // ...
    }
```

3. Create the comparers used to locate nodes in the tree. The comparer must implement Comparer<T>. You can also create a comparer by extending StringToObjectComparer<T> which allows comparing a node of type T with a string value.
```java
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
            Logger.Log(ex);
            return false;
        }
    }
}
```

4. Define visitors for your general tree in the 'visitor' namespace. The visitors define operations on node<T>.
```java
public class EntityPrintVisitor : Visitor<Entity> {
    public StringBuilder sb;

    public EntityPrintVisitor()
    {
        sb = new StringBuilder();
    }

    public void visit(Node<Entity> node)
    {
        Entity entity = node.value();
        if (entity != null)
        {
            Console.WriteLine("EntityId: " + entity.EntityId + ", Name: " + entity.EntityName + ", ParentId: " + entity.ParentId);
        }
    }
}
```

5. Create a concrete factory in the factory.concrete namespace. This factory will contain the builder where the different strategies are declared. The builder within the factory method will return the abstract tree upon calling build(). You may use any of the existing strategies or make new ones.
```java
public class EntityTreeFactory : GeneralTreeFactory
{
    /**
        * Invokes the builder with entity strategies
        * @return
        */
    public GeneralTree<T> getTree<T>() where T: class
    {
        try
        {
            TreeBuilder<Entity> builder = new EntityTreeBuilder();

            return
                (GeneralTree<T>)builder
                .traverseWith(IteratorFactory<Entity>.PRE_ORDER)
                .insertWith(Constants.InsertStrategies.ENTITY_INSERT_STRATEGY)
                .deleteWith(Constants.DeleteStrategies.SIMPLE_DELETE_STRATEGY)
                .findWith(Constants.FinderStrategies.ENTITY_FINDER_STRATEGY)                    
                .withVisitorCommandFactory(new EntityVisitorFactory())
                .build();
        }
        catch (Exception e)
        {
            Logger.Log(e);
        }
        return null;
    }
}
```

6. Create the visitor factory inside of the tree factory above. All the visitor factory does is store a map with a command as a key (e.g., "print") and the visitor implementation as the value.
```java
public class EntityTreeFactory : GeneralTreeFactory
{
    private class EntityVisitorFactory : VisitorFactory<Entity>
    {
        public EntityVisitorFactory() : base()
        {
            visitorMap["print"] = new EntityPrintVisitor();
        }
    }
```

7. Update the GeneralTreeAbstractFactory map with the factory created above.
```java
public abstract class GeneralTreeAbstractFactory {
    public final static String ENTITY_HIERARCHY = "Entity Hierarchy General Tree";
    public final static String ENTITY_HIERARCHY_LEVEL_ORDER = "Entity Hierarchy Level Order General Tree";
    public final static String CALCULATION = "Calculations General Tree";

    Map<String, GeneralTreeFactory> factoryMap = new HashMap<>();

    public GeneralTreeAbstractFactory() {
        factoryMap.put(ENTITY_HIERARCHY, new EntityHierarchyFactory());
        factoryMap.put(ENTITY_HIERARCHY_LEVEL_ORDER, new EntityHierarchyLevelOrderFactory());
        factoryMap.put(CALCULATION, new CalculationsFactory());
        /**
         * Define more trees here...
         */
    }

    public abstract GeneralTree CreateTree();
}
```

8. In the client application, instantiate and perform operations on the tree:
```java
TreeAbstractFactory treeAbsFactory = new TreeAbstractFactory();
GeneralTree<Entity> entities = treeAbsFactory.CreateTree<Entity>(TreeAbstractFactory.ENTITY_PRE_ORDER);

entities.addChild(new Entity(1, "Test Entity 1", -1));
// ...
```
