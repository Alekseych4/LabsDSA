
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
            if (isFull()) return false;
            
            if (isEmpty())
            {
                return add(item);
            }
            else
            {
                if (item == null) return false;
                try
                {

                    if (item is string)
                    {
                        bool sorted = false;
                        for (int i = beforeElement; i < counter; i++)
                        {
                            sorted = compareStrings(item as string, array[i] as string);

                            if (sorted)
                            {
                                moveRight(i);
                                array[i] = item;
                                counter++;
                                break;
                            }

                        }
                        if (!sorted)
                        {
                            array[counter++] = item;
                        }
                    }
                    else if (item is int)
                    {
                        bool sorted = false;
                        int item1 = (int)(object) item;
                        for (int i = beforeElement; i < counter; i++)
                        {
                            sorted = compareInts(item1, (int)(object) array[i]);

                            if (sorted)
                            {
                                moveRight(i);
                                array[i] = item;
                                counter++;
                                break;
                            }

                        }
                        if (!sorted)
                        {
                            array[counter++] = item;
                        }
                    }

                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        //use only if array hasn't been filled 
        private void moveRight(int from)
        {
            var start = array[from];
            for (int i = from; i < array.Length - 1; i++)
            {
                temp = array[i + 1];
                array[i + 1] = start;
                start = temp;
            }
        }

        //Returns true if this is a place for the element in the sorted list
        private bool compareStrings(string elementToAdd, string existingElement)
        {
            if (string.IsNullOrEmpty(existingElement)) return true;
            if (existingElement.Length < elementToAdd.Length) return false;
            if (!existingElement.Equals(elementToAdd))
            {
                char[] lettersToAdd = elementToAdd.ToCharArray();
                char[] existingLetters = existingElement.ToCharArray();

                for (int i = 0; i < existingLetters.Length; i++)
                {
                    if (lettersToAdd[i] == existingLetters[i])
                    {
                        if (lettersToAdd.Length == i + 1)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                
            }

            return true;
        }

        //Returns true if this is a place for the element in the sorted list
        private bool compareInts(int elementToAdd, int existingElement)
        {
            return elementToAdd <= existingElement;
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