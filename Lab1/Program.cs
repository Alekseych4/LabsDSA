using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Lab1
{
    internal class Program
    {
        private static MyStaticQueue<char> myQueue;
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
                        myQueue = new MyStaticQueue<char>(length);
                        break;
                    }
                }
            } while (true);


            Console.WriteLine("Команды для использования программы:");
            Console.WriteLine("q  выход из программы");
            Console.WriteLine();
            random = new Random();
            Console.WriteLine("Инициирован датчик псевдослучайных чисел");
            
            while (true)
            {
                var randomNum = random.Next(1, 101);
                if (randomNum % 2 == 0)
                {
                    AddElements();
                }
                else
                {
                    DeleteElements();
                }
                
                Console.WriteLine();
                Console.WriteLine("Закончить выполнение? (для выхода введите q и нажмите Enter, для продолжения нажмите Enter)");
                var command =  Console.ReadLine();
                if (command == "q")
                {
                    myQueue.Dispose();
                    break;
                }
            }
        
        }

        private static void DeleteElements()
        {
            var delElements = random.Next(1, 4);
            Console.WriteLine("Операция: удаление");
            Console.WriteLine($"Количество удаляемых элементов: {delElements}");
            for (int i = 0; i < delElements; i++)
            {
                var el = myQueue.remove();
                if (el != '\0')
                {
                    Console.WriteLine("Удаленный элемент: " + el);
                    Console.Write("Состояние очереди после удаления: ");
                    myQueue.showState();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Очередь пуста!");
                    break;
                }
            }
        }

        private static void AddElements()
        {
            var addElements = random.Next(1, 4);
            Console.WriteLine("Операция: добавление");
            Console.WriteLine($"Количество добавляемых элементов: {addElements}");
            for (int i = 0; i < addElements; i++)
            {
                if (myQueue.isFull())
                {
                    Console.WriteLine("Очередь заполнена!");
                    myQueue.showState();
                    break;
                }

                var charCode = random.Next(65, 91);
                char sym = (char) charCode;
                myQueue.offer(sym);
                myQueue.showState();
                Console.WriteLine();
            }
        }

    }
}