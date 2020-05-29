using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab1
{
    internal class Program
    {
        private static MyStack<Student> myStack;
        private static MyStack<Student> deletedStudents;
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Команды для использования программы:");
            Console.WriteLine("PUSH  добавить объект в стек");
            Console.WriteLine("PUSHRANDOM  добавить определенное число случайных объектов в стек");
            Console.WriteLine("SHOWALL  вывести на экран все объекты");
            Console.WriteLine("SHOWDELETED  вывести на экран удаленные во вспомогательный стек объекты");
            Console.WriteLine("POP  вывести на экран верхний объект стека");
            Console.WriteLine("ISEMPTY  проверка на пустоту стека");
            Console.WriteLine("EXT  выход из программы");
            Console.WriteLine();
            
            myStack = new MyStack<Student>();
            deletedStudents = new MyStack<Student>();

            while (true)
            {
                var command = Console.ReadLine();
                if (command.Equals("EXT"))
                {
                    myStack.Dispose();
                    deletedStudents.Dispose();
                    break;
                }
                CommandRecognizer(command);
            }
        }

        private static void CommandRecognizer(string command)
        {
            switch (command)
            {
                case "PUSH":
                    Console.WriteLine("Добавить элемент из вспомогательного стека? Y/N");
                    switch (Console.ReadLine())
                    {
                        case "Y":
                            if (myStack.push(deletedStudents.pop()))
                            {
                                Console.WriteLine("Данные успешно записаны");
                            }
                            else
                            {
                                Console.WriteLine("Данные отсутствуют");
                            }
                            break;
                        case "N":
                            Console.WriteLine("Введите имя студента:");
                            var name = Console.ReadLine();
                            Console.WriteLine("Введите фамилию студента:");
                            var surname = Console.ReadLine();
                            Console.WriteLine("Введите оценку студента:");
                            var mark = Console.ReadLine();
                            if (myStack.push(new Student(name, surname, mark)))
                            {
                                Console.WriteLine("Данные успешно записаны");
                            }
                            else
                            {
                                Console.WriteLine("Не удалось записать данные");
                            }
                            break;
                        default:
                            Console.WriteLine("Необходимо выбрать способ добавления элемента");
                            break;
                    }
                    break;
                case "PUSHRANDOM":
                    Console.WriteLine("Введите число элементов для добавления:");
                    var randomElementsQuantity = Console.ReadLine();
                    try
                    {
                        int elementsQuantity = Int32.Parse(randomElementsQuantity);
                        for (int i = 0; i < elementsQuantity; i++)
                        {
                            Random r = new Random();

                            myStack.push(new Student("Дефолт", "Дефолтович", r.Next(1,6).ToString()));
                            Thread.Sleep(10);
                        }
                        Console.WriteLine("Элементы добавлены");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Число введено неверно");
                    }
                    break;
                case "SHOWDELETED":
                    deletedStudents.showState();
                    break;
                case "SHOWALL":
                    myStack.showState();
                    break;
                case "POP":
                    
                    var student = myStack.pop();
                    if (student != null)
                    {
                        Console.WriteLine("Удалить элемент во вспомогательный стек?Y/N");
                        switch (Console.ReadLine())
                        {
                            case "Y":
                                if (deletedStudents.push(student))
                                {
                                    Console.WriteLine("Данные успешно записаны");
                                    Console.WriteLine(student.Name + " " + student.Surname + " " + student.Mark);
                                }
                                else
                                {
                                    Console.WriteLine("Не удалось записать данные");
                                }
                                break;
                            case "N":
                                Console.WriteLine(student.Name + " " + student.Surname + " " + student.Mark);
                                break;
                            default:
                                Console.WriteLine("Чтобы удалить элемент выберите способ удаления");
                                myStack.push(student);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Данные отсутствуют");
                    }
                    break;
                case "ISEMPTY":
                    if (myStack.isEmpty())
                    {
                        Console.WriteLine("Стек пуст");
                    }
                    else
                    {
                        Console.WriteLine("Стек не пуст");
                    }

                    break;
                default: 
                    Console.WriteLine("No such command");
                    break;
            }
            
        }
        
    }
}