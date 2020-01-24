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
        private string description2;
        private int number;
        private int hp;
        private int shield;
        private int numOfTurns;
        private int[] prices = new int[5];
        private int[] generate = new int[5];
        private Planet p;


        public PlanetBuildings(string name, string description, int[] prices, int[] generate, int hp, int shield, int numOfTurns, Planet p = null)
        {
            //split to two because is too long
            string[] d = description.Split(' ');
            for (int i = 0; i < d.Length / 2; i++)
            {
                this.description += d[i] + " ";
            }
            if (this.description.Length > 24)
            {
                this.description = "";
                for (int i = 0; i < d.Length / 2 - 1; i++)
                {
                    this.description += d[i] + " ";
                }
                for (int i = d.Length / 2 - 1; i < d.Length; i++)
                {
                    this.description2 += d[i] + " ";
                }
            }
            else
            {
                for (int i = d.Length / 2; i < d.Length; i++)
                {
                    this.description2 += d[i] + " ";
                }
            }

            this.name = name;
            this.prices = prices;
            this.generate = generate;
            this.hp = hp;
            this.shield = shield;
            this.numOfTurns = numOfTurns;
            this.number = 1;
            this.p = p;
        }

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Description2 { get => description2; set => description2 = value; }
        public int NumOfTurns { get => numOfTurns; set => numOfTurns = value; }

        public bool build(int number = 1)
        {
            if (this.prices[0] * number <= Program.manager.Metals && this.prices[1] * number <= Program.manager.Energy && this.prices[2] * number <= Program.manager.Food) //¯\_(ツ)_/¯ need to differentiate popswithing planets and overall
            {
                Program.manager.Metals -= this.prices[0] * this.number;
                Program.manager.Energy -= this.prices[1] * this.number;
                Program.manager.Food -= this.prices[2] * this.number;
                this.number += number;
                return true;
            }
            return false;
        }
        public bool turn()
        {
            if (numOfTurns == 1)
            {
                Program.manager.Metals += this.generate[0] * this.number;
                Program.manager.Energy += this.generate[1] * this.number;
                Program.manager.Food += this.generate[3] * this.number;
                Program.manager.Happiness += this.generate[4] * this.number;
                return true;
            }
            else
            {
                this.numOfTurns--;
                Console.WriteLine(this.name + ": " + numOfTurns);
                return false;
            }
        }
        public PlanetBuildings copy()
        {
            PlanetBuildings p = new PlanetBuildings(name, description, prices, generate, hp, shield, NumOfTurns, this.p);
            return p;
        }
        public void upgrade()
        {
            
        }
        public void attachp(Planet p)
        {
            this.p = p;
        }

    }
}
