
using System;
using System.CodeDom;
using System.Text;

namespace Lab1
{
    public class MyDynamicList<T> : IDisposable where T : IDisposable
    {
        private DataStructure<T> temp;
        private DataStructure<T> tail;
        private DataStructure<T> head;
        private string _tag;

        public string Tag => _tag;

        public MyDynamicList()
        {
            temp = null;
            tail = null;
            head = null;
        }
        
        public MyDynamicList(string tag)
        {
            temp = null;
            tail = null;
            head = null;
            this._tag = tag;
        }

        public bool remove(T itemToRemove)
        {
            if (isEmpty()) return false;
            if (itemToRemove == null) return false;

            try
            {
                var dataStructure = findItemByName(itemToRemove);
                if (dataStructure == null) return false;
                
                itemToRemove.Dispose();
                itemToRemove = default;

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
                        head = null;
                        temp = head;
                        tail = head;
                    }
                    else
                    {
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
                }
                
                Console.WriteLine("Удаленный элемент:");
                Console.WriteLine(obj.ToString());
                
                obj.Dispose();
                obj = default;
                
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
                    //Console.WriteLine(e);
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

        public bool addAfter(T itemToAdd, T itemToFind)
        {
            if (itemToAdd == null || itemToFind == null) return false;
            if (isEmpty())
            {
                itemToFind.Dispose();
                itemToFind = default;
                return add(itemToAdd);
            }
        
            try
            {
                var itemFromList = findItemByName(itemToFind);
                
                itemToFind.Dispose();
                itemToFind = default;
                
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

        public bool addBefore(T itemToAdd, T itemToFind)
        {
            if (itemToAdd == null || itemToFind == null) return false;
            if (isEmpty())
            {
                itemToFind.Dispose();
                itemToFind = default;
                return add(itemToAdd);
            }
            
            try
            {
                //itemFromList   (itemToAdd)   itemToFind
                var itemFromList = findPreviousItem(itemToFind);
                
                itemToFind.Dispose();
                itemToFind = default;
                
                if (itemFromList == null) return false;
                
                if (itemFromList.Node == null && itemFromList.Next == null)
                {
                    temp = itemFromList;
                    temp.Node = itemToAdd;
                    temp.Next = head;

                    head = temp;
                }
                else
                {
                    temp = new DataStructure<T>()
                    {
                        Node = itemToAdd,
                        Next = itemFromList.Next
                    };
                    
                    itemFromList.Next = temp;
                }
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public T getItem(T itemToFind)
        {
            if (!isEmpty() && itemToFind != null)
            {
                var result = findItemByName(itemToFind);
                
                if (result != null)
                {
                    itemToFind.Dispose();
                    itemToFind = default;
                    return result.Node;
                }
                itemToFind.Dispose();
                itemToFind = default;
            }

            return default;
        }

        private DataStructure<T> findItemByName(T itemToFind)
        {
            temp = head;
            do
            {
                var el = temp.Node;
                if (el.Equals(itemToFind))
                {
                    itemToFind = default;
                    return temp;    
                }

                temp = temp.Next;
            } while (temp != null);
            
            itemToFind = default;
            return default;
        }
        
        private DataStructure<T> findPreviousItem(T itemToFind)
        {
            if (head.Node.Equals(itemToFind))
            {
                itemToFind = default;
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
                    itemToFind = default;
                    return temp;    
                }

                temp = temp.Next;
            } 
            
            itemToFind = default;
            return default;
        }

        public bool isEmpty()
        {
            return tail == null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType().Equals(this.GetType()))
            {
                if (this.Tag != null && ((MyDynamicList<T>) obj).Tag != null)
                {
                    return this.Tag.Equals(((MyDynamicList<T>) obj).Tag);
                }
            }

            return false;
        }

        public override string ToString()
        {
            if (!isEmpty())
            {
                try
                {
                    StringBuilder output = new StringBuilder();
                    temp = head;
                    do
                    {
                        if (temp == head)
                        {
                            output.Append($"| Tag: {Tag} |  Items:  ");
                        }
                        output.Append(temp.Node);
                        output.Append("  |  ");
                        
                        temp = temp.Next;
                    } while (temp != null);

                    return output.ToString();
                }
                catch (Exception e)
                {
                    return "Ошибка перевода в строку";
                }
            }

            if (Tag != null)
            {
                return $"| Tag: {Tag} |  Items:  ";
            }

            return "Данные отсутствуют";
        }

        public void Dispose()
        {
            while (!isEmpty())
            {
                head.Node = default;
                if (head.Next != null)
                {
                    temp = head.Next;
                    head = null;
                    head = temp;
                }
                else
                {
                    temp = null;
                    head = null;
                    tail = null;
                }
            }
        }
    }
}