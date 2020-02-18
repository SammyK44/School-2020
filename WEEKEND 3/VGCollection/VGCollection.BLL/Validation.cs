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
            foreach (Videogame game in Handler.RetrieveAll())
            {
                if (game.Name == input)
                {
                    valid = true;
                }
            }
            return valid;
        }
        public static bool StringValidator(string input)
        {
            bool valid = true;
            if (string.IsNullOrEmpty(input) || input.Contains('~'))
            {
                valid = false;
            }
            return valid;
        }
        public static bool IntValidator(string input)
        {
            bool valid = true;
            if (string.IsNullOrEmpty(input) || !int.TryParse(input, out _))
            {
                valid = false;
            }
            return valid;
        }
        public static bool ValidateCreate(Videogame game)
        {
            bool valid = ValidateGame(game);

            if (valid == true)
            {
                Handler.Create(game);
            }
            return valid;
        }
        public static Videogame ValidateRetrieveOne(string input)
        {
            Videogame game = new Videogame();
            bool valid = NameValidator(input);
            if (valid == true)
            {
                game = Handler.RetrieveOne(input);
            }
            //Else indicate that it's invalid.
            return game;
        }
        public static List<Videogame> ValidRetreiveAll()
        {
            return Handler.RetrieveAll();
        }
        public static bool ValidateUpdate(string oldName, Videogame game)
        {
            bool validName = NameValidator(oldName);
            bool validGame = ValidateGame(game);
            bool overallValidity = false;
            if (validName == true && validGame == true)
            {
                overallValidity = true;
                Handler.Update(oldName, game);
            }
            return overallValidity;
        }
        public static bool ValidateDelete(string input)
        {
            bool valid = NameValidator(input);
            if (valid == true)
            {
                Handler.Delete(input);
            }
            return valid;
        }
        public static bool ValidateGame(Videogame game)
        {
            bool valid = true;
            if (game.Name == "" || game.Name.Contains('~') ||
                game.Genre == "" || game.Genre.Contains('~') ||
                game.Publisher == "" || game.Publisher.Contains('~') ||
                game.System == "" || game.System.Contains('~'))
            {
                valid = false;
            }
            return valid;
        }
    }
}
