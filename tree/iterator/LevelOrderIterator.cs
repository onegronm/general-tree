using general_tree.tree.node;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace general_tree.tree.iterator
{
    /**
     * The level order iterator
     */
    public class LevelOrderIterator<T> : IEnumerable<Node<T>> {
        Node<T> root;
        int level = 0;
        List<Stack<Node<T>>> levels = new List<Stack<Node<T>>>();

        public LevelOrderIterator(Node<T> root)
        {
            this.root = root;
            helper(this.root, level);
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            Stack<Node<T>> curLevel = levels[level];

            if(curLevel.Count == 0)
            {
                yield break;
            }

            Node<T> temp = curLevel.Pop();

            // process child nodes for the next level
            if (temp.getFirstChild() != null)
            {
                helper(temp.getFirstChild(), level + 1);

                if (curLevel.Count == 0)
                {
                    level++;
                }
            }

            yield return temp;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /*
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Node<T> Current
        {
            get
            {
                try
                {
                    Stack<Node<T>> curLevel = levels[level];

                    Node<T> temp = curLevel.Pop();

                    // process child nodes for the next level
                    if (temp.getFirstChild() != null)
                    {
                        helper(temp.getFirstChild(), level + 1);

                        if (curLevel.Count == 0)
                        {
                            level++;
                        }
                    }

                    return temp;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n" + e.InnerException + "\n" + e.StackTrace);
                    throw e;
                }
            }
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            return levels[level].Count > 0;
        }

        public void Reset()
        {
            levels.Clear();
            level = 0;
        }
        */

        private void helper(Node<T> node, int level)
        {
            // start at the current level
            if (levels.Count == level)
            {
                levels.Add(new Stack<Node<T>>());
            }

            // append the current node
            levels[level].Push(node);

            // add sibling nodes
            Node<T> siblingTemp = node.getSibling();
            while (siblingTemp != null)
            {
                levels[level].Push(siblingTemp);
                siblingTemp = siblingTemp.getSibling();
            }
        }
    }
}
