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
            Console.WriteLine("PPRINT   обход в прямом порядке");
            Console.WriteLine("IPRINT  обход в симметричном порядке");
            Console.WriteLine("RPRINT  обход в обратном порядке");

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
                    if (_myPbTree.add(randomArray(_myPbTree.getSize())))
                    {
                        Console.WriteLine("Дерево успешно построено");
                    }
                    else
                    {
                        Console.WriteLine("Дерево уже построено, либо произошла ошибка");
                    }
                    break;
                
                case "PPRINT":
                    _myPbTree.printPreorderTraversal();
                    break;
                
                case "IPRINT":
                    _myPbTree.printInorderTraversal();
                    break;
                case "RPRINT": 
                    _myPbTree.printReversedInorderTraversal();
                    break;
            }
        }

        private static int[] randomArray(int length)
        {
            var random = new Random();
            var a = new int[length];
            for (int i = 0; i < length; i++)
            {
                a[i] = random.Next(0, 99);
            }

            return a;
        }
    }
}