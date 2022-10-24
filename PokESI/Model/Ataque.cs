using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokESI
{
    public class Ataque
    {
        public string name { get; set; }
        public string type { get; set; }
        public double power { get; set; }

        public Ataque(string name, string type, double power)
        {
            this.name=name;
            this.type=type;
            this.power=power;
        }


    }
}