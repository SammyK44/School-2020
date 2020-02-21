using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGCollection.Models;
using VGCollection.DAL;

namespace VGCollection.BLL
{
    public static class Validation
    {
        public static bool NameValidator(string input)
        {
            bool valid = false;
            Handler.ReadGames();
            foreach (Videogame game in Handler.RetrieveAll())
            {
                if (game.Name == input)
                {
                    valid = true;
                }
            }
            Handler.WriteGames();
            return valid;
        }
        public static bool StringValidator(string input)
        {
            bool valid = true;
            Handler.ReadGames();
            if (string.IsNullOrEmpty(input) || input.Contains('~'))
            {
                valid = false;
            }
            Handler.WriteGames();
            return valid;
        }
        public static bool IntValidator(string input)
        {
            bool valid = true;
            Handler.ReadGames();
            if (string.IsNullOrEmpty(input) || !int.TryParse(input, out _))
            {
                valid = false;
            }
            Handler.WriteGames();
            return valid;
        }
        public static bool ValidateCreate(Videogame game)
        {
            Handler.ReadGames();
            bool valid = ValidateGame(game);

            if (valid == true)
            {
                Handler.Create(game);
            }
            Handler.WriteGames();
            return valid;
        }
        public static Videogame ValidateRetrieveOne(string input)
        {
            Handler.ReadGames();
            Videogame game = new Videogame();
            bool valid = NameValidator(input);
            if (valid == true)
            {
                game = Handler.RetrieveOne(input);
            }
            Handler.WriteGames();
            //Will be coupled with ValidateGame() to complete validation.
            ///ValidateGame(ValidateRetrieveOne(input))
            return game;
        }
        public static List<Videogame> ValidRetreiveAll()
        {
            Handler.ReadGames();
            Handler.WriteGames();
            return Handler.RetrieveAll();
        }
        public static bool ValidateUpdate(string oldName, Videogame game)
        {
            Handler.ReadGames();
            bool validName = NameValidator(oldName);
            bool validGame = ValidateGame(game);
            bool overallValidity = false;
            if (validName == true && validGame == true)
            {
                overallValidity = true;
                Handler.Update(oldName, game);
            }
            Handler.WriteGames();
            return overallValidity;
        }
        public static bool ValidateDelete(string input)
        {
            Handler.ReadGames();
            bool valid = NameValidator(input);
            if (valid == true)
            {
                Handler.Delete(input);
            }
            Handler.WriteGames();
            return valid;
        }
        public static bool ValidateGame(Videogame game)
        {
            bool valid = true;
            if (string.IsNullOrEmpty(game.Name) || game.Name.Contains('~') ||
                string.IsNullOrEmpty(game.Genre) || game.Genre.Contains('~') ||
                string.IsNullOrEmpty(game.Publisher) || game.Publisher.Contains('~') ||
                string.IsNullOrEmpty(game.System) || game.System.Contains('~'))
            {
                valid = false;
            }
            return valid;
        }
    }
}
