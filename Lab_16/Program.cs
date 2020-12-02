using System;

namespace Lab_16
{
    internal class Program
    {
        private static int tableLength;
        private static MyDynamicList<string>[] hashTable;
        private static long equalityCounter;
        private static long totalOperations;
        public static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Введите длину хэш-таблицы");
                Console.WriteLine();
                var lengthStr = Console.ReadLine();
                if (int.TryParse(lengthStr, out var length))
                {
                    if (length > 0)
                    {
                        tableLength = length;
                        hashTable = new MyDynamicList<string>[length];
                        break;
                    }
                }
            } while (true);
            
            Console.WriteLine("L  ввести длину хэш-таблицы");
            Console.WriteLine("A  добавить элемент");
            Console.WriteLine("F  поиск элемента в хэш-таблице");
            Console.WriteLine("C  хэш-код слова");
            Console.WriteLine("P  вывести на экран");
            Console.WriteLine("TO  количество всех сравнений за время использования");
            Console.WriteLine("R  удалить элемент хэш-таблицы");
            Console.WriteLine("EXT  выход");

            while (true)
            {
                var command = Console.ReadLine();
                if (command.Equals("EXT"))
                {
                    break;
                }

                commandRecognizer(command);
            }
        }

        private static int hash(string key)
        {
            var charArray = key.ToCharArray();
            int sum = 0;
            
            for (int i = 0; i < charArray.Length; i++)
            {
                sum += charArray[i];
            }
            
            //Console.WriteLine($"{key} = {sum}");

            return sum % tableLength;
        }

        private static bool putInTable(int hashCode, string el)
        {
            equalityCounter = 3;

            if (hashTable[hashCode] == null)
            {
                hashTable[hashCode] = new MyDynamicList<string>();
            }
            else
            {
                if (!hashTable[hashCode].isEmpty())
                {
                    if (hashTable[hashCode].getItem(el) != null)
                    {
                        return false;
                    }
                }
            }
            
            return hashTable[hashCode].add(el);
        }

        private static void printTable()
        {
            for (int i = 0; i < hashTable.Length; i++)
            {
                Console.Write($"[{i}]:  ");
                if (hashTable[i] != null)
                {
                    hashTable[i].showState();
                }
            }
            Console.WriteLine();
        }

        private static bool searchKey(string key)
        {
            equalityCounter = 0;
            
            var hashSearch = hash(key);
            string item = null;
            equalityCounter++;
            if (hashTable[hashSearch] != null && !hashTable[hashSearch].isEmpty())
            {
                item = hashTable[hashSearch].getItem(key);
                equalityCounter = hashTable[hashSearch].getEqualityCounter();
            }

            if (item != null)
            {
                Console.WriteLine($"Элемент {item} находится в {hashSearch} ячейке");
                return true;
            }

            return false;
        }

        private static bool remove(string key)
        {
            equalityCounter = 0;
            var hashSearch = hash(key);
            string item = null;
            equalityCounter++;
            if (hashTable[hashSearch] != null && !hashTable[hashSearch].isEmpty())
            {
                item = hashTable[hashSearch].remove(key);
                equalityCounter = hashTable[hashSearch].getEqualityCounter();
            }

            if (item != null)
            {
                Console.WriteLine($"Удален элемент {item}, он находился в {hashSearch} ячейке");
                return true;
            }

            return false;
        }

        private static void commandRecognizer(string command)
        {
            switch (command)
            {
                case "L":
                    do
                    {
                        Console.WriteLine("Введите длину хэш-таблицы");
                        var lengthStr = Console.ReadLine();
                        if (int.TryParse(lengthStr, out var length))
                        {
                            if (length > 0)
                            {
                                totalOperations = 0;
                                equalityCounter = 0;
                                tableLength = length;
                                hashTable = new MyDynamicList<string>[length];
                                break;
                            }
                        }
                    } while (true);
                    break;
                case "A":
                    Console.WriteLine("Введите элемент:");
                    var e = Console.ReadLine();

                    var code = hash(e);
                    if (putInTable(code, e))
                    {
                        Console.WriteLine("Элемент успешно добавлен");
                        totalOperations += equalityCounter;
                    }
                    else
                    {
                        Console.WriteLine("Элемент не добавлен, таблица заполнена или ключ уже существует");
                    }
                    
                    Console.WriteLine($"Кол-во сравнений: {equalityCounter}");
                    
                    break;
                case "F":
                    Console.WriteLine("Введите ключ для поиска");
                    var keyForSearch = Console.ReadLine();
                    if (!searchKey(keyForSearch))
                    {
                        Console.WriteLine("Данный элемент отсутствует в хэш-таблице");
                    }
                    
                    Console.WriteLine($"Кол-во сравнений: {equalityCounter}");
                    totalOperations += equalityCounter;
                    break;
                case "C":
                    var input = Console.ReadLine();
                    var h = hash(input);
                    Console.WriteLine($"Хэш-код значения {input}: {h}");
                    break;
                case "P":
                    printTable();
                    break;
                case "TO":
                    Console.WriteLine("Все сравнения: " + totalOperations);
                    break;
                case "R":
                    Console.WriteLine("Введите ключ:");
                    if (!remove(Console.ReadLine()))
                    {
                        Console.WriteLine("Данный элемент отсутствует в хэш-таблице");
                    }
                    Console.WriteLine($"Кол-во сравнений: {equalityCounter}");
                    totalOperations += equalityCounter;
                    break;
            }
        }
    }
}