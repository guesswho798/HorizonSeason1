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
        private int hp;
        private int shield;
        private int[] prices = new int[5]; //prices and generate are [Metals, Energy, Food, Happiness, maxpop]
        private int[] generate = new int[5]; 

        public PlanetBuildings(string name, string description, int[] prices, int[] generate,int hp, int shield)
        {
            this.name = name;
            this.description = description;
            this.prices = prices;
            this.generate = generate;
            this.hp = hp;
            this.shield = shield;
        }

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }

        public bool build(int number = 1)
        {
            if (prices[0] * number <= Program.manager.Metals && prices[1] * number <= Program.manager.Energy && prices[2] * number <= Program.manager.Food) //¯\_(ツ)_/¯ need to differentiate popswithing planets and overall
            {
                Program.manager.Metals -= prices[0] * number;
                Program.manager.Energy -= prices[1] * number;
                Program.manager.Food -= prices[2] * number;
                this.number += number;
                return true;
            }
            return false;
        }
        public void turn()
        {
            Program.manager.Metals += generate[0] * number;
            Program.manager.Energy += generate[1] * number;
            Program.manager.Food += generate[2] * number;
        }
        public void upgrade()
        {

        }
    }
}
