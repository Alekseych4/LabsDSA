﻿using System;

namespace LabSaod_9
{
    internal class Program
    {
        private static MyPBTree<int> _myPbTree;
        public static void Main(string[] args)
        {

            _myPbTree = new MyPBTree<int>();
            
            Console.WriteLine("Команды для использования программы:");
            Console.WriteLine("ADD  добавить элемент в дерево");
            Console.WriteLine("PPRINT   обход в прямом порядке");
            Console.WriteLine("IPRINT  обход в симметричном порядке");
            Console.WriteLine("RPRINT  обход в обратном порядке");
            Console.WriteLine("ILPRINT  нерекурсивный обход в симметричном порядке");
            Console.WriteLine("GET  найти элемент");
            Console.WriteLine("DEL   удалить дерево");
            Console.WriteLine("EXT  выход");

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
                    Console.WriteLine("Введите число - элемент для добавления:");
                    var itemToAdd = Console.ReadLine();
                    Console.WriteLine("Введите число для поиска");
                    var itemToFind = Console.ReadLine();
                    if (int.TryParse(itemToAdd, out int a) && int.TryParse(itemToFind, out int b))
                    {
                        if (_myPbTree.addItem(a, b))
                        {
                            Console.WriteLine("Элемент успешно добавлен");
                        }
                        else
                        {
                            Console.WriteLine("Произошла ошибка");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Введите ЧИСЛА!");
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
                case "ILPRINT":
                    _myPbTree.printInorderWithLoop();
                    break;
                case "GET":
                    Console.WriteLine("Введите число для поиска");
                    var itemToSearch = Console.ReadLine();
                    if (int.TryParse(itemToSearch, out int o))
                    {
                        _myPbTree.getItem(o);
                    }
                    else
                    {
                        Console.WriteLine("Введите ЧИСЛО!");
                    }
                    
                    break;
                case "DEL":
                    _myPbTree.Dispose();
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