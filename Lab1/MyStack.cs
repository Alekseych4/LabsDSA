using System;

namespace Lab1
{
    public class MyStack<T> : IDisposable
    {
        private StackDataStructure<T> current = null;
        private StackDataStructure<T> head = null;
        
        public MyStack()
        {
            
        }

        public T pop()
        {
            if (head != null)
            {
                try
                {
                    current = head;
                    head = head.Previous;
                    return current.Node;
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
            if (head != null)
            {
                try
                {
                    current = head;
                    do
                    {
                        Console.Write(current.Node.ToString() + " ");
                        current = current.Previous;
                    } while (current != null);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка выполнения запроса");
                }
            }
            else
            {
                Console.WriteLine("Данные отсутствуют");
            }
        }

        public bool push(T item)
        {
            if (item == null) return false;
            
            try
            {
                if (head == null)
                {
                    head = new StackDataStructure<T>(){Node = item, Previous = null};
                }
                else
                {
                    current = new StackDataStructure<T>(){Node = item, Previous = head};
                    head = current;
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
            return head == null;
        }

        ~MyStack()
        {
            while (!isEmpty())
            {
                var element = pop();
                element = default(T);
            }
        }

        public void Dispose()
        {
            while (!isEmpty())
            {
                var element = pop();
                element = default(T);
            }
        }
    }
}