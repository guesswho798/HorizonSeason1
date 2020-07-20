using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    public class fleet
    {
        static int fleetId;
        private string name;
        private int attackPoints;
        private int hp;
        private int shield;
        private int numOfShips;
        private Ship[] ships;
        private int x;
        private int y;
        private Planet place;     //place of fleet
        private Planet target;    //target planet
        private int eta;          //estimated time of arrival


        public static int FleetId { get => fleetId; set => fleetId = value; }
        public string Name { get => name; set => name = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public Planet Place { get => place; set => place = value; }
        public int Eta { get => eta; set => eta = value; }
        public Planet Target { get => target; set => target = value; }

        public fleet(string name, Planet place, int x, int y)
        {
            this.name = name;
            ships = new Ship[50];
            attackPoints = 0;
            hp = 0;
            shield = 0;
            numOfShips = 0;
            fleetId++;
            this.x = x;
            this.y = y;
            this.place = place;
        }

        public void add(Ship s, int number)
        {
            for (int i = 0; i < number; i++)
            {
                ships[numOfShips] = s.copy();
                this.numOfShips++;
            }
            
            //this.attackPoints += attackPoints * number;
            //this.hp += hp * number;
            //this.shield += shield * number;
        }
        public void subtract(int damage)
        {
            int tmp = 0;
            if (shield > 0)
            {
                shield -= damage;
                if (shield < 0)
                {
                    tmp = shield * -1;
                    hp -= tmp;
                }
            }
            else
            {
                hp -= damage;
            }
        }
        public void lunch(Planet p)
        {
            this.place = null;
            this.target = p;
        }

        public Ship[] GetInfo()
        {
            return ships;
        }
        public string getInfoString()
        {
            Dictionary<string, int> dict = new System.Collections.Generic.Dictionary<string, int>();
            for (int i = 0; i < ships.Length; i++)
            {
                //checking if in dictionary
                bool has = false;
                if (dict.Count != 0) //on first time there is nothing to look for in dictionary
                {
                    //i dont know why i need to do this but i need to
                    if (ships[i] != null)
                    {
                        foreach (KeyValuePair<string, int> item in dict.ToList())
                        {
                            //if it is then add one to it
                            if (ships[i].Name == item.Key)
                            {
                                has = true;
                                dict[ships[i].Name]++;
                            }

                        }
                    }
                }
                //if not then create a place for it in dictionary
                if (has == false)
                {
                    if (ships[i] != null)
                    {
                        dict.Add(ships[i].Name, 1);
                    }
                }
            }


            string s = "";
            int l = dict.Count;
            int counter = 0;
            foreach (KeyValuePair<string, int> item in dict)
            {
                if (item.Value != 1)
                    s += item.Key + " X " + item.Value;
                else
                    s += item.Key;

                if (counter != l - 1)
                    s += ", ";

                counter++;
            }

            return s;
        }
        public int GetHP()
        {
            return hp;
        }
        public int GetShield()
        {
            return shield;
        }
        public int Getatt()
        {
            return attackPoints;
        }
        public int getSpeed()
        {
            int min = ships[0].Speed;

            for (int i = 0; i < ships.Length; i++)
            {
                
                if (ships[i] != null && ships[i].Speed < min)
                {
                    if (ships[i].Speed == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("hit 0");
                        Console.WriteLine(ships[i].Name);
                        Console.WriteLine(ships[i].Speed);
                        Console.WriteLine(ships[i].Hp);
                        Console.ReadKey();
                    }
                    min = ships[i].Speed;
                }
            }

            return min;
        }
        public int calcETA(int x, int y, bool lunch=false, Planet p=null)
        {
            int eta = Math.Abs(this.X - x) + Math.Abs(this.Y - y) / this.getSpeed();

            if (eta <= 0)
                eta = 1;

            if (lunch)
            {
                this.eta = eta;
                this.lunch(p);
            }

            return eta;
        }
    }
}
