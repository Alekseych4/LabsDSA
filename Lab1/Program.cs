using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Lab1
{
    internal class Program
    {
        private static MyStaticQueue<Student> myQueue;

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
                        myQueue = new MyStaticQueue<Student>(length);
                        break;
                    }
                }
            } while (true);


            Console.WriteLine("Команды для использования программы:");
            Console.WriteLine("ADD  добавить объект в очередь");
            Console.WriteLine("ISFULL  проверка очереди на заполненность");
            Console.WriteLine("SHOWALL  вывести на экран все объекты");
            Console.WriteLine("REMOVE  вывести на экран и удалить объект с головы очереди");
            Console.WriteLine("ISEMPTY  проверка на пустоту очереди");
            Console.WriteLine("EXT  выход из программы");
            Console.WriteLine();

            while (true)
            {
                var command = Console.ReadLine();
                if (command.Equals("EXT"))
                {
                    myQueue.Dispose();
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
                    if (myQueue.isFull()) Console.WriteLine("Очередь заполнена");
                    else
                    {
                        Console.WriteLine("Введите имя студента:");
                        var name = Console.ReadLine();
                        Console.WriteLine("Введите фамилию студента:");
                        var surname = Console.ReadLine();
                        Console.WriteLine("Введите оценку студента:");
                        var mark = Console.ReadLine();
                        if (myQueue.offer(new Student(name, surname, mark)))
                        {
                            Console.WriteLine("Данные успешно записаны");
                        }
                        else
                        {
                            Console.WriteLine("Не удалось записать данные");
                        }
                    }

                    break;
                
                case "SHOWALL":
                    myQueue.showState();
                    break;
                
                case "REMOVE":
                    var student = myQueue.remove();
                    if (student != null)
                    {
                        Console.WriteLine(student.Name + " " + student.Surname + " " + student.Mark);
                    }
                    else
                    {
                        Console.WriteLine("Данные отсутствуют");
                    }
                    break;
                case "ISEMPTY":
                    if (myQueue.isEmpty())
                    {
                        Console.WriteLine("Очередь пуста");
                    }
                    else
                    {
                        Console.WriteLine("Очередь не пуста");
                    }
                    break;
                case "ISFULL":
                    if (myQueue.isFull())
                    {
                        Console.WriteLine("Очередь заполнена");
                    }
                    else
                    {
                        Console.WriteLine("Очередь не заполнена");
                    }
                    break;
                default: 
                    Console.WriteLine("No such command");
                    break;
            }
            
        }
        
    }
}