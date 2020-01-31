using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyHearts
{
    class Program
    {
        static void Main(string[] args)
        {
            int age;

            Console.WriteLine("What is your age?");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out age) && age > -1)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("A real number, please.");
                }
            }
            Console.WriteLine("Your maximum heart rate is " + (220 - age) + " Beats Per Minute.");
            Console.WriteLine("Your targer heart rate zone is between " + (age * 1.5) + " & " + (age * 1.85) + " Beats Per Minute.");
            Console.ReadLine();
        }
    }
}
