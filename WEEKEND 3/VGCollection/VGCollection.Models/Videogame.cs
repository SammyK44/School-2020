using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGCollection.Models
{
    public class Videogame
    {
        public string Name { get; set; }
        public string System { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
    }
}
