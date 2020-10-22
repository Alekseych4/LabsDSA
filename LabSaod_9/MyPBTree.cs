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

        public void printDirectTraversal()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }
            
            directTraversal(root, 0);
        }

        private void directTraversal(DataStructure<T> el, int level)
        {
            for (int i = 0; i < level; i++)
            {
                Console.Write("     ");
            }
            Console.WriteLine(el.Node.ToString());
            
            if (el.LeftDesc != null)
            {
                directTraversal(el.LeftDesc, level + 1);
            }

            if (el.RightDesc != null)
            {
                directTraversal(el.RightDesc, level + 1);
            }
        }
    }
}