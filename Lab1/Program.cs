using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Lab1
{
    internal class Program
    {
        private static MyDynamicList<Student> _myList;

        public static void Main(string[] args)
        {
            _myList = new MyDynamicList<Student>();

            Console.WriteLine("Команды для использования программы:");
            Console.WriteLine("ADD  добавление элемента");
            Console.WriteLine("ADDAFTER  добавление после элемента");
            Console.WriteLine("ADDBEFORE  добавление до элемента");
            Console.WriteLine("DEL  удаление элемента");
            Console.WriteLine("SHOWHEAD  состояние списка с головы");
            Console.WriteLine("SHOWTAIL  состояние списка с конца");
            Console.WriteLine("ISEMPTY  пустота");
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
                case "DEL":
                    Console.WriteLine("Введите имя для удаления:");
                    var st = _myList.remove(Console.ReadLine());
                    if (st == null)
                    {
                        Console.WriteLine("Данные отсутствуют");
                    }
                    else
                    {
                        Console.WriteLine("Данные удалены:");
                        Console.WriteLine(st.ToString());   
                    }
                    break;
                case "ADD":
                    Console.WriteLine("Введите имя студента:");
                    var name = Console.ReadLine();
                    Console.WriteLine("Введите фамилию студента:");
                    var surname = Console.ReadLine();
                    Console.WriteLine("Введите оценку студента:");
                    var mark = Console.ReadLine();
                    if (_myList.add(new Student(){Name = name, Mark = mark, Surname = surname}))
                    {
                        Console.WriteLine("Данные успешно записаны");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось записать данные");
                    }
                    
            
                    break;
                case "ADDAFTER":
                    Console.WriteLine("Введите элемент для добавления:");
                    Console.WriteLine("Введите имя студента:");
                    var name1 = Console.ReadLine();
                    Console.WriteLine("Введите фамилию студента:");
                    var surname1 = Console.ReadLine();
                    Console.WriteLine("Введите оценку студента:");
                    var mark1 = Console.ReadLine();
                    Console.WriteLine("Введите имя элемента, после которого добавить:");
                    var addAfter = Console.ReadLine();
                    if (_myList.addAfter(new Student(){Name = name1, Surname = surname1, Mark = mark1}, addAfter))
                    {
                        Console.WriteLine("Данные успешно записаны");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось записать данные");
                    }
                    break;
                
                case "ADDBEFORE":
                    Console.WriteLine("Введите элемент для добавления:");
                    Console.WriteLine("Введите имя студента:");
                    var name2 = Console.ReadLine();
                    Console.WriteLine("Введите фамилию студента:");
                    var surname2 = Console.ReadLine();
                    Console.WriteLine("Введите оценку студента:");
                    var mark2 = Console.ReadLine();
                    Console.WriteLine("Введите имя элемента, до которого добавить:");
                    var addAfter2 = Console.ReadLine();
                    if (_myList.addBefore(new Student(){Name = name2, Surname = surname2, Mark = mark2}, addAfter2))
                    {
                        Console.WriteLine("Данные успешно записаны");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось записать данные");
                    }
                    break;

                case "SHOWHEAD":
                    _myList.showStateFromHead();
                    Console.WriteLine();
                    break;
                
                case "SHOWTAIL":
                    _myList.showStateFromTail();
                    Console.WriteLine();
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
                
                default: 
                    Console.WriteLine("No such command");
                    break;
            }
            
        }

    }
}