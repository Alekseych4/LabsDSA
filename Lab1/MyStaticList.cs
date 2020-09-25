
using System;
using System.Runtime.Remoting.Services;

namespace Lab1
{
    public class MyStaticList<T>
    {
        private int counter;
        private T temp;

        private T[] array;
        
        public MyStaticList(int length)
        {
            counter = 0;
            temp = default;
            array = new T[length];
        }

        public int remove(T item)
        {
            if (isEmpty()) return -1;
            
            if (item != null)
            {
                try
                {
                    int itemIndex = findItemIndex(item);
                    if (itemIndex != counter - 1)
                    {
                        array[itemIndex] = default;
                        moveLeft(itemIndex);
                        counter--;
                        return itemIndex;
                    }
                    else
                    {
                        counter--;
                        array[itemIndex] = default;
                        return itemIndex;
                    }
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);
                    return -1;
                }
            }
            return -1;
        }

        public void showState()
        {
            if (!isEmpty())
            {
                try
                {
                    for (int i = 0; i < counter; i++)
                    {
                        Console.Write($"{array[i]}  ");
                    }
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
            if (isFull()) return false;
            
            
            if (item == null) return false;
            try
            {
                if (isEmpty())
                {
                    array[counter] = item;
                    counter++;
                    return true;
                }

                if (item is string)
                {
                    bool sorted = false;
                    for (int i = 0; i < counter; i++)
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
                    for (int i = 0; i < counter; i++)
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
                //Console.WriteLine(e);
                return false;
            }
        }

        public int getItemIndex(T item)
        {
            if (item == null) return -1;

            try
            {
                int elementIndex = findItemIndex(item);
                return elementIndex;

            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                return -1;
            }
        }

        private int findItemIndex(T item)
        {
            if (item is string)
            {
                for (int i = 0; i < counter; i++)
                {
                    if ((array[i] as string).Equals(item as string))
                    {
                        return i;
                    }
                }
            }
            else if (item is int)
            {
                int itemInt = (int) (object) item;
                for (int i = 0; i < counter; i++)
                {
                    if (itemInt == (int)(object)array[i])
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        //use only if array hasn't been filled 
        private void moveRight(int from)
        {
            var start = array[from];
            var t = default(T);
            for (int i = from; i < counter; i++)
            {
                t = array[i + 1];
                array[i + 1] = start;
                start = t;
            }
        }

        private void moveLeft(int blankSpace)
        {
            var start = array[counter - 1];
            var t = default(T);
            for (int i = counter - 1; i > blankSpace; i--)
            {
                t = array[i - 1];
                array[i - 1] = start;
                start = t;
            }
        }

        //Returns true if this is a place for the element in the sorted list
        private bool compareStrings(string elementToAdd, string existingElement)
        {
            if (string.IsNullOrEmpty(existingElement)) return true;
            if (!existingElement.Equals(elementToAdd))
            {
                char[] lettersToAdd = elementToAdd.ToCharArray();
                char[] existingLetters = existingElement.ToCharArray();

                int length = 0;
                if (existingLetters.Length <= lettersToAdd.Length)
                {
                    length = existingLetters.Length;
                }
                else
                {
                    length = lettersToAdd.Length;
                }

                for (int i = 0; i < length; i++)
                {
                    if (lettersToAdd[i] == existingLetters[i])
                    {
                        if (i == length - 1)
                        {
                            if (existingLetters.Length >= lettersToAdd.Length)
                            {
                                return true;
                            }
                            else if (existingLetters.Length < lettersToAdd.Length)
                            {
                                return false;
                            }
                        }
                        else continue;
                        
                    }

                    if (lettersToAdd[i] < existingLetters[i] && i <= lettersToAdd.Length - 1)
                    {
                        return true;
                    }

                    if (lettersToAdd[i] > existingLetters[i])
                    {
                        return false;
                    }
                }

                return false;
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
            return counter == 0;
        }

        public bool isFull()
        {
            return counter == array.Length;
        }

        // ~MyStaticList()
        // {
        //     while (!isEmpty())
        //     {
        //         var element = remove();
        //         element = default(T);
        //     }
        // }
        //
        // public void Dispose()
        // {
        //     while (!isEmpty())
        //     {
        //         var element = remove();
        //         element = default(T);
        //     }
        // }
    }
}