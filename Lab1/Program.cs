using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Lab1
{
    internal class Program
    {
        private static MyDynamicList<MyDynamicList<Student>> _listOfLists;
        

        public static void Main(string[] args)
        {
            _listOfLists = new MyDynamicList<MyDynamicList<Student>>("base_list");

            Console.WriteLine("Команды для использования программы:");
            Console.WriteLine("ADD  добавление элемента");
            Console.WriteLine("ADDAFTER  добавление после элемента");
            Console.WriteLine("ADDBEFORE  добавление до элемента");
            Console.WriteLine("DEL  удаление элемента");
            Console.WriteLine("SHOW  состояние списка");
            Console.WriteLine("ISEMPTY  пустота отдельного списка");
            Console.WriteLine("ISBASEEMPTY  пустота списка списков");
            Console.WriteLine("ADDLIST  добавление элемента в список списков");
            Console.WriteLine("DELLIST  удаление элемента списка списков");
            Console.WriteLine("SHOWLIST  состояние списка списков");
            Console.WriteLine("ADDLAFTER  добавление списка после списка");
            Console.WriteLine("ADDLBEFORE  добавление списка перед списком");
            Console.WriteLine("q  выход из программы");
            Console.WriteLine();

            var command = Console.ReadLine();
            do
            {
                if (command.Equals("q"))
                {
                    _listOfLists.Dispose();
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
                    Console.WriteLine("Введите тег списка, в котором искать: ");
                    var listToSearchIn = _listOfLists.getItem(new MyDynamicList<Student>(Console.ReadLine()));
                    if (listToSearchIn != null)
                    {
                        Console.WriteLine("Введите имя для удаления:");
                        var stToDelete = listToSearchIn.remove(new Student(Console.ReadLine(), null, null));
                        if (stToDelete)
                        {
                            Console.WriteLine("Удаление прошло успешно");
                        }
                        else
                        {
                            Console.WriteLine("Не удалось удалить элемент");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не удалось найти список");
                    }
                    
                    break;
                case "DELLIST":
                    Console.WriteLine("Введите тег списка для удаления:");
                    var listToDel = Console.ReadLine();
                    var deletedList = _listOfLists.remove(new MyDynamicList<Student>(listToDel));
                    if (deletedList)
                    {
                        Console.WriteLine("Удаление прошло успешно");
                        
                    }
                    else
                    {
                        Console.WriteLine("Не удалось удалить список");
                    }
                    break;
                
                case "ADD":
                    Console.WriteLine("Введите тег списка, в который добавить: ");
                    var listToAddIn = _listOfLists.getItem(new MyDynamicList<Student>(Console.ReadLine()));
                    if (listToAddIn != null)
                    {
                        Console.WriteLine("Введите имя студента:");
                        var name = Console.ReadLine();
                        Console.WriteLine("Введите фамилию студента:");
                        var surname = Console.ReadLine();
                        Console.WriteLine("Введите оценку студента:");
                        var mark = Console.ReadLine();
                        if (listToAddIn.add(new Student(){Name = name, Mark = mark, Surname = surname}))
                        {
                            Console.WriteLine("Данные успешно записаны");
                        }
                        else
                        {
                            Console.WriteLine("Не удалось записать данные");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не удалось найти список");
                    }
                    break;
                
                case "ADDAFTER":
                    Console.WriteLine("Введите тег списка, в который добавить: ");
                    var listToAddAfter = _listOfLists.getItem(new MyDynamicList<Student>(Console.ReadLine()));
                    if (listToAddAfter != null)
                    {
                        Console.WriteLine("Введите имя студента:");
                        var name1 = Console.ReadLine();
                        Console.WriteLine("Введите фамилию студента:");
                        var surname1 = Console.ReadLine();
                        Console.WriteLine("Введите оценку студента:");
                        var mark1 = Console.ReadLine();
                        Console.WriteLine("Введите имя элемента, после которого добавить:");
                        var addAfter = Console.ReadLine();
                        if (listToAddAfter.addAfter(new Student(){Name = name1, Surname = surname1, Mark = mark1}, 
                            new Student(addAfter, null, null)))
                        {
                            Console.WriteLine("Данные успешно записаны");
                        }
                        else
                        {
                            Console.WriteLine("Не удалось записать данные");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не удалось найти список");
                    }
                    break;
                
                case "ADDBEFORE":
                    Console.WriteLine("Введите тег списка, в который добавить: ");
                    var listToAddBefore = _listOfLists.getItem(new MyDynamicList<Student>(Console.ReadLine()));
                    if (listToAddBefore != null)
                    {
                        Console.WriteLine("Введите имя студента:");
                        var name2 = Console.ReadLine();
                        Console.WriteLine("Введите фамилию студента:");
                        var surname2 = Console.ReadLine();
                        Console.WriteLine("Введите оценку студента:");
                        var mark2 = Console.ReadLine();
                        Console.WriteLine("Введите имя элемента, до которого добавить:");
                        var addBefore = Console.ReadLine();
                        if (listToAddBefore.addBefore(new Student(){Name = name2, Surname = surname2, Mark = mark2}, 
                            new Student(addBefore, null, null)))
                        {
                            Console.WriteLine("Данные успешно записаны");
                        }
                        else
                        {
                            Console.WriteLine("Не удалось записать данные");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не удалось найти список");
                    }
                    break;
                
                case "ADDLIST":
                    Console.WriteLine("Введите тег списка:");
                    var newTag = Console.ReadLine();
                    if (_listOfLists.add(new MyDynamicList<Student>(newTag)))
                    {
                        Console.WriteLine($"Добавлен новый список с тегом: {newTag}");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось добавить список");
                    }
                    break;
                
                case "ADDLAFTER":
                    Console.WriteLine("Введите тег для нового списка:");
                    var newTagAfter = Console.ReadLine();
                    Console.WriteLine("Введите тег списка, после которого добавить:");
                    if (_listOfLists.addAfter(new MyDynamicList<Student>(newTagAfter), 
                        new MyDynamicList<Student>(Console.ReadLine())))
                    {
                        Console.WriteLine($"Добавлен новый список с тегом: {newTagAfter}");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось добавить список");
                    }
                    break;
                
                case "ADDLBEFORE":
                    Console.WriteLine("Введите тег для нового списка:");
                    var newTagBefore = Console.ReadLine();
                    Console.WriteLine("Введите тег списка, до которого добавить:");
                    if (_listOfLists.addBefore(new MyDynamicList<Student>(newTagBefore), 
                        new MyDynamicList<Student>(Console.ReadLine())))
                    {
                        Console.WriteLine($"Добавлен новый список с тегом: {newTagBefore}");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось добавить список");
                    }
                    break;

                case "SHOW":
                    Console.WriteLine("Введите тег списка для просмотра: ");
                    var showList = _listOfLists.getItem(new MyDynamicList<Student>(Console.ReadLine()));
                    if (showList != null)
                    {
                        showList.showState();
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Список не найден");
                    }
                    break;

                case "SHOWLIST":
                    _listOfLists.showState();
                    Console.WriteLine();
                    break;

                    case "ISBASEEMPTY":
                    if (_listOfLists.isEmpty())
                    {
                        Console.WriteLine("Список пуст");
                    }
                    else
                    {
                        Console.WriteLine("Список не пуст");
                    }
                    break;
                    
                    case "ISEMPTY":
                        Console.WriteLine("Введите тег списка для проверки на пустоту");
                        var isListEmpty = _listOfLists.getItem(new MyDynamicList<Student>(Console.ReadLine()));
                        if (isListEmpty != null)
                        {
                            if (isListEmpty.isEmpty())
                            {
                                Console.WriteLine("Список пуст");
                            }
                            else
                            {
                                Console.WriteLine("Список не пуст");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Список не найден");
                        }
                        break;
                
                default: 
                    Console.WriteLine("No such command");
                    break;
            }
            
        }

    }
}