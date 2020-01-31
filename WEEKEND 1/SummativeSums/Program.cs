using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummativeSums
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array1 = { 1, 90, -33, -55, 67, -16, 28, -55, 15 };
            int[] array2 = { 999, -60, -77, 14, 160, 301 };
            int[] array3 = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200, -99 };

            int sum1 = sumMethod(array1);
            int sum2 = sumMethod(array2);
            int sum3 = sumMethod(array3);

            Console.WriteLine("Array Sum #1: " + sum1);
            Console.WriteLine("Array Sum #2: " + sum2);
            Console.WriteLine("Array Sum #3: " + sum3);
            Console.ReadLine();
        }
        static int sumMethod(int[] array)
        {
            int total = 0;
            for (int i = 0; i < array.Length; i++)
            {
                total = total + array[i];
            }
            return total;
        }
    }
}
