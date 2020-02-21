using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGCollection.Models;
using System.IO;

namespace VGCollection.DAL
{
    public static class Handler
    {
        public static List<Videogame> videogames = new List<Videogame>();
        public static void Create(Videogame game)
        {
            videogames.Add(game);
        }
        public static Videogame RetrieveOne(string name)
        {
            Videogame videogame = new Videogame();
            foreach (Videogame game in videogames)
            {
                if (game.Name == name)
                {
                    videogame = game;
                    break;
                }
            }
            return videogame;
        }
        public static List<Videogame> RetrieveAll()
        {
            return videogames;
        }
        public static void Update(string oldName, Videogame newGame)
        {
            foreach (Videogame game in videogames)
            {
                if (game.Name == oldName)
                {
                    Delete(game.Name);
                }
                break;
            }
            Create(newGame);
        }
        public static void Delete(string name)
        {
            videogames.Remove(RetrieveOne(name));
        }

        //Read & write
        public static string path = @"List.txt";

        public static string GameToString(Videogame game)
        {
            return game.Name + '~' + game.Genre + '~' + game.Publisher + '~' + game.System + '~' + game.Year;
        }
        public static void WriteGames()
        {
            StreamWriter writer = new StreamWriter(path);
            foreach (Videogame game in videogames)
            {
                writer.WriteLine(GameToString(game));
            }
            writer.Close();
        }
        public static void ReadGames()
        {
            videogames.Clear();
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] temp = line.Split('~');
                Create(new Videogame { Name = temp[0], System = temp[3], Genre = temp[1], Publisher = temp[2], Year = int.Parse(temp[4]) });
            }
        }
    }
}
