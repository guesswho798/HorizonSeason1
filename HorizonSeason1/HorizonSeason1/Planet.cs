using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizonSeason1
{
    //add a home star planet chance from life form
    public class Planet
    {
        private string typeName;
        private int baseHabitabilty;
        private int maxPop;
        private int size;
        private int pops = 0;
        private int x;
        private int y;

        public string TypeName { get => typeName; set => typeName = value; }
        public int BaseHabitabilty { get => baseHabitabilty; set => baseHabitabilty = value; }
        public int MaxPop { get => maxPop; set => maxPop = value; }
        public int Size { get => size; set => size = value; }
        public int Pops { get => pops; set => pops = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public Planet(bool homestar, int[] array, int radius)
        {
            string name = "";
            int habitabilty = 0;

            if (homestar == false)
            {
                if (array[0] < 3) //dead planet
                {
                    name = "Destroyed Planet";
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
            }

            TypeName = name;
            baseHabitabilty = habitabilty;

            //getting size of planet
            this.size = array[2];
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
            Random rand = new Random();
            int position = rand.Next(0, 10);

            int middlex = Console.WindowWidth / 2 + radius * 4 + 5;
            int middley = Console.WindowHeight / 2 - radius;

            switch (position)
            {
                case 0:
                    this.x = middlex;
                    this.y = middley;
                    break;
                case 1:
                    this.x = middlex + size;
                    this.y = middley - size;
                    break;
            }

        }
    }
}
