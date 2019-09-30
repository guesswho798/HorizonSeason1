using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    public class PlanetBuildings
    {
        private string name;
        private string description;
        private int number;
        private int[] prices = new int[5];
        private int[] generate = new int[5];

        public PlanetBuildings(string name, string description, int[] prices, int hp, int shield, int number)
        {
            if (prices[0] * number <= Program.Metals || prices[1] * number <= Program.Energy || prices[2] * number <= Program.Pops) //¯\_(ツ)_/¯ need to differentiate popswithing planets and overall
            {
                Program.Metals -= prices[0] * number;
                Program.Energy -= prices[1] * number;
                Program.Pops -= prices[2] * number;
                this.name = name;
                this.description = description;
                this.number += number;
            }

        }
        public void upgrade()
        {

        }
    }
}
