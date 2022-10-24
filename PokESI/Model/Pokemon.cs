using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokESI
{
    public class Pokemon
    {
        public string name { get; set; }
        public int id { get; set; }
        public string specie { get; set; }
        public List<string> types { get; set; }
        public List<string> abilities { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public List<string> evolutionLine { get; set; }
        public bool starter { get; set; }
        public bool legendary { get; set; }
        public bool mega { get; set; }
        public bool mythical { get; set; }
        public bool ultraBeast { get; set; }
        public int generation { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public bool combat { get; set; }
        public bool captured { get; set; }
        public int level { get; set; }
        public int experience { get; set; }
        public double health { get; set; }



    }
}
