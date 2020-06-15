
using System;

namespace Lab1
{
    public class MyStaticList<T> : IDisposable
    {

        private int last;
        private int first;
        private int current;
        private int counter;
        private T temp;

        private T[] array;
        
        public MyStaticList(int length)
        {
            first = 0;
            last = 0;
            counter = 0;
            temp = default;
            array = new T[length];
        }

        public T remove()
        {
            if (first != last)
            {
                try
                {
                    current = first;
                    temp = array[current];
                    array[current] = default(T);
                    
                    if (first == array.Length - 1)
                    {
                        first = 0;
                    }
                    else
                    {
                        first++;
                    }

                    if (last == -1)
                    {
                        last = current;
                    }

                    counter--;
                    return temp;
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
                    current = first;
                    do
                    {
                        Console.Write("'" + array[current].ToString() + "'");
                        if (current == array.Length - 1)
                        {
                            current = 0;
                        }
                        else
                        {
                            current++;
                        }
                    }
                    while (current != last && current != first);
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
                if (last >= 0)
                {
                    array[last] = item;
                    counter++;
                    
                    if (last == array.Length - 1)
                    {
                        if (counter != array.Length)
                        {
                            last = 0;
                        }
                        else
                        {
                            last = -1;
                        }
                    }
                    else
                    {
                        if (counter != array.Length)
                        {
                            last++;
                        }
                        else
                        {
                            last = -1;
                        }
                    }

                    return true;
                }
                
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool addBefore(T item, int beforeElement)
        {
            if (isEmpty())
            {
                add(item);
            }
            else
            {
                if (item == null) return false;
                try
                {
                    temp = array[beforeElement];
                    bool sorted = false;
                    do
                    {
                        //sorted = compareStrings();
                    } while (!sorted);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }

            return true;
        }
        
        //Returns true if this is a place for the element in the sorted list
        private bool compareStrings(string elementToAdd, string existingElement)
        {
            if (existingElement.Length < elementToAdd.Length) return false;
            if (!existingElement.Equals(elementToAdd))
            {
                char[] lettersToAdd = elementToAdd.ToCharArray();
                char[] existingLetters = existingElement.ToCharArray();

                //int length = lettersToAdd.Length >= existingLetters.Length ? lettersToAdd.Length :  
            }

            return true;
        }

        public bool isEmpty()
        {
            return first == last;
        }

        public bool isFull()
        {
            return last == -1;
        }

        ~MyStaticList()
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