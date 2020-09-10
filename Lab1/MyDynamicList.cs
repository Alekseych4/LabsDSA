
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
                var dataStructure = findItemByName(name);
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

        public bool moveElementTo(MyDynamicList<T> to, string elementName)
        {
            if (isEmpty()) return false;
            if (to == null) return false;

            try
            {
                temp = head;
                DataStructure<T> searchResult = default;
                //находим элемент для перемещения
                if (elementName.Equals((temp.Node as Student).Name))
                {
                    searchResult = temp;
                    head = head.Next;
                    if (temp == tail)
                    {
                        tail = head;
                    }
                }
                else
                {
                    while (temp.Next != null)
                    {
                        var el = temp.Next.Node as Student;
                        if (el.Name.Equals(elementName))
                        {
                            searchResult = temp.Next;
                            if (temp.Next == tail)
                            {
                                tail = temp;
                            }
                            temp.Next = temp.Next.Next;
                            break;
                        }

                        temp = temp.Next;
                    }
                }
                //вставляем элемент в другой список
                if (searchResult == null) return false;
                
                to.temp = searchResult;
                to.temp.Next = default;
                
                if (to.isEmpty())
                {
                    to.head = to.temp;
                }
                else
                {
                    to.tail.Next = to.temp;
                }

                to.tail = to.temp;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
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

        public bool addAfter(T itemToAdd, string name)
        {
            if (itemToAdd == null || name == null) return false;
            if (isEmpty()) return add(itemToAdd);
        
            try
            {
                var itemFromList = findItemByName(name);
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

        private DataStructure<T> findItemByName(string name)
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
            } while (temp != null);
            
            return default;
        }
        
        private DataStructure<T> findPreviousItem(string name)
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
            } while (temp != null);
            
            return default;
        }

        public bool isEmpty()
        {
            return tail == null;
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