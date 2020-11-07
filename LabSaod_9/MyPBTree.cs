using System;

namespace LabSaod_9
{
    public class MyPBTree<T> : IDisposable
    {
        private DataStructure<T> root;

        public MyPBTree()
        {
            root = default;
        }

        public bool addItem(T itemToAdd, T itemToSearch)
        {
            if (root == null)
            {
                root = new DataStructure<T>()
                {
                    Node = itemToAdd,
                    LeftDesc = null,
                    RightDesc = null
                };
                
                return true;
            }
            
            var structureFound = getItem_preorder(root, itemToSearch);

            if (structureFound == null) return false;

            if (structureFound.LeftDesc != null && structureFound.RightDesc != null)
            {
                Console.WriteLine("Добавление невозможно - существуют оба потомка");
                return false;
            }

            if (structureFound.LeftDesc == null && structureFound.RightDesc == null)
            {
                Console.WriteLine("Создать вершину как левый или правый потомок? L/R (По умолчанию создастся как левый потомок)");
                var answer = Console.ReadLine();
                if (answer == "L")
                {
                    var newItem = new DataStructure<T>()
                    {
                        Node = itemToAdd,
                        LeftDesc = null,
                        RightDesc = null
                    };

                    structureFound.LeftDesc = newItem;
                    return true;
                }

                if (answer == "R")
                {
                    var newItem = new DataStructure<T>()
                    {
                        Node = itemToAdd,
                        LeftDesc = null,
                        RightDesc = null
                    };

                    structureFound.RightDesc = newItem;
                    return true;
                }
                
                Console.WriteLine("Входные данные неверны - сохранение по умолчанию");
            }

            if (structureFound.LeftDesc == null)
            {
                var newItem = new DataStructure<T>()
                {
                    Node = itemToAdd,
                    LeftDesc = null,
                    RightDesc = null
                };

                structureFound.LeftDesc = newItem;
                return true;
            }

            if (structureFound.RightDesc == null)
            {
                var newItem = new DataStructure<T>()
                {
                    Node = itemToAdd,
                    LeftDesc = null,
                    RightDesc = null
                };

                structureFound.RightDesc = newItem;
                return true;
            }

            return false;
        }

        public void getItem(T itemToSearch)
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }

            var searchResult = getItem_preorder(root, itemToSearch);
            if (searchResult != null)
            {
                Console.WriteLine(searchResult.Node);
                return;
            }
            
            Console.WriteLine("Элемент не найден");
        }

        private DataStructure<T> getItem_preorder(DataStructure<T> el, T itemToSearch)
        {
            if (itemToSearch.Equals(el.Node))
            {
                return el;
            }
            
            if (el.LeftDesc != null)
            {
                var res = getItem_preorder(el.LeftDesc, itemToSearch);
                if (res != null) return res;
            }

            if (el.RightDesc != null)
            {
                var res = getItem_preorder(el.RightDesc, itemToSearch);
                if (res != null) return res;
            }

            return null;
        }

        public void printInorderWithLoop()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }
            
            var level = 0;
            var stack = new MyStack<TreeNodeForPrint<DataStructure<T>>>();
            var currentEl = root;
            stack.push(new TreeNodeForPrint<DataStructure<T>>() {Node = currentEl, Level = level});

            do
            {
                if (currentEl.LeftDesc != null)
                {
                    stack.push(new TreeNodeForPrint<DataStructure<T>>() {Node = currentEl.LeftDesc, Level = ++level});
                    currentEl = currentEl.LeftDesc;
                }
                else
                {
                    if (stack.isEmpty())
                    {
                        break;
                    }
                    
                    var elOut = stack.pop();
                    if (elOut != null)
                    {
                        var el = elOut.Node;
                        currentEl = el;
                        
                        printIndent(elOut.Level);
                        Console.WriteLine(el.Node.ToString());
                        level--;

                        if (currentEl.RightDesc != null)
                        {
                            stack.push(new TreeNodeForPrint<DataStructure<T>>()
                                {Node = currentEl.RightDesc, Level = elOut.Level + 1});
                            
                            currentEl = el.RightDesc;
                            level = elOut.Level + 1;
                        }
                    }
                    
                }
            } while (true);
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

        private void disposeTraversal(DataStructure<T> el)
        {
            if (el.LeftDesc != null)
            {
                disposeTraversal(el.LeftDesc);
            }

            if (el.RightDesc != null)
            {
                disposeTraversal(el.RightDesc);
            }

            el.Node = default;
            el.LeftDesc = null;
            el.RightDesc = null;
            el = null;
        }

        public void Dispose()
        {
            if (root != null)
            {
                disposeTraversal(root);
            }

            root = null;
        }
    }
}