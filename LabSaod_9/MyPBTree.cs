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
            int leftHalf = nodesAmount / 2;
            int rightHalf = nodesAmount - leftHalf;
            elementsToAdd = elements;
            
            int count = nodesAmount;
            nodesAmount--;

            root = addNodes(leftHalf, rightHalf);
            
            nodesAmount = count;

            if (root != default)
            {
                return true;
            }

            return false;
        }

        private DataStructure<T> addNodes(int leftHalf, int rightHalf)
        {
            
            if (leftHalf == 0 && rightHalf == 0)
            {
                return default;
            }

            DataStructure<T> current = default;
            if (leftHalf > 0)
            {
                current = new DataStructure<T>(){Node = elementsToAdd[nodesAmount--]};
                
                int subLeftHalf = leftHalf / 2;
                int subRightHalf = leftHalf - 1 - subLeftHalf;
                
                var nextLeft = addNodes(subLeftHalf, subRightHalf);
                current.LeftDesc = nextLeft;
            }
            if(rightHalf > 0)
            {
                current = new DataStructure<T>(){Node = elementsToAdd[nodesAmount--]};
                
                int subLeftHalf = rightHalf / 2;
                int subRightHalf = rightHalf - 1 - subLeftHalf;
                
                var nextRight = addNodes(subLeftHalf, subRightHalf);
                current.RightDesc = nextRight;
            }

            return current;
        }
    }
}