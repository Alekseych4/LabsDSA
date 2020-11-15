using System;

namespace LabSaod_9
{
    internal class Program
    {
        private static MyPBTree<int> _myPbTree;
        public static void Main(string[] args)
        {

            _myPbTree = new MyPBTree<int>();
            
            Console.WriteLine("Команды для использования программы:");
            Console.WriteLine("ADDR  добавить элемент в дерево рекурсивно");
            Console.WriteLine("ADDL  добавить элемент в дерево в цикле");
            Console.WriteLine("RPRINT  обход в обратно-симметричном порядке");
            Console.WriteLine("STRP  вывод в строку в порядке возрастания ключей");
            Console.WriteLine("GET  найти элемент");
            Console.WriteLine("DEL   удалить элемент");
            Console.WriteLine("DIS   удалить дерево");
            Console.WriteLine("RAN  дерево с заданным числом вершин");
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
                case "ADDR":
                    Console.WriteLine("Введите ключ:");
                    var itemToAdd = Console.ReadLine();
                    
                    if (int.TryParse(itemToAdd, out int a))
                    {
                        if (_myPbTree.add(a, 999))
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
                
                case "ADDL":
                    Console.WriteLine("Введите ключ:");
                    var itemToAdd2 = Console.ReadLine();
                    
                    if (int.TryParse(itemToAdd2, out int b))
                    {
                        if (_myPbTree.addNodeInLoop(b, 999))
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
                    Console.WriteLine("Введите ключ для поиска");
                    
                    if (int.TryParse(Console.ReadLine(), out int o))
                    {
                        var ch = _myPbTree.getItemRepeats(o);
                        if (ch > 0)
                        {
                            Console.WriteLine("Число повторений элемента: " + ch);
                        }
                        else
                        {
                            Console.WriteLine("Элемент отсутствует");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Введите ЧИСЛО!");
                    }
                    
                    break;
                case "RAN":
                    Random random = new Random();
                    Console.WriteLine("Введите число элементов для дерева:");
                    
                    if (int.TryParse(Console.ReadLine(), out int els))
                    {
                        for (int i = 0; i < els; i++)
                        {
                            if (!_myPbTree.add(random.Next(1, 99), 666))
                            {
                                Console.WriteLine("Произошла ошибка");
                            }
                        }
                        Console.WriteLine("Элементы добавлены");
                    }
                    else
                    {
                        Console.WriteLine("Введите ЧИСЛО!");
                    }
                    break;
                case "STRP":
                    _myPbTree.printTreeInString();
                    break;
                
                case "DEL":
                    Console.WriteLine("Введите ключ:");
                    if (int.TryParse(Console.ReadLine(), out int d))
                    {
                        if (_myPbTree.delete(d))
                        {
                            Console.WriteLine("Элемент успешно удален");
                        }
                        else
                        {
                            Console.WriteLine("Произошла ошибка");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Введите ЧИСЛО!");
                    }
                    break;
                case "DIS":
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