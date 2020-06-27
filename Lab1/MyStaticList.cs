
using System;

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
                    if (itemIndex == -1) return -1;
                    
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
                    Console.WriteLine(e);
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
                array[counter++] = item;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool addBefore(T itemToAdd, T beforeItem)
        {
            if (isFull()) return false;
            if (itemToAdd == null || beforeItem == null) return false;
            if (isEmpty()) return add(itemToAdd);

            try
            {
                int beforeIndex = findItemIndex(beforeItem);
                if (beforeIndex != -1)
                {
                    moveRight(beforeIndex);
                    array[beforeIndex] = itemToAdd;
                    counter++;
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

        public bool addAfter(T itemToAdd, T afterItem)
        {
            if (isFull()) return false;
            if (itemToAdd == null || afterItem == null) return false;
            if (isEmpty()) return add(itemToAdd);

            try
            {
                int afterIndex = findItemIndex(afterItem);
                if (afterIndex != -1)
                {
                    moveRight(afterIndex + 1);
                    array[afterIndex + 1] = itemToAdd;
                    counter++;
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
                Console.WriteLine(e);
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