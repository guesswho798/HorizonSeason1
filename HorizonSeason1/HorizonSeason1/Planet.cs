using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    public class Planet
    {
        private string typeName;
        private int baseHabitabilty;
        private int maxPop;
        private int size;
        private int pops = 0;
        private int position;
        private bool playerOwned;
        private bool creatureOwned;
        private LifeForm life;
        private PlanetBuildings[] buildings;
        private PlanetBuildings[] buildingqueue;
        

        public string TypeName { get => typeName; set => typeName = value; }
        public int BaseHabitabilty { get => baseHabitabilty; set => baseHabitabilty = value; }
        public int MaxPop { get => maxPop; set => maxPop = value; }
        public int Size { get => size; set => size = value; }
        public int Pops { get => pops; set => pops = value; }
        public int Position { get => position; set => position = value; }
        public bool CreatureOwned { get => creatureOwned; set => creatureOwned = value; }
        public bool PlayerOwned { get => playerOwned; set => playerOwned = value; }
        public LifeForm Life { get => life; set => life = value; }

        public Planet(bool homestar, int[] array, int radius, Random rand, int position)
        {
            string name = "";
            int habitabilty = 0;
            this.creatureOwned = false;
            buildingqueue = new PlanetBuildings[0];
            buildings = new PlanetBuildings[0];

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

        public Planet(int[] array, int radius, Random rand, int position, LifeForm life)
        {
            this.playerOwned = false;
            this.creatureOwned = true;
            this.Position = position;
            this.life = life;
            buildingqueue = new PlanetBuildings[0];
            buildings = new PlanetBuildings[0];

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


        public string GetQueue()
        {
            string s = "";

            for (int i = 0; i < buildingqueue.Length; i++)
            {
                s += buildingqueue[i].Name;
                if (i != buildingqueue.Length - 1)
                {
                    s += ", ";
                }
            }

            return s;
        }
        public void addQueue(PlanetBuildings b)
        {
            Array.Resize(ref buildingqueue, buildingqueue.Length + 1);
            buildingqueue[buildingqueue.Length - 1] = b;
        }
        public void addbuilding(PlanetBuildings b)
        {
            Array.Resize(ref buildings, buildings.Length + 1);
            buildings[buildings.Length - 1] = b;
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
