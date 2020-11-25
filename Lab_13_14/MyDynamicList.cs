using System;
using System.CodeDom;
using Lab_13_14;

namespace Lab1
{
    public class MyDynamicList<T>
    {
        private DataStructure<T> temp;
        private DataStructure<T> tail;
        private DataStructure<T> head;
        
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
                Console.WriteLine(e);
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
                        Console.WriteLine(temp.Node.ToString());
                        
                        temp = temp.Next;
                    } while (temp != null);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка выполнения запроса");
                }
            }
            else
            {
                Console.Write("Данные отсутствуют");
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

        private DataStructure<T> findItemByName(T itemToFind)
        {
            temp = head;
            do
            {
                var el = temp.Node;
                if (el.Equals(itemToFind))
                {
                    return temp;    
                }

                temp = temp.Next;
            } while (temp != null);
            
            return default;
        }
        
        private DataStructure<T> findPreviousItem(T itemToFind)
        {
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
                if (el.Equals(itemToFind))
                {
                    return temp;    
                }

                temp = temp.Next;
            } 
            
            return default;
        }

        public bool isEmpty()
        {
            return tail == null;
        }
        
    }
}