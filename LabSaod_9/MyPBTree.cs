using System;

namespace LabSaod_9
{
    public class MyPBTree<T>
    {
        private DataStructure<T> root;
        private int nodesAmount;
        private T[] elementsToAdd;
        
        public MyPBTree(int nodesAmount)
        {
            root = default;
            this.nodesAmount = nodesAmount;
        }

        public bool add(T[] elements)
        {
            if (elements.Length != nodesAmount)
            {
                return false;
            }
            
            elementsToAdd = elements;
            
            int count = nodesAmount;
            nodesAmount--;

            root = addNodes(count);
            
            nodesAmount = count;

            if (root != default)
            {
                return true;
            }

            return false;
        }

        private DataStructure<T> addNodes(int amount)
        {
            
            if (amount == 0)
            {
                return default;
            }

            DataStructure<T> current = new DataStructure<T>(){Node = elementsToAdd[nodesAmount--]};

            amount--;

            var leftHalf = amount / 2;
            var rightHalf = amount - leftHalf;

            if (leftHalf > 0)
            {
                var nextLeft = addNodes(leftHalf);
                current.LeftDesc = nextLeft;
            }
            if(rightHalf > 0)
            {
                var nextRight = addNodes(rightHalf);
                current.RightDesc = nextRight;
            }

            return current;
        }

        public void printPreorderTraversal()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }
            
            preorderTraversal(root, 0);
        }

        private void preorderTraversal(DataStructure<T> el, int level)
        {
            printIndent(level);
            Console.WriteLine(el.Node.ToString());
            
            if (el.LeftDesc != null)
            {
                preorderTraversal(el.LeftDesc, level + 1);
            }

            if (el.RightDesc != null)
            {
                preorderTraversal(el.RightDesc, level + 1);
            }
        }

        public void printInorderTraversal()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }
            
            inorderTraversal(root, 0);
        }

        private void inorderTraversal(DataStructure<T> el, int level)
        {
            if (el.LeftDesc != null)
            {
                inorderTraversal(el.LeftDesc, level + 1);
            }
            
            printIndent(level);
            Console.WriteLine(el.Node);
            
            
            if (el.RightDesc != null)
            {
                inorderTraversal(el.RightDesc, level + 1);
            }

        }

        public void printReversedInorderTraversal()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }
            reversedInorderTraversal(root, 0);
        }

        private void reversedInorderTraversal(DataStructure<T> el, int level)
        {
            if (el.RightDesc != null)
            {
                reversedInorderTraversal(el.RightDesc, level + 1);
            }
            
            printIndent(level);
            Console.WriteLine(el.Node);
            
            if (el.LeftDesc != null)
            {
                reversedInorderTraversal(el.LeftDesc, level + 1);
            }
        }

        private void printIndent(int indent)
        {
            for (int i = 0; i < indent; i++)
            {
                Console.Write("     ");
            }
        }

        public int getSize()
        {
            return nodesAmount;
        }
    }
}