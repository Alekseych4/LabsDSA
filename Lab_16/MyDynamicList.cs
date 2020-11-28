using System;

namespace Lab_16
{
    public class MyDynamicList<T> : IDisposable
    {
        private DataStructure<T> temp;
        private DataStructure<T> tail;
        private DataStructure<T> head;

        private long equalityCounter;
        
        public MyDynamicList()
        {
            temp = default;
            tail = default;
            head = default;
        }

        public T remove(T itemToRemove)
        {
            if (isEmpty()) return default;
            if (itemToRemove == null) return default;

            try
            {
                var dataStructure = findItemByName(itemToRemove);
                var obj = dataStructure.Node;

                if (dataStructure.Next != null)
                {
                    dataStructure.Node = dataStructure.Next.Node;
                    dataStructure.Next = dataStructure.Next.Next;
                }
                else
                {
                    dataStructure.Node = default;
                    temp = head;
                    if (temp.Node == null)
                    {
                        tail = head;
                        return obj;
                    }
                    do
                    {
                        if (temp.Next.Node == null)
                        {
                            temp.Next = null;
                            tail = temp;
                            break;
                        }

                        temp = temp.Next;
                    } while (temp != null);
                }
                return obj;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                return default;
            }
        }

        public void showState()
        {
            if (!isEmpty())
            {
                try
                {
                    temp = head;
                    do
                    {
                        Console.Write(temp.Node.ToString() + " | ");
                        
                        temp = temp.Next;
                    } while (temp != null);
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    //Console.WriteLine("Данные отсутствуют");
                }
            }
            else
            {
                //Console.Write("Данные отсутствуют");
            }
        }

        public bool add(T item)
        {
            if (item == null) return false;
            
            try
            {
                if (tail == null)
                {
                    head = new DataStructure<T>()
                    {
                        Node = item,
                        Next = null
                    };
                    tail = head;
                    return true;
                }
                
                temp = new DataStructure<T>()
                {
                    Node = item,
                    Next = null
                };
                tail.Next = temp;
                tail = temp;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        
        public T getItem(T itemToFind)
        {
            if (!isEmpty() && itemToFind != null)
            {
                var result = findItemByName(itemToFind);
                if (result != null)
                {
                    return result.Node;
                }
            }

            return default;
        }

        public bool addAfter(T itemToAdd, T itemToFind)
        {
            if (itemToAdd == null || itemToFind == null) return false;
            if (isEmpty()) return add(itemToAdd);
        
            try
            {
                var itemFromList = findItemByName(itemToFind);
                if (itemFromList == null) return false;
                
                temp = new DataStructure<T>()
                {
                    Node = itemToAdd,
                    Next = itemFromList.Next
                };
                itemFromList.Next = temp;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool addBefore(T itemToAdd, T itemToFind)
        {
            if (itemToAdd == null || itemToFind == null) return false;
            if (isEmpty()) return add(itemToAdd);
            
            try
            {
                //itemFromList   (itemToAdd)   itemToFind
                var itemFromList = findPreviousItem(itemToFind);
                if (itemFromList == null) return false;
                
                if (itemFromList.Node == null && itemFromList.Next == null)
                {
                    temp = itemFromList;
                    temp.Node = itemToAdd;
                    temp.Next = head;

                    head = temp;
                }
                else
                {
                    temp = new DataStructure<T>()
                    {
                        Node = itemToAdd,
                        Next = itemFromList.Next
                    };
                    
                    itemFromList.Next = temp;
                }
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private DataStructure<T> findItemByName(T itemToFind)
        {
            equalityCounter = 0;
            
            temp = head;
            do
            {
                equalityCounter++;
                var el = temp.Node;
                if (el.Equals(itemToFind))
                {
                    return temp;    
                }

                temp = temp.Next;
            } while (temp != null);
            
            return null;
        }
        
        private DataStructure<T> findPreviousItem(T itemToFind)
        {
            equalityCounter = 0;

            equalityCounter++;
            if (head.Node.Equals(itemToFind))
            {
                return new DataStructure<T>()
                {
                    Node = default,
                    Next = null
                };
            }

            temp = head;
            while (temp.Next != null)
            {
                var el = temp.Next.Node;
                equalityCounter++;
                if (el.Equals(itemToFind))
                {
                    return temp;    
                }

                temp = temp.Next;
            } 
            
            return null;
        }

        public bool isEmpty()
        {
            return tail == null;
        }

        public long getEqualityCounter()
        {
            return equalityCounter;
        }

        ~MyDynamicList()
        {
            while (!isEmpty())
            {
                tail.Node = default;
                tail = tail.Next;
            }
        }
        
        public void Dispose()
        {
            while (!isEmpty())
            {
                tail.Node = default;
                tail = tail.Next;
            }
        }
    }
}