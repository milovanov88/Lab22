using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab22_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Задача №22");
            Console.WriteLine("Задайте размерность массива");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> funk1 = new Func<object, int[]>(GetArrey);
            Task<int[]> task1 = new Task<int[]>(funk1, n);

            Func<Task<int[]>, int> funk2 = new Func<Task<int[]>, int>(SumArrey);
            Task<int> task2 = task1.ContinueWith<int>(funk2);

            Func<Task<int[]>, int> funk3 = new Func<Task<int[]>, int>(MaxArrey);
            Task<int> task3 = task1.ContinueWith<int>(funk3);

            task1.Start();

            Thread.Sleep(100);
            Console.WriteLine( $"сумма значений массива: {task2.Result}");
            Console.WriteLine($"максимальное значение элемента массива: {task3.Result}");

            Console.ReadKey();
        }
        static int[] GetArrey(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
            return array;
        }
        static int SumArrey(Task<int[]> task)
        {
            int[] array = task.Result;
            Console.WriteLine("Метод Sum начал работу");
            int sum = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                sum += array[i];
            }
            Console.WriteLine("Метод Sum завершил работу");
            return sum;
        }
        static int MaxArrey(Task<int[]> task)
        {
            int[] array = task.Result;
            Console.WriteLine("Метод Max начал работу");
            int max = array[0];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i] > max) max = array[i];
            }
            Console.WriteLine("Метод Max завершил работу");
            return max;
        }
    }
}
