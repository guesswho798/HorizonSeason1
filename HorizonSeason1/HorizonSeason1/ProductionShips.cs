using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    public class ProductionShips
    {
        private string name;
        private string description;
        private int[] prices = new int[5];
        private int hp;
        private int shield;
        private int number;
        private int[] generate = new int[5];

        public ProductionShips(string name, string description, int[] prices, int hp, int shield, int number)
        {
            if (prices[0] * number <= Program.manager.Metals || prices[1] * number <= Program.manager.Energy || prices[2] * number <= Program.manager.Pops) //¯\_(ツ)_/¯ need to differentiate popswithing planets and overall
            {
                Program.manager.Metals -= prices[0] * number;
                Program.manager.Energy -= prices[1] * number;
                Program.manager.Pops -= prices[2] * number;
                this.name = name;
                this.description = description;
                this.hp = hp;
                this.shield = shield;
                this.number += number;
            }

        }

        public void upgrade()
        {
            //¯\_(ツ)_ /¯
        }
    }
}
