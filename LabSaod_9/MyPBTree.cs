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
                    Value = itemToAdd,
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
                        Value = itemToAdd,
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
                        Value = itemToAdd,
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
                    Value = itemToAdd,
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
                    Value = itemToAdd,
                    LeftDesc = null,
                    RightDesc = null
                };

                structureFound.RightDesc = newItem;
                return true;
            }

            return false;
        }

        public bool add(int key, T value)
        {
            
            var addResult = addNodeRecursively(root, key);

            if (addResult == null)
            {
                Console.WriteLine("Не удалось добавить объект, либо объект с таким ключом уже существует");
                return false;
            }

            if (root == null)
            {
                root = addResult;
            }

            return true;
        }
        
        

        private DataStructure<T> addNodeRecursively(DataStructure<T> item, int key)
        {
            if (item == null)
            {
                return new DataStructure<T>()
                {
                    Key = key,
                    KeyEqualityCounter = 1,
                    LeftDesc = null,
                    RightDesc = null
                };
            }
            else
            {
                if (key < item.Key)
                {
                    var added =  addNodeRecursively(item.LeftDesc, key);
                    item.LeftDesc = added;
                    return item;
                }
                else if (key > item.Key)
                {
                    var added =  addNodeRecursively(item.RightDesc, key);
                    item.RightDesc = added;
                    return item;
                }
                else 
                {
                    item.KeyEqualityCounter++;
                }
            }

            return item;
        }

        public bool addNodeInLoop(int key, T value)
        {
            if (root == null)
            {
                root = new DataStructure<T>()
                {
                    Value = value,
                    Key = key,
                    KeyEqualityCounter = 1,
                    LeftDesc = null,
                    RightDesc = null
                };

                return true;
            }

            var current = root;
            var parent = root;

            while (current != null)
            {
                parent = current;
                if (key < current.Key)
                {
                    current = current.LeftDesc;
                }
                else if(key > current.Key)
                {
                    current = current.RightDesc;
                }
                else
                {
                    current.KeyEqualityCounter++;
                    Console.WriteLine("Увеличен счетчик");
                    return true;
                }
            }
            
            var newItem = new DataStructure<T>()
            {
                Key = key,
                Value = value,
                KeyEqualityCounter = 1,
                LeftDesc = null,
                RightDesc = null
            }; 

            if (key < parent.Key)
            {
                parent.LeftDesc = newItem;
            }
            else if(key > parent.Key)
            {
                parent.RightDesc = newItem;
            }

            return true;
        }

        //возвращает количество повторений узла
        public int getItemRepeats(int key)
        {
            var item = findItem(key);
            if (item == null)
            {
                return -1;
            }

            return item.KeyEqualityCounter;
        }

        private DataStructure<T> findItem(int key)
        {
            if (root == null)    
            {
                Console.WriteLine("Дерево пустое");
                return null;
            }

            var current = root;

            while (current != null)
            {
                if (key < current.Key)
                {
                    current = current.LeftDesc;
                }
                else if(key > current.Key)
                {
                    current = current.RightDesc;
                }
                else
                {
                    return current;
                }
            }
            
            Console.WriteLine("Элемент не найден");
            return null;
        }
        
        public void printTreeInString()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }
            
            printAscending(root);
        }

        private void printAscending(DataStructure<T> el)
        {
            if (el.LeftDesc != null)
            {
                printAscending(el.LeftDesc);
            }
            
            Console.Write($"{el.Key}({el.KeyEqualityCounter})  ");
            
            if (el.RightDesc != null)
            {
                printAscending(el.RightDesc);
            }

        }

        public bool delete(int key)
        {
            if (root == null) return false;
            
            var current = root;
            var temp = root;

            while (current != null)
            {

                if (key < current.Key)
                {
                    temp = current;
                    current = current.LeftDesc;
                }
                else if (key > current.Key)
                {
                    temp = current;
                    current = current.RightDesc;
                }
                else
                {
                    break;
                }
            }

            if (current == null)
            {
                Console.WriteLine("Элемент не найден");
                return false;
            }
            
            if (current.LeftDesc == null && current.RightDesc == null)
            {
                if (temp.LeftDesc == current)
                {
                    temp.LeftDesc = null;
                }

                else if (temp.RightDesc == current)
                {
                    temp.RightDesc = null;
                }
                else
                {
                    root = null;
                }
                
                current.Dispose();
                current = null;
                return true;
            }

            if (current.LeftDesc == null)
            {
                current.Value = current.RightDesc.Value;
                current.Key = current.RightDesc.Key;
                current.KeyEqualityCounter = current.RightDesc.KeyEqualityCounter;
                current.LeftDesc = current.RightDesc.LeftDesc;
                current.RightDesc = current.RightDesc.RightDesc;
                return true;
            }

            if (current.RightDesc == null)
            {
                current.Value = current.LeftDesc.Value;
                current.Key = current.LeftDesc.Key;
                current.KeyEqualityCounter = current.LeftDesc.KeyEqualityCounter;
                current.RightDesc = current.LeftDesc.RightDesc;
                current.LeftDesc = current.LeftDesc.LeftDesc;
                return true;
            }

            var itemToSwap = swapOnLastRightOnLeftSubtree(current.LeftDesc);

            current.Value = itemToSwap.Value;
            current.KeyEqualityCounter = itemToSwap.KeyEqualityCounter;
            var tempKey = itemToSwap.Key;
            delete(itemToSwap.Key);
            
            current.Key = tempKey;

            return true;
            
        }

        private DataStructure<T> swapOnLastRightOnLeftSubtree(DataStructure<T> item)
        {
            if (item.RightDesc != null)
            {
                return swapOnLastRightOnLeftSubtree(item.RightDesc);
            }

            return item;
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
                Console.WriteLine(searchResult.Value);
                return;
            }
            
            Console.WriteLine("Элемент не найден");
        }

        private DataStructure<T> getItem_preorder(DataStructure<T> el, T itemToSearch)
        {
            if (itemToSearch.Equals(el.Value))
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
                        Console.WriteLine(el.Value.ToString());
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
            Console.WriteLine(el.Value.ToString());
            
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
            Console.WriteLine(el.Value);
            
            
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
            Console.WriteLine($"{el.Key}");
            
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

            el.Value = default;
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