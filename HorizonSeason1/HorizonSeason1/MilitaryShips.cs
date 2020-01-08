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
        private int shipid;
        private string description;
        private int[] prices;
        private int hp;
        private int shield;
        private int damage;
        private int number;

        public MilitaryShips(string name, int shipid, string description, int[] prices, int hp, int shield, int damage)
        {
            this.name = name;
            this.shipid = shipid;
            this.description = description;
            this.prices = prices;
            this.hp = hp;
            this.shield = shield;
            this.damage = damage;
            this.number = 0;
        }

        public void Add(int number, int fleetnumber)
        {
            if (this.prices[0] * number <= Program.manager.Metals || this.prices[1] * number <= Program.manager.Energy)
            {
                Program.manager.Metals -= prices[0] * number;
                Program.manager.Energy -= prices[1] * number;
                Program.manager.Fleet[fleetnumber].add(shipid, number, damage, hp, shield);
            }
        }

        public void upgrade()
        {
            //¯\_(ツ)_/¯
        }
    }
}
