using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    public class MilitaryShips
    {
        private string name;
        private string description;
        private int[] price;
        private int hp;
        private int shield;
        private int damage;

        public MilitaryShips(string name, string description, int[] prices, int hp, int shield, int damage)
        {
            this.name = name;
            this.description = description;
            this.price = prices;
            this.hp = hp;
            this.shield = shield;
            this.damage = damage;
        }

        public void Add(int number, int fleetnumber)
        {
            if (this.price[0] * number <= Program.manager.Metals || this.price[1] * number <= Program.manager.Energy)
            {
                Program.manager.Metals -= price[0] * number;
                Program.manager.Energy -= price[1] * number;
                Program.manager.Fleet[fleetnumber].add(number, damage, hp, shield);
            }
        }

        public void upgrade()
        {
            //¯\_(ツ)_/¯
        }
    }
}
