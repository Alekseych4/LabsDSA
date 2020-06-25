using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Lab1
{
    internal class Program
    {
        private static MyStaticList<string> _myList;
        private static Random random;

        public static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Задайте длину массива:");
                Console.WriteLine();
                var lengthStr = Console.ReadLine();
                if (int.TryParse(lengthStr, out var length))
                {
                    if (length > 0)
                    {
                        _myList = new MyStaticList<string>(length);
                        break;
                    }
                }
            } while (true);

            Console.WriteLine("Команды для использования программы:");
            Console.WriteLine("ADD  добавление элемента");
            Console.WriteLine("DEL  удаление элемента");
            Console.WriteLine("SHOW  состояние списка");
            Console.WriteLine("GET поиск элемента");
            Console.WriteLine("ISEMPTY  пустота");
            Console.WriteLine("ISFULL  полнота");
            Console.WriteLine("q  выход из программы");
            Console.WriteLine();

            var command = Console.ReadLine();
            do
            {
                if (command.Equals("q"))
                {
                    break;
                }
                CommandRecognizer(command);
                command = Console.ReadLine();
            } while (true);
        }
        
         private static void CommandRecognizer(string command)
        {
            switch (command)
            {
                case "ADD":
                    if (_myList.isFull()) Console.WriteLine("Список заполнен");
                    else
                    {
                        var data = Console.ReadLine();
                        if (_myList.add(data))
                        {
                            Console.WriteLine("Данные успешно записаны");
                        }
                        else
                        {
                            Console.WriteLine("Не удалось записать данные");
                        }
                    }

                    break;
                
                case "SHOW":
                    _myList.showState();
                    Console.WriteLine();
                    break;
                
                case "DEL":
                    var i = _myList.remove(Console.ReadLine());
                    if (i != -1)
                    {
                        Console.WriteLine($"Удаленный элемент имел индекс {i}");
                    }
                    else
                    {
                        Console.WriteLine("Нет такого элемента");
                    }
                    break;
                case "GET":
                    var index = _myList.getItemIndex(Console.ReadLine());
                    if (index != -1)
                    {
                        Console.WriteLine($"Данный элемент имеет индекс {index}");
                    }
                    else
                    {
                        Console.WriteLine("Нет такого элемента");
                    }
                    
                    break;
                case "ISEMPTY":
                    if (_myList.isEmpty())
                    {
                        Console.WriteLine("Список пуст");
                    }
                    else
                    {
                        Console.WriteLine("Список не пуст");
                    }
                    break;
                case "ISFULL":
                    if (_myList.isFull())
                    {
                        Console.WriteLine("Список заполнен");
                    }
                    else
                    {
                        Console.WriteLine("Список не заполнен");
                    }
                    break;
                default: 
                    Console.WriteLine("No such command");
                    break;
            }
            
        }

    }
}