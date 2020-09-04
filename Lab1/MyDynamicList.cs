
using System;
using System.CodeDom;

namespace Lab1
{
    public class MyDynamicList<T> : IDisposable
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

        public T remove(string name)
        {
            if (isEmpty()) return default;
            if (string.IsNullOrEmpty(name)) return default;

            try
            {
                var dataStructure = findByNameFromHead(name);
                var obj = dataStructure.Node;

                if (dataStructure == head)
                {
                    head = dataStructure.Next;
                }

                if (dataStructure == tail)
                {
                    tail = dataStructure.Previous;
                }


                dataStructure.Previous.Next = dataStructure.Next;
                dataStructure.Next.Previous = dataStructure.Previous;

                return obj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }

        }

        public void showStateFromHead()
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
                    } while (temp != head);
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
        
        public void showStateFromTail()
        {
            if (!isEmpty())
            {
                try
                {
                    temp = tail;
                    do
                    {
                        Console.WriteLine(temp.Node.ToString());

                        temp = temp.Previous;
                    } while (temp != tail);
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
                if (head == null)
                {
                    head = new DataStructure<T>()
                    {
                        Node = item,
                        Next = head,
                        Previous = head
                    };
                    tail = head;
                    return true;
                }
                
                temp = new DataStructure<T>()
                {
                    Node = item,
                    Next = head,
                    Previous = tail
                };
                tail.Next = temp;
                tail = temp;
                head.Previous = tail;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool addAfter(T itemToAdd, string name)
        {
            if (itemToAdd == null || name == null) return false;
            if (isEmpty()) return add(itemToAdd);
        
            try
            {
                var itemFromList = findByNameFromTail(name);
                if (itemFromList == null) return false;
                
                temp = new DataStructure<T>()
                {
                    Node = itemToAdd,
                    Next = itemFromList.Next,
                    Previous = itemFromList
                };

                if (itemFromList == tail)
                {
                    tail = temp;
                }
                
                itemFromList.Next = temp;
                temp.Next.Previous = temp;
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        
        public bool addBefore(T itemToAdd, string name)
        {
            if (itemToAdd == null || name == null) return false;
            if (isEmpty()) return add(itemToAdd);
        
            try
            {
                var itemFromList = findByNameFromHead(name);
                if (itemFromList == null) return false;
                
                temp = new DataStructure<T>()
                {
                    Node = itemToAdd,
                    Next = itemFromList,
                    Previous = itemFromList.Previous
                };

                if (itemFromList == head)
                {
                    head = temp;
                }

                itemFromList.Previous = temp;
                temp.Previous.Next = temp;
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private DataStructure<T> findByNameFromHead(string name)
        {
            temp = head;
            do
            {
                var el = temp.Node as Student;
                if (el.Name.Equals(name))
                {
                    return temp;    
                }

                temp = temp.Next;
            } while (temp != head);
            
            return default;
        }
        
        private DataStructure<T> findByNameFromTail(string name)
        {
            temp = tail;
            do
            {
                var el = temp.Node as Student;
                if (el.Name.Equals(name))
                {
                    return temp;    
                }

                temp = temp.Previous;
            } while (temp != tail);
            
            return default;
        }

        public bool isEmpty()
        {
            return head == null;
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