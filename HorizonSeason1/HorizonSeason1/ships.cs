using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    public class Ship
    {
        private string name;
        private string description;
        private string description2;
        private int[] price;
        private int hp;
        private int shield;
        private int damage;
        private int speed;

        public Ship(string name, string description, int[] price, int hp, int shield, int damage, int speed)
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
            this.description = description;
            this.price = price;
            this.hp = hp;
            this.shield = shield;
            this.damage = damage;
            this.speed = speed;
        }

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Description2 { get => description2; set => description2 = value; }
        public int[] Price { get => price; set => price = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Hp { get => hp; set => hp = value; }
        public int Shield { get => shield; set => shield = value; }
        public int Speed { get => speed; set => speed = value; }

        public void Add(int number, int fleetnumber)
        {
            if (this.price[0] * number <= Program.manager.Metals || this.price[1] * number <= Program.manager.Energy)
            {
                Program.manager.Metals -= price[0] * number;
                Program.manager.Energy -= price[1] * number;
            }
        }

        public bool build(int number = 1)
        {
            if (this.price[0] * number <= Program.manager.Metals && this.price[1] * number <= Program.manager.Energy && this.price[2] * number <= Program.manager.Food) //¯\_(ツ)_/¯ need to differentiate popswithing planets and overall
            {
                Program.manager.Metals -= this.price[0] * number;
                Program.manager.Energy -= this.price[1] * number;
                Program.manager.Food -= this.price[2] * number;
                return true;
            }
            return false;
        }
        public Ship copy()
        {
            return new Ship(name, description, price, hp, shield, damage, speed);
        }
        public void upgrade()
        {
            //¯\_(ツ)_/¯
        }
    }
}
