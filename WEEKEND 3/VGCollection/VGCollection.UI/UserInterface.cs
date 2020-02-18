using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGCollection.DAL;
using VGCollection.BLL;

namespace VGCollection.UI
{
    class UserInterface
    {
        static void Main(string[] args)
        {
            Handler.ReadGames();

            while (true)
            {
                Console.WriteLine("What would you like to do? (create, retrieve one, retrieve all, update, delete)");
                string initial = Console.ReadLine();

                if (initial == "create")
                {
                    Handler.Create(PromptAllPieces());
                    Console.WriteLine("Game created.");
                }
                else if (initial == "retrieve one")
                {
                    WriteGameToConsole(Handler.RetrieveOne(NamePrompt()));
                }
                else if (initial == "retrieve all")
                {
                    foreach (Videogame game in Handler.RetrieveAll())
                    {
                        WriteGameToConsole(game);
                    }
                }
                else if (initial == "update")
                {
                    Console.WriteLine("Which game do you want to update? (Enter exact name.)");
                    Handler.Update(NamePrompt(), PromptAllPieces());
                    Console.WriteLine("Updated.");
                    break;
                }
                else if (initial == "delete")
                {
                    Handler.Delete(NamePrompt());
                    Console.WriteLine("Deleted.");
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }

                Handler.WriteGames();
            }
        }
        public static Videogame PromptAllPieces()
        {
            Console.WriteLine("Enter the game's name.");
            string name = StringPrompt();

            Console.WriteLine("Enter the game's system/console.");
            string system = StringPrompt();

            Console.WriteLine("Enter the game's genre.");
            string genre = StringPrompt();

            Console.WriteLine("Enter the game's publisher.");
            string publisher = StringPrompt();

            Console.WriteLine("Enter the game's year of release.");
            int year = NumberPrompt();

            return new Videogame { Name = name, Genre = genre, System = system, Publisher = publisher, Year = year};
        }
        public static string StringPrompt()
        {
            bool valid = false;
            string input = "";

            while (valid == false)
            {
                input = Console.ReadLine();
                valid = Validation.StringValidator(input);
                if (valid == false)
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            return input;
        }
        public static int NumberPrompt()
        {
            bool valid = false;
            string input;
            int number = 0;

            while (valid == false)
            {
                input = Console.ReadLine();
                valid = Validation.StringValidator(input);
                if (valid == false)
                {
                    Console.WriteLine("Invalid input.");
                }
                else
                {
                    number = int.Parse(input);
                }
            }
            return number;
        }
        public static string NamePrompt()
        {
            string input = "";
            bool done = false;

            Console.WriteLine("What is the game's name?");
            while (done == false)
            {
                input = Console.ReadLine();
                done = Validation.NameValidator(input);
                if (done == false)
                {
                    Console.WriteLine("Invalid name.");
                }
            }
            return input;
        }
        public static void WriteGameToConsole(Videogame game)
        {
            Console.WriteLine(game.Name);
            Console.WriteLine(game.System);
            Console.WriteLine(game.Genre);
            Console.WriteLine(game.Publisher);
            Console.WriteLine(game.Year);
            Console.WriteLine();
        }
    }
}
