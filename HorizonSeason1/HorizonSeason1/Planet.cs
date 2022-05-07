using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HorizonSeason1
{
    public class Planet
    {
        private string typeName;
        private int baseHabitabilty;
        private int maxPop;
        private int size;
        private double pops = 0;
        private int position;
        private int shipyardPosition;
        private bool playerOwned;
        private bool creatureOwned;
        private int range;
        private LifeForm life;
        private PlanetBuildings[] buildings;
        private PlanetBuildings[] buildingqueue;
        private bool hasShipyard;
        private int x;
        private int y;


        public string TypeName { get => typeName; set => typeName = value; }
        public int BaseHabitabilty { get => baseHabitabilty; set => baseHabitabilty = value; }
        public int MaxPop { get => maxPop; set => maxPop = value; }
        public int Size { get => size; set => size = value; }
        public double Pops { get => pops; set => pops = value; }
        public int Position { get => position; set => position = value; }
        public bool CreatureOwned { get => creatureOwned; set => creatureOwned = value; }
        public bool PlayerOwned { get => playerOwned; set => playerOwned = value; }
        public LifeForm Life { get => life; set => life = value; }
        public bool HasShipyard { get => hasShipyard; set => hasShipyard = value; }
        public int ShipyardPosition { get => shipyardPosition; set => shipyardPosition = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Range { get => range; set => range = value; }

        public Planet(bool homestar, int[] array, int radius, Random rand, int position, int x, int y)
        {
            hasShipyard = homestar;
            string name = "";
            int habitabilty = 0;
            this.creatureOwned = false;
            buildingqueue = new PlanetBuildings[0];
            buildings = new PlanetBuildings[0];
            this.x = x;
            this.y = y;

            if (homestar == false)
            {
                this.playerOwned = false;
                if (array[0] < 3) //dead planet
                {
                    name = "Destroyed Planet";
                    //size = 3;
                    habitabilty = 0;
                }
                else //a habitable planet
                {
                    switch (array[1])
                    {
                        case 1:
                            name = "Gaia Planet";
                            habitabilty = 100;
                            break;
                        case 2:
                            name = "Temperate Planet";
                            habitabilty = 80;
                            break;
                        case 3:
                            name = "Cold Planet";
                            habitabilty = 80;
                            break;
                        case 4:
                            name = "Hot Planet";
                            habitabilty = 80;
                            break;
                        case 5:
                            name = "Ocean Planet";
                            habitabilty = 80;
                            break;
                        case 6:
                            name = "Jungle Planet";
                            habitabilty = 60;
                            break;
                        case 7:
                            name = "Permafrost Planet";
                            habitabilty = 60;
                            break;
                        case 8:
                            name = "Frigid Planet";
                            habitabilty = 60;
                            break;
                        case 9:
                            name = "Scorched Planet";
                            habitabilty = 60;
                            break;
                        case 10:
                            name = "Tropical Planet";
                            habitabilty = 60;
                            break;
                        case 11:
                            name = "Tundra Planet";
                            habitabilty = 60;
                            break;
                        case 12:
                            name = "Desert Planet";
                            habitabilty = 40;
                            break;
                        case 13:
                            name = "Frozen Planet";
                            habitabilty = 40;
                            break;
                        case 14:
                            name = "Extreme Planet";
                            habitabilty = 40;
                            break;
                        case 15:
                            name = "Dead Planet (hot)";
                            habitabilty = 20;
                            break;
                        case 16:
                            name = "Dead Planet (cold)";
                            habitabilty = 20;
                            break;
                        case 17:
                            name = "Tomb Planet";
                            habitabilty = 20;
                            break;
                    }
                }
            }
            else if (homestar == true)
            {
                //first you need to add life forms
                this.playerOwned = true;
                pops = 1;

                switch (rand.Next(0, 3))
                {
                    case 0:
                        name = "Gaia Planet";
                        habitabilty = 100;
                        break;
                    case 1:
                        name = "Ocean Planet";
                        habitabilty = 80;
                        break;
                    case 2:
                        name = "Temperate Planet";
                        habitabilty = 80;
                        break;
                }
            }

            TypeName = name;
            baseHabitabilty = habitabilty;

            //getting size of planet
            //if (name != "Destroyed Planet")
            //{
            this.size = array[2];
            //}
            switch (array[2])
            {
                case 1:
                    maxPop = 8;
                    break;
                case 2:
                    maxPop = 10;
                    break;
                case 3:
                    maxPop = 12;
                    break;
                case 4:
                    maxPop = 15;
                    break;
            }

            //randomise place in system
            this.Position = position;
        }

        public Planet(int[] array, int radius, Random rand, int position, LifeForm life, int x, int y)
        {
            hasShipyard = false;
            this.playerOwned = false; //false
            this.creatureOwned = true;
            this.Position = position;
            this.life = life;
            buildingqueue = new PlanetBuildings[0];
            buildings = new PlanetBuildings[0];
            this.x = x;
            this.y = y;


            //which planet
            switch (rand.Next(0, 3))
            {
                case 0:
                    typeName = "Gaia Planet";
                    baseHabitabilty = 100;
                    break;
                case 1:
                    TypeName = "Ocean Planet";
                    baseHabitabilty = 80;
                    break;
                case 2:
                    TypeName = "Temperate Planet";
                    baseHabitabilty = 80;
                    break;
            }

            //size and max population
            this.size = array[2];
            switch (size)
            {
                case 1:
                    maxPop = 8;
                    break;
                case 2:
                    maxPop = 10;
                    break;
                case 3:
                    maxPop = 12;
                    break;
                case 4:
                    maxPop = 15;
                    break;
            }
            this.pops = rand.Next(1, this.maxPop / 2);
        }

        public void turn()
        {
            //building buildings
            for (int i = 0; i < buildings.Length; i++)
            {
                buildings[i].turn();
            }
            for (int i = buildingqueue.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(buildingqueue[i].NumOfTurns);
                if (buildingqueue[i].turn())
                {
                    addbuilding(buildingqueue[i]);
                    Array.Resize(ref buildingqueue, buildingqueue.Length - 1);
                }
            }

            //adding pop
            double add = this.baseHabitabilty;
            add /= 100;
            pops += pops * 0.05 * add;
            pops = Math.Min(pops, maxPop);
        }
        public string GetQueue()
        {
            Dictionary<string, int> dict = new System.Collections.Generic.Dictionary<string, int>();
            for (int i = 0; i < buildingqueue.Length; i++)
            {
                //checking if in dictionary
                bool has = false;
                if (dict.Count != 0) //on first time there is nothing to look for in dictionary
                {
                    //i dont know why i need to do this but i need to
                    try
                    {
                        foreach (KeyValuePair<string, int> item in dict)
                        {
                            //if it is then add one to it
                            if (buildingqueue[i].Name + "(" + buildingqueue[i].NumOfTurns + ")" == item.Key)
                            {
                                has = true;
                                dict[buildingqueue[i].Name + "(" + buildingqueue[i].NumOfTurns + ")"]++;
                            }

                        }
                    }
                    catch { }
                }
                //if not then create a place for it in dictionary
                if (has == false)
                {
                    dict.Add(buildingqueue[i].Name + "(" + buildingqueue[i].NumOfTurns + ")", 1);
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
        public string GetBuildings()
        {
            Dictionary<string, int> dict = new System.Collections.Generic.Dictionary<string, int>();
            for (int i = 0; i < buildings.Length; i++)
            {
                //checking if in dictionary
                bool has = false;
                if (dict.Count != 0) //on first time there is nothing to look for in dictionary
                {
                    //i dont know why i need to do this but i need to
                    try
                    {
                        foreach (KeyValuePair<string, int> item in dict)
                        {
                            //if it is then add one to it
                            if (buildings[i].Name == item.Key)
                            {
                                has = true;
                                dict[buildings[i].Name]++;
                            }

                        }
                    }
                    catch { }
                }
                //if not then create a place for it in dictionary
                if (has == false)
                {
                    dict.Add(buildings[i].Name, 1);
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
        public string GetProduction()
        {
            Dictionary<string, int> dictnames = new System.Collections.Generic.Dictionary<string, int>();
            for (int i = 0; i < buildings.Length; i++)
            {
                //checking if in dictionary
                bool has = false;
                if (dictnames.Count != 0) //on first time there is nothing to look for in dictionary
                {
                    //i dont know why i need to do this but i need to
                    try
                    {
                        foreach (KeyValuePair<string, int> item in dictnames)
                        {
                            //if it is then add one to it
                            if (buildings[i].Name == item.Key)
                            {
                                has = true;
                                dictnames[buildings[i].Name]++;
                            }

                        }
                    }
                    catch { }
                }
                //if not then create a place for it in dictionary
                if (has == false)
                {
                    dictnames.Add(buildings[i].Name, 1);
                }
            }


            Dictionary<string, int> dict = new System.Collections.Generic.Dictionary<string, int>();

            //turns names to productions
            foreach (KeyValuePair<string, int> item in dictnames)
            {
                switch (item.Key)
                {
                    case "Energy Plant":
                        dict.Add("Energy: ", 50 * item.Value);
                        break;
                    case "Mines":
                        dict.Add("Metals: ", 25 * item.Value);
                        break;
                    case "Farms":
                        dict.Add("Food: ", 5 * item.Value);
                        break;
                    case "Mega-malls":
                        dict.Add("Happiness: ", 10 * item.Value);
                        break;
                }
            }


            string s = "";
            int l = dict.Count;
            int counter = 0;
            foreach (KeyValuePair<string, int> item in dict)
            {
                s += item.Key + item.Value;

                if (counter != l - 1)
                    s += ", ";

                counter++;
            }

            return s;
        }
        public void addQueue(PlanetBuildings b)
        {
            Array.Resize(ref buildingqueue, buildingqueue.Length + 1);
            buildingqueue[buildingqueue.Length - 1] = b;

            //making it easier to remove the last one
            buildingqueue = buildingqueue.OrderByDescending(d => d.NumOfTurns).ToArray();
        }
        public void addbuilding(PlanetBuildings b)
        {
            Array.Resize(ref buildings, buildings.Length + 1);
            buildings[buildings.Length - 1] = b;

            //on first round dont add
            switch (b.Name)
            {
                case "Housing district":
                    maxPop++;
                    break;
                case "Energy Plant":
                    Program.manager.Energy -= 50;
                    break;
                case "Mines":
                    Program.manager.Metals -= 25;
                    break;
                case "Farms":
                    Program.manager.Food -= 5;
                    break;
                case "Mega-malls":
                    Program.manager.Happiness -= 10;
                    break;
            }

            if (b.Name == "Shipyard")
                HasShipyard = true;
        }
        public void MovePlanet()
        {
            Position++;
            if (Position == 8)
            {
                position = 0;
            }
        }
    }
}
