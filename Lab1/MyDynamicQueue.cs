
using System;

namespace Lab1
{
    public class MyDynamicQueue<T> : IDisposable
    {

        private DataStructure<T> last;
        private DataStructure<T> first;
        private DataStructure<T> temp;

        public MyDynamicQueue()
        {
            first = default;
            last = default;
            temp = default;
        }

        public T remove()
        {
            if (first != null)
            {
                try
                {
                    if (first == last)
                    {
                        temp = first;
                        first = first.Next;
                        last = first;
                        return temp.Node;
                    }
                    else
                    {
                        temp = first;
                        first = first.Next;
                        return temp.Node;
                    }
                    
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return default(T);
                }
            }
            return default(T);
        }

        public void showState()
        {
            if (!isEmpty())
            {
                try
                {
                    temp = first;
                    do
                    {
                        Console.Write("'" + temp.Node.ToString() + "'");
                        temp = temp.Next;
                    }
                    while (temp != null);
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

        public bool offer(T item)
        {
            if (item == null) return false;
            
            try
            {
                if (first != null)
                {
                    temp = new DataStructure<T>()
                    {
                        Node = item,
                        Next = null
                    };

                    last.Next = temp;
                    last = temp;
                }
                else
                {
                    temp = new DataStructure<T>()
                    {
                        Node = item,
                        Next = null
                    };
                    first = temp;
                    last = temp;
                }
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool isEmpty()
        {
            return first == last && first == null;
        }

        ~MyDynamicQueue()
        {
            while (!isEmpty())
            {
                var element = remove();
                element = default(T);
            }
        }

        public void Dispose()
        {
            while (!isEmpty())
            {
                var element = remove();
                element = default(T);
            }
        }
    }
}