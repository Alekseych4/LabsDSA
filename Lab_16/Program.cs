using System;

namespace Lab_16
{
    internal class Program
    {
        private static string[] keys = new[] {"begin;", "!end", "do", "start", "for", "else", "as", "var", "IF", "integer"};
        private const int Length = 10;
        private static string[] hashTable = new string [10];
        public static void Main(string[] args)
        {
            Console.WriteLine("H  создать хэш-таблицу и вывести её на экран");
            Console.WriteLine("F  поиск элемента в хэш-таблице");
            Console.WriteLine("C  хэш-код слова");
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

            return sum % Length;
        }

        private static void putInTable(int hashCode, string el)
        {
            hashTable[hashCode] = el;
        }

        private static void printTable()
        {
            for (int i = 0; i < hashTable.Length; i++)
            {
                Console.Write($"[{i}]: {hashTable[i]}  ");
            }
            Console.WriteLine();
        }

        private static bool searchKey(string key)
        {
            var hashSearch = hash(key);
            if (hashTable[hashSearch].Equals(key))
            {
                Console.WriteLine($"Элемент находится в {hashSearch} ячейке");
                return true;
            }

            return false;
        }

        private static void commandRecognizer(string command)
        {
            switch (command)
            {
                case "H":
                    foreach (var el in keys)
                    {
                        var hashCode = hash(el);
                        putInTable(hashCode, el);
                    }
                    
                    printTable();
                    break;
                case "F":
                    Console.WriteLine("Введите ключ для поиска");
                    var keyForSearch = Console.ReadLine();
                    if (!searchKey(keyForSearch))
                    {
                        Console.WriteLine("Данный элемент отсутствует в хэш-таблице");
                    }
                    
                    break;
                case "C":
                    var input = Console.ReadLine();
                    var h = hash(input);
                    Console.WriteLine($"Хэш-код значения {input}: {h}");
                    break;
            }
        }
    }
}