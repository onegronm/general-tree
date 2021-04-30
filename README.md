# Final Project

## Purpose

The purpose of this project is to create a reusable general tree data structure. A general tree is a tree in which each node can have zero or many child nodes. There are many examples of entities with relationships that can be represented by a general tree such as directories, family trees, business entities, geohashes, among many others. Development teams might find themselves writing tree structures from scratch when there's complex parent/child relationships that need to be traversed in a certain order and that require unique operations to be processed for each node. This may lead to tightly coupled solutions with little reusability. This library provides an extensible framework for general trees where the client can mix and match "lego blocks" to generate a tree with the desired functionality for insertion, deletion, location, and many other tasks.

## Architecture

### tree.factory
Contains the abstract factory or the "factory of factories" responsible for creating the general tree concrete factory. Each  general tree implementation has its own factory and builder. An easy method for storing the different implementations when you want to reuse those across your application.

### tree.builder
The builder responsible for creating the implementation of the General Tree with the given strategies for inserting, deleting, traversing, finding, and visiting nodes.

### tree.comparator
This package contains the different comparators used for locating nodes in the general tree. Each comparator typically maps to the primary key of the concrete type belonging to the tree.

### tree.iterable
A facade that attaches a node to an iterator. This allows the client code to easily traverse the children of any node in the tree.

### tree.iterator
This package contains the implementations for the iterators supporting the different traversal methods in the tree. Only pre-order and level-order are implemented. The IteratorFactory uses the command pattern to return the instance of the iterator when executing the iterator command.

### tree.node
Contains the interface and implementation of the general tree using first child/next sibling representation.

### tree.strategy
Contains the implementations of the strategies for inserting, finding, and deleting nodes. Each strategy subclasses from one of the abstract base classes (Inserter, Finder, and Deleter). The abstract classes contain the preProcessor() and postProcessor() hook methods for implementing additional validation of the strategy operations.

### tree.visitor
Uses the visitor, iterator, and bridge patterns to support operations that are unique to the concrete type of the tree in a loosely coupled manner while respecting the traversal order requested by the client. Currently, only print visitors are implemented but this could be expanded to support any operation with more visitors.

## How to use it
The application can be tested simply by running it as a command line tool or through the debugger in Eclipse or IntelliJ. In order to extend this library, complete the following steps:

1. If you want to implement new strategies, do this by extending the Finder, Deleter, and Inserter abstract classes in the 'strategy' package.
```java
public class EntityHierarchyInserterStrategy<T extends EntityHierarchy> extends Inserter<T> {

    Comparator<T> comparator;

    public EntityHierarchyInserterStrategy(Finder finder) {
        super(finder);
        this.comparator = new ParentComparator<>();
    }

    @Override
    public Node<T> add(Node<T> root, Node<T> target, T value, Iterator<Node<T>> iterator) {
        if (target == null){
            target = root;
        }

        Node<T> newNode = new NodeImpl<>(target, null, null, value, iteratorFactory);

        if (target.getFirstChild() == null) {
            target.insertChild(newNode);
        }
        else
            target.insertSibling(newNode);

        return newNode;
    }
```

2. If you want to implement new iterators, do this by extending Iterator<Node<T>> and updating the iterator factory in the 'iterator' package.

```java
public class LevelOrderIterator implements Iterator<Node> {
    Node root;
    int level = 0;
    List<Stack<Node>> levels = new ArrayList<>();

    public LevelOrderIterator(Node root){
        this.root = root;
        helper(this.root, level);
    }

    @Override
    public boolean hasNext() {
        return !levels.get(level).isEmpty();
    }

    @Override
    public Node next() {
        Stack<Node> curLevel = levels.get(level);

        Node temp = curLevel.pop();

        // process child nodes for the next level
        if (temp.getFirstChild() != null) {
            helper(temp.getFirstChild(), level+1);

            if (curLevel.isEmpty()) {
                level++;
            }
        }

        return temp;
    }
```

3. Create the comparators used to locate nodes in the tree. The comparator must extend Comparator<T> and override the compare(T 1, T2) method. You can also create a comparator by extending StringToObjectComparator<T> which allows locating a node of type T using a string key.

```java
public class CompareStringToToEntityId implements StringToObjectComparator<EntityHierarchy> {

    public boolean compare(String find, EntityHierarchy e) {
        try {
            Integer entityId = Integer.parseInt(find);
            return entityId == e.EntityId ? true : false;
        } catch (Exception ex) {
            return false;
        }
    }
}
```

4. Define visitors for your general tree in the 'visitor' package. The visitors define operations supported on each node of type T.

```java
public class PrintVisitor implements Visitor<EntityHierarchy> {
    public StringBuilder sb;

    public PrintVisitor() {
        sb = new StringBuilder();
    }

    @Override
    public void visit(Node<EntityHierarchy> node) {
        EntityHierarchy entity = node.value();
        if (entity != null) {
            System.out.println("EntityId: " + entity.EntityId + ", Name: " + entity.EntityName + ", ParentId: " + entity.ParentId);
        }
    }
}
```

5. Create a concrete factory in the factory.concrete package. This factory will contain the builder where the different strategies are determined. The builder within the factory method will return the abstract tree upon calling build(). You may use any of the existing strategies or code new ones.

6. Create the visitor factory inside of the tree factory above. All the visitor factory does is store a map with a command as a key (e.g., "print") and the visitor implementation as the value.

```java
public class EntityHierarchyFactory implements GeneralTreeFactory {
    public static class EntityVisitorFactory extends VisitorFactory {
        public EntityVisitorFactory() {
            super();
            visitorMap.put("print", new PrintVisitor());
        }
    }

    /**
     * Invokes the builder with entity strategies
     * @return
     */
    @Override
    public GeneralTree<EntityHierarchy> getTree() {
        try {
            TreeBuilder<EntityHierarchy> builder = new TreeBuilder<>();

            return
                    builder
                            .insertWith(Constants.InsertStrategies.ENTITY_INSERT_STRATEGY)
                            .deleteWith(Constants.DeleteStrategies.SIMPLE_DELETE_STRATEGY)
                            .findWith(Constants.FinderStrategies.ENTITY_FINDER_STRATEGY)
                            .traverseWith(IteratorFactory.PRE_ORDER)
                            .withVisitorCommandFactory(new EntityVisitorFactory())
                            .build();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
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
GeneralTreeAbstractFactory treeAbsFactory = new MyConcreteGeneralTreeFactory();
GeneralTree<COMPLEX_TYPE> myTree = treeAbsFactory.CreateTree();
myTree.addChild(new COMPLEX_TYPE())
...
```



## Interesting challenges and key takeaways
The most challenging aspect of this project was finding a way to incorporate the different patterns in the most elegant way possible to achieve the modularity, loose coupling, and reusability desired. At the start of the project, I put together a list of patterns I wanted to use understanding how they would help me achieve my objective but it was challenging to translate those into code. I overcame this challenge by reviewing lecture notes, sample code, and by keeping in mind the goal to create a tree data structure that could be configurable and reusable. I was surprised at how easy it was to implement the first child next sibling tree structure as I only had to deal with two pointers. This really speaks to the simplicity this data structure can bring to the code. 
