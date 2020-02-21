using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGCollection.BLL;
using VGCollection.Models;

namespace VGCollection.UI
{
    class UserInterface
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("What would you like to do? (create, retrieve one, retrieve all, update, delete)");
                string initial = Console.ReadLine();
                initial = initial.ToLower();
                if (initial == "create")
                {
                    bool valid = false;
                    while (valid == false)
                    {
                        valid = Validation.ValidateCreate(PromptAllPieces());
                        if (valid == false)
                        {
                            Console.WriteLine("CRITICAL ERROR: Final validation failed.");
                        }
                    }
                    Console.WriteLine("Game created.");
                }
                else if (initial == "retrieve one")
                {
                    bool valid = false;
                    Videogame game = new Videogame();
                    while (valid == false)
                    {
                        game = Validation.ValidateRetrieveOne(NamePrompt());
                        valid = Validation.ValidateGame(game);
                        if (valid == false)
                        {
                            Console.WriteLine("CRITICAL ERROR: Final validation failed.");
                        }
                    }
                    WriteGameToConsole(game);
                }
                else if (initial == "retrieve all")
                {
                    foreach (Videogame game in Validation.ValidRetreiveAll())
                    {
                        WriteGameToConsole(game);
                    }
                }
                else if (initial == "update")
                {
                    bool valid = false;
                    while (valid == false)
                    {
                        Console.WriteLine("Which game do you want to update? (Enter exact name.)");
                        string name = NamePrompt();
                        Console.WriteLine("Enter the updated information.");
                        Videogame game = PromptAllPieces();
                        valid = Validation.ValidateUpdate(name, game);
                        if (valid == false)
                        {
                            Console.WriteLine("CRITICAL ERROR: Final validation failed.");
                        }
                    }
                    Console.WriteLine("Updated.");
                }
                else if (initial == "delete")
                {
                    bool valid = false;
                    while (valid == false)
                    {
                        valid = Validation.ValidateDelete(NamePrompt());
                        if (valid == false)
                        {
                            Console.WriteLine("CRITICAL ERROR: Final validation failed.");
                        }
                    }
                    Console.WriteLine("Deleted.");
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
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

            return new Videogame { Name = name, Genre = genre, System = system, Publisher = publisher, Year = year };
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
                valid = Validation.IntValidator(input);
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
