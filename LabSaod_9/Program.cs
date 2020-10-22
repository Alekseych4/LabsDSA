using System;

namespace LabSaod_9
{
    internal class Program
    {
        private static MyPBTree<int> _myPbTree;
        public static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Задайте количество элементов ИСД:");
                Console.WriteLine();
                var lengthStr = Console.ReadLine();
                if (int.TryParse(lengthStr, out var length))
                {
                    if (length > 0)
                    {
                        _myPbTree = new MyPBTree<int>(length);
                        break;
                    }
                }
            } while (true);
            
            
            Console.WriteLine("Команды для использования программы:");
            Console.WriteLine("ADD  добавить элементы в ИСД");
            Console.WriteLine("DPRINT   обход в прямом порядке");

            while (true)
            {
                var command = Console.ReadLine();
                if (command.Equals("EXT"))
                {
                    break;
                }
                CommandRecognizer(command);
            }
        }

        private static void CommandRecognizer(string command)
        {
            switch (command)
            {
                case "ADD":
                    if (_myPbTree.add(new int []{1,2,3,4,5,6}))
                    {
                        Console.WriteLine("Дерево успешно построено");
                    }
                    else
                    {
                        Console.WriteLine("Дерево уже построено, либо произошла ошибка");
                    }
                    break;
                
                case "DPRINT":
                    _myPbTree.printDirectTraversal();
                    break;
            }
        }
    }
}