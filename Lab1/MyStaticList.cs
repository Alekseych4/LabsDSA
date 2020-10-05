
using System;

namespace Lab1
{
    public class MyStaticList<T>
    {
        private int counter;
        private int tail;
        private int head;
        private int temp;
        private MyStack<int> freeCells;

        private DataStructure<T>[] array;
        
        public MyStaticList(int length)
        {
            counter = 0;
            temp = -1;
            tail = -1;
            head = -1;
            
            freeCells = new MyStack<int>();
            
            array = new DataStructure<T>[length];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new DataStructure<T>();
                freeCells.push(i);
            }

            foreach (var item in array)
            {
                item.Next = -1;
            }
        }

        public T remove(T item)
        {
            if (isEmpty()) return default;
            
            if (item != null)
            {
                try
                {
                    int itemIndex = findItemIndex(item);
                    if (itemIndex == -1) return default;

                    var data = array[itemIndex].Node;
                    array[itemIndex].Node = default;

                    if (array[itemIndex].Previous != -1)
                    {
                        if (array[itemIndex].Next != -1)
                        {
                            array[array[itemIndex].Previous].Next = array[itemIndex].Next;
                            array[array[itemIndex].Next].Previous = array[itemIndex].Previous;
                        }
                        else
                        {
                            tail = array[itemIndex].Previous;
                            array[array[itemIndex].Previous].Next = -1;
                        }
                    }
                    else
                    {
                        if (array[itemIndex].Next != -1)
                        {
                            array[array[itemIndex].Next].Previous = -1;
                            head = array[itemIndex].Next;
                        }
                        else
                        {
                            tail = -1;
                            head = -1;
                        }
                    }

                    freeCells.push(itemIndex);
                    
                    counter--;

                    return data;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Элемент не найден");
                    return default;
                }
            }
            return default;
        }

        public void showState()
        {
            if (!isEmpty())
            {
                try
                {
                    temp = head;
                    
                    freeCells.showState();
                    
                    do
                    {
                        Console.WriteLine(array[temp].Node);
                        temp = array[temp].Next;
                        
                    } while (temp != -1);
                    
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
                var freeCellIndex = freeCells.pop();
                array[freeCellIndex] = new DataStructure<T>()
                {
                    Node = item,
                    Next = -1,
                    Previous = tail
                };

                if (head == -1)
                {
                    head = freeCellIndex;
                }

                if (tail != -1)
                {
                    array[tail].Next = freeCellIndex;
                }

                tail = freeCellIndex;

                counter++;

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
                    var freeCellIndex = freeCells.pop();
                    
                    array[freeCellIndex] = new DataStructure<T>()
                    {
                        Node = itemToAdd,
                        Next = beforeIndex,
                        Previous = array[beforeIndex].Previous
                    };

                    if (array[beforeIndex].Previous != -1)
                    {
                        array[array[beforeIndex].Previous].Next = freeCellIndex;
                    }

                    if (beforeIndex == head)
                    {
                        head = freeCellIndex;
                    }
                    
                    array[beforeIndex].Previous = freeCellIndex;

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
                    var freeCellIndex = freeCells.pop();
                    
                    array[freeCellIndex] = new DataStructure<T>()
                    {
                        Node = itemToAdd,
                        Next = array[afterIndex].Next,
                        Previous = afterIndex
                    };

                    if (array[afterIndex].Next != -1)
                    {
                        array[array[afterIndex].Next].Previous = freeCellIndex;
                    }

                    if (afterIndex == tail)
                    {
                        tail = freeCellIndex;
                    }

                    array[afterIndex].Next = freeCellIndex;

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
                return findItemIndex(item);
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось найти элемент");
                return -1;
            }
        }

        private int findItemIndex(T item)
        {
            if (item is string && !isEmpty())
            {
                temp = head;
                do
                {
                    if ((array[temp].Node as string).Equals(item as string))
                    {
                        return temp;
                    }

                    temp = array[temp].Next;
                    
                } while (temp != -1);
                
            }
            else if (item is int && !isEmpty())
            {
                int itemInt = (int) (object) item;
                temp = head;
                do
                {
                    if ((int)(object) array[temp].Node == itemInt)
                    {
                        return temp;
                    }

                    temp = array[temp].Next;
                    
                } while (temp != -1);
                
            }

            return -1;
        }

        public bool isEmpty()
        {
            return counter == 0;
        }

        public bool isFull()
        {
            return freeCells.isEmpty();
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