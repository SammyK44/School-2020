using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
