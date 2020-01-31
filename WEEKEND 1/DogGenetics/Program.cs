using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogGenetics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What is the dog's name?");
            string dogName = Console.ReadLine();

            Random rng = new Random();
            int gene1 = rng.Next(1, 96);
            int gene2 = rng.Next(1, 97 - gene1);
            int gene3 = rng.Next(1, 98 - (gene1 + gene2));
            int gene4 = rng.Next(1, 99 - (gene1 + gene2 + gene3));
            int gene5 = 100 - (gene1 + gene2 + gene3 + gene4);

            Console.WriteLine(dogName + "'s breeds are...");
            Console.WriteLine(gene1 + "% Burmese Mountain dog");
            Console.WriteLine(gene2 + "% Pug");
            Console.WriteLine(gene3 + "% Pitbull");
            Console.WriteLine(gene4 + "% Pomeranian");
            Console.WriteLine(gene5 + "% Husky");
            Console.ReadLine();
        }
    }
}
